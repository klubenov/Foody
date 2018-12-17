using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.DataServices.Images
{
    public interface IImagesService
    {
        string CreateImage(IFormFile formFile, string folder, string id);
    }
}
