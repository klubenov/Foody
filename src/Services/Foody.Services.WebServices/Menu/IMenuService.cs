using System;
using Foody.Services.WebServices.Models.Menu;

namespace Foody.Services.WebServices.Menu
{
    public interface IMenuService
    {
        MenuViewModel GetMenuItems(Type controllerType, Type httpGetAttributeType, Type authorizeAttributeType, string area, string selectedPartial, dynamic selectedPartialModel);
    }
}
