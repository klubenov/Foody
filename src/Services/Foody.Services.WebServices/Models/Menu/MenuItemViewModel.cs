namespace Foody.Services.WebServices.Models.Menu
{
    public class MenuItemViewModel
    {
        public string AreaName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string[] AvailableToRoles { get; set; }
    }
}
