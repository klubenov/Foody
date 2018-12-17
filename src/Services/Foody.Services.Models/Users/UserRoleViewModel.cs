using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Users
{
    public class UserRoleViewModel
    {
        public string Username { get; set; }

        public IList<string> Roles { get; set; }
    }
}
