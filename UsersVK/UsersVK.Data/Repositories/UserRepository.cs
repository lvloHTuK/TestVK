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


        public async Task Add(User user) => await db.AddAsync(user);

        public async Task<User> AddRole(User user, string role)
        {
            var _user = await db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (user != null)
            {
                if (GroupEnum.Admin.ToString() == role)
                {
                    if (GetRole("Admin") == null)
                    {
                        _user.UserGroupId.Code = GroupEnum.Admin;
                        db.Users.Update(user);

                        return _user;
                    }

                    return null;
                }
                else if (GroupEnum.User.ToString() == role)
                {
                    _user.UserGroupId.Code = GroupEnum.User;
                    db.Users.Update(user);

                    return _user;
                }

                return null;
            }

            return null;
        }

        public async Task<User> Delete(User user)
        {
            var _user = await db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (user != null)
            {
                _user.UserStateId.Code = StateEnum.Blocked;
                db.Users.Update(user);

                return _user;
            }

            return null;
        }

        public async Task<User> Edit(User user, string login)
        {
            var _user = await db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (user != null)
            {
                _user.Login = login;
                db.Users.Update(user);

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

        public async Task<User> GetRole(string role)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.UserStateId.Code.ToString() == role);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        public async Task Save() => await db.SaveChangesAsync();
    }
}
