using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.DataServices.Images
{
    public class ImagesService : IImagesService
    {
        private readonly IHostingEnvironment environment;

        public ImagesService(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        public string CreateImage(IFormFile formFile, string folder, string id)
        {
            var fileLocation = $"\\images\\{folder}\\" + $"{id}.jpg";
            var fileName = this.environment.WebRootPath + fileLocation;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }

            return fileLocation;
        }
    }
}
