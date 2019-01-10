using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.DataServices.Models.Users;

namespace Foody.Services.DataServices.Users
{
    public  interface IUsersService
    {
        AllUsersRolesViewModel GetAllUsersWithRoles();

        void ChangeRole(string username, string role);
    }
}
