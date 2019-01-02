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

            using (var fileStream = new FileStream(fileName, FileMode.CreateNew))
            {
                formFile.CopyTo(fileStream);
            }

            return fileLocation;
        }

        public string RecreateImage(IFormFile formFile, string folder, string imageLocation, string id)
        {
            if (string.IsNullOrEmpty(imageLocation))
            {
                return this.CreateImage(formFile, folder, id);
            }

            var fileName = this.environment.WebRootPath + imageLocation;

            if (File.Exists(fileName))
            {
                if (fileName.Contains("version="))
                {
                    var version = int.Parse(fileName.Split("version=")[1].Replace(".jpg", string.Empty));
                    version++;
                    imageLocation = $"\\images\\{folder}\\" + $"{id}version={version}.jpg";
                    fileName = this.environment.WebRootPath + imageLocation;
                }
                else
                {
                    imageLocation = $"\\images\\{folder}\\" + $"{id}version=1.jpg";
                    fileName = this.environment.WebRootPath + imageLocation;
                }
            }

            using (var fileStream = new FileStream(fileName, FileMode.CreateNew))
            {
                formFile.CopyTo(fileStream);
            }

            return imageLocation;
        }
    }
}
