using System.Collections.Generic;

namespace Foody.Services.DataServices.Models.Users
{
    public class UserRoleViewModel
    {
        public string Username { get; set; }

        public IList<string> Roles { get; set; }
    }
}
