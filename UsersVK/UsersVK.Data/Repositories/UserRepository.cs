using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersVK.Interfaces;
using UsersVK.Models;

namespace UsersVK.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext db;

        public UserRepository(ApplicationDbContext context)
        {
            db = context;
        }


        public async Task Add(User user)
        {

            //await db.AddAsync(user);
            user.UserGroupId = Guid.NewGuid();
            user.UserStateId = Guid.NewGuid();
            await db.Users.AddAsync(user);
            UserGroup userGroup = new UserGroup { Id = user.UserGroupId };
            UserState userState = new UserState { Id = user.UserStateId, Code = StateEnum.Active };
            await db.UserGroups.AddAsync(userGroup);
            await db.UserStates.AddAsync(userState);

        }

        public async Task Remove(User user)
        {
            db.Users.Remove(user);
            var userGroup = await db.UserGroups.FirstOrDefaultAsync(x => x.Id == user.UserGroupId);
            var userState = await db.UserStates.FirstOrDefaultAsync(x => x.Id == user.UserStateId);
            db.UserGroups.Remove(userGroup);
            db.UserStates.Remove(userState);

        }

        public async Task<User> AddRole(User user, string role)
        {
            var _user = await db.Users.FirstOrDefaultAsync(x => x.Login == user.Login);
            var _userGroup = await db.UserGroups.FirstOrDefaultAsync(x => x.Id == _user.UserGroupId);

            if (user != null)
            {
                if (GroupEnum.Admin.ToString() == role)
                {
                    var getRole = await GetRole("Admin");
                    if (getRole == null)
                    {
                        _userGroup.Code = GroupEnum.Admin;
                        db.UserGroups.Update(_userGroup);

                        return _user;
                    }

                    return null;
                }
                else if (GroupEnum.User.ToString() == role)
                {
                    _userGroup.Code = GroupEnum.User;
                    db.UserGroups.Update(_userGroup);

                    return _user;
                }

                return null;
            }

            return null;
        }

        public async Task<User> Delete(User user)
        {
            var _user = await db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            var _userState = await db.UserStates.FirstOrDefaultAsync(x => x.Id == _user.UserStateId);

            if (user != null)
            {
                _userState.Code = StateEnum.Blocked;
                db.UserStates.Update(_userState);

                return _user;
            }

            return null;
        }

        public async Task<User> AddDescription(User user, string descGroup, string descState)
        {
            var _user = await db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            var _userGroup = await GetUserGroup(user.UserGroupId);
            var _userState = await GetUserState(user.UserStateId);

            if (user != null)
            {
                _userGroup.Description = descGroup;
                db.UserGroups.Update(_userGroup);
                _userState.Description = descState;
                db.UserStates.Update(_userState);

                return _user;
            }

            return null;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await db.Users.ToListAsync();

            if (users != null)
            {
                return users;
            }

            return null;
        }

        public async Task<User> GetById(string id)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        public async Task<User> GetByLogin(string userLogin)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Login == userLogin);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        public async Task<UserGroup> GetRole(string role)
        {
            GroupEnum y = GroupEnum.User;
            if(role == "Admin")
            {
                y = GroupEnum.Admin;
            }
            var userGroup = await db.UserGroups.FirstOrDefaultAsync(x => x.Code == y);

            if (userGroup != null)
            {
                return userGroup;
            }

            return null;
        }

        public async Task<UserGroup> GetUserGroup(Guid id)
        {
            var userGroup = await db.UserGroups.FirstOrDefaultAsync(x => x.Id == id);

            if (userGroup != null)
            {
                return userGroup;
            }

            return null;
        }

        public async Task<UserState> GetUserState(Guid id)
        {
            var userState = await db.UserStates.FirstOrDefaultAsync(x => x.Id == id);

            if (userState != null)
            {
                return userState;
            }

            return null;
        }

        public async Task Save() => await db.SaveChangesAsync();
    }
}
