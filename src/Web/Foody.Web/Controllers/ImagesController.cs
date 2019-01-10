using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Foody.Web.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IHostingEnvironment environment;

        public ImagesController(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        public IActionResult Get(string location)
        {
            if (string.IsNullOrEmpty(location) || !location.StartsWith("\\images"))
            {
                return null;
            }
            var filePath = this.environment.WebRootPath + location;
            
            var file = PhysicalFile(filePath, "image/jpeg");

            return file;
        }
    }
}