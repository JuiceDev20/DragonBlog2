using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBlog2.Utilities
{
    public class ImageHelper
    {
        public byte[] EncodeImage(IFormFile image)
        {
            var ms = new MemoryStream();
            image.CopyTo(ms);
            var output = ms.ToArray();

            ms.Close();
            ms.Dispose();

            return output;
        }
        public string DecodeImage(byte[] image, string fileName)
        {
            var binary = Convert.ToBase64String(image);

            var ext = Path.GetExtension(fileName);

            string imageDataURL = $"data:image/{ext};base64,{binary}";

            return imageDataURL;
        } 
    }
}
