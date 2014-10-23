﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LuaBijoux.Core.DomainModels.Identity;
using LuaBijoux.Data.Extensions;
using LuaBijoux.Data.Identity.Models;
using Microsoft.AspNet.Identity;
using LuaBijoux.Core.Identity;
using Microsoft.Owin.Security;

namespace LuaBijoux.Data.Identity
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly UserManager<ApplicationIdentityUser, int> _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private bool _disposed;

        public ApplicationUserManager(UserManager<ApplicationIdentityUser, int> userManager, IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }

        public async Task<AppUser> FindByIdAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            return user.ToAppUser();
        }

        public async Task<AppUser> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
            return user.ToAppUser();
        }
       
        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync().ConfigureAwait(false);
            return users.ToAppUserList();
        } 

        public async Task<ApplicationIdentityResult> CreateAsync(AppUser user)
        {
            var applicationUser = user.ToApplicationUser();
            var identityResult = await _userManager.CreateAsync(applicationUser).ConfigureAwait(false);
            user.CopyApplicationIdentityUserProperties(applicationUser);
            return identityResult.ToApplicationIdentityResult();
        }

        public async Task<ApplicationIdentityResult> CreateAsync(AppUser user, string password)
        {
            var applicationUser = user.ToApplicationUser();
            var identityResult = await _userManager.CreateAsync(applicationUser, password).ConfigureAwait(false);
            user.CopyApplicationIdentityUserProperties(applicationUser);
            return identityResult.ToApplicationIdentityResult();
        }

        public async Task<ApplicationIdentityResult> UpdateAsync(AppUser user)
        {
            if (user == null)
            {
                return new ApplicationIdentityResult(new[] { "Usuário não informado" }, false);
            }

            try
            {
                ApplicationIdentityUser applicationUser = _userManager.FindById(user.Id);
                applicationUser.CopyAppUserProperties(user);
                var identityResult = await _userManager.UpdateAsync(applicationUser);
                return identityResult.ToApplicationIdentityResult();
            }
            catch(Exception e)
            {
                return new ApplicationIdentityResult(new[] { e.Message }, false);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
