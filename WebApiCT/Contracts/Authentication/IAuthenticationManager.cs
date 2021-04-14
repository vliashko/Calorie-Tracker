﻿using Entities.DataTransferObjects;
using System.Threading.Tasks;

namespace Contracts.Authentication
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}
