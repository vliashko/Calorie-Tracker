﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges);
        Task<User> GetUserAsync(Guid userId, bool trackChanges);
        void CreateUser(User user);
        void DeleteUser(User user);
    }
}
