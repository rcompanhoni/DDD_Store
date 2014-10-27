﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using LuaBijoux.Core.DomainModels.Identity;

namespace LuaBijoux.Core.Identity
{
    public interface IApplicationUserManager : IDisposable
    {
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<ApplicationIdentityResult> CreateAsync(AppUser user, string password);
        Task<AppUser> FindByIdAsync(int userId);
        Task<AppUser> FindByEmailAsync(string email);
        AppUser FindByEmail(string email);
        Task<AppUser> FindByCpfAsync(string cpf);
        AppUser FindByCpf(string cpf);
        Task<ApplicationIdentityResult> UpdateAsync(AppUser user);
        Task<ApplicationIdentityResult> DeleteAsync(int userId);
    }
}