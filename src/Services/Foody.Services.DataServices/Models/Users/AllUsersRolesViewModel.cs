using System.Collections.Generic;

namespace Foody.Services.DataServices.Models.Users
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
