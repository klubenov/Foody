using System;
using System.Collections.Generic;
using System.Text;
using Foody.Data.Models;
using System.Linq;
using Foody.Services.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace Foody.Services.DataServices.Users
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<FoodyUser> userManager;

        public UsersService(UserManager<FoodyUser> userManager)
        {
            this.userManager = userManager;
        }

        public AllUsersRolesViewModel GetAllUsersWithRoles()
        {
            var users = this.userManager.Users.ToArray();

            var allUsersRolesViewModel = new AllUsersRolesViewModel();

            foreach (var user in users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    Username = user.UserName,
                    Roles = this.userManager.GetRolesAsync(user).Result
                };

                allUsersRolesViewModel.UserRoleViewModels.Add(userRoleViewModel);
            }

            return allUsersRolesViewModel;
        }

        public void ChangeRole(string username, string role)
        {
            var user = this.userManager.Users.First(u => u.UserName == username);
            var currentRole = this.userManager.GetRolesAsync(user).Result.First();
            this.userManager.RemoveFromRoleAsync(user, currentRole).GetAwaiter().GetResult();

            this.userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();
        }
    }
}
