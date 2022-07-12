using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Helpers
{
    public static class ImageHelper
    {
        public static string SaveImage(IFormFile image, string rootPath)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.FileName);
            string extension = Path.GetExtension(image.FileName);
            string imagePath = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(rootPath + "/Image", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return imagePath;
        }
        public static void DeleteImage(string rootPath, string imagePath) 
        {
            string existingFile = Path.Combine(rootPath + "/Image", imagePath);
            System.IO.File.Delete(existingFile);
        }
    }
}
