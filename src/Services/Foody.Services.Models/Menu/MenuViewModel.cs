using System.Collections.Generic;

namespace Foody.Services.Models.Menu
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            this.MenuItemViewModels = new List<MenuItemViewModel>();
        }

        public List<MenuItemViewModel> MenuItemViewModels { get; set; }

        public string SelectedPartial { get; set; }

        public dynamic PreLoadedModel { get; set; }
    }
}
