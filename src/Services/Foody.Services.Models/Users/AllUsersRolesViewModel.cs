using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Users
{
    public class AllUsersRolesViewModel
    {
        public AllUsersRolesViewModel()
        {
            this.UserRoleViewModels = new List<UserRoleViewModel>();
        }

        public List<UserRoleViewModel> UserRoleViewModels { get; set; }
    }
}
