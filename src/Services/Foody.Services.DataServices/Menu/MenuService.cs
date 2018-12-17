using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Foody.Services.Models;
using Foody.Services.Models.Menu;

namespace Foody.Services.DataServices.Menu
{
    public class MenuService : IMenuService
    {
        
        public MenuViewModel GetMenuItems(Type controllerType, Type httpGetAttributeType, Type authorizeAttributeType, string area, string selectedPartial, dynamic selectedPartialModel)
        {
            var httpGetMethods = controllerType.GetMethods().Where(m => m.GetCustomAttributes(httpGetAttributeType, true).Length > 0 && !m.Name.Contains("Menu")).ToArray();

            var menuViewModel = new MenuViewModel
            {
                SelectedPartial = selectedPartial,
                PreLoadedModel = selectedPartialModel
                
            };
            
            foreach (var methodInfo in httpGetMethods)
            {
                var authorizeAttributeData = methodInfo.GetCustomAttributesData().First(ca => ca.AttributeType == authorizeAttributeType);
                var roleData = authorizeAttributeData.NamedArguments.FirstOrDefault().TypedValue.Value.ToString();
                var availableToRoles = roleData.Split(new []{ ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var menuItemViewModel = new MenuItemViewModel
                {
                    ControllerName = controllerType.Name.Replace("Controller", ""),
                    AreaName = area,
                    ActionName = methodInfo.Name,
                    AvailableToRoles = availableToRoles
                };
                menuViewModel.MenuItemViewModels.Add(menuItemViewModel);
            }

            return menuViewModel;
        }
    }
}
