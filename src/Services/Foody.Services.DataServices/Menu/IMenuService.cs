using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models;
using Foody.Services.Models.Menu;

namespace Foody.Services.DataServices.Menu
{
    public interface IMenuService
    {
        MenuViewModel GetMenuItems(Type controllerType, Type httpGetAttributeType, Type authorizeAttributeType, string area, string selectedPartial, dynamic selectedPartialModel);
    }
}
