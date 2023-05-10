using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using UsersVK.Models;
using System.Collections.Generic;

namespace UsersVK.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddRole(User user, string role);
        Task<User> GetRole(string role);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(string id);
        Task<User> GetByLogin(string userLogin);
        Task Add(User user);
        Task<User> Edit(User user, string login);
        Task<User> Delete(User user);
        Task Save();
    }
}
