using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;
//using System.Web.Mvc;
//using System.Web.HttpContext.Current;
//using System.Web.Optimization;

namespace EbayView.Controllers.UploadImg
{
    public class UploadImgController : Controller //Microsoft.AspNetCore.Mvc.Controller
    {   // to run add services.AddSingleton<UploadImgController>();
        private readonly IWebHostEnvironment webHostEnvironment;
        [Obsolete]
        private IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public UploadImgController(IWebHostEnvironment _webHostEnvironment, IHostingEnvironment hostingEnvironment)
        {
             webHostEnvironment = _webHostEnvironment;
            _hostingEnvironment = hostingEnvironment;
        } 
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> UploadLogo(IFormFile file)
        {
            var fileName = "";
            var fullPath = "";
            try
            {
                if (file.Length > 0)
                {
                    //string webRootPath = _hostingEnvironment.WebRootPath + "\\ProfileImages\\";
                    string webRootPath = _hostingEnvironment.WebRootPath + "\\img\\Uploads\\Photos";
                    fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    fullPath = Path.Combine(webRootPath, fileName); 
                    using (FileStream fileStream = System.IO.File.Create(fullPath))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                    }
                } 
                var imagePath = "/ProfileImages/" + fileName; 
               // return Ok(imagePath)                               
               return Json(new { success = true, imageURL = fullPath, imagename = fileName, responseText = "wellcom" }); 
            }
            catch (Exception ex)
            {
                //return Ok(fileName);
                return Json(new { Success = false, Message = ex.Message });
            }
        } 
        // not working 
        public static string DeleteFile(string folderName, string fileName)
        {
            try
            {
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\img\\Uploads\\Photos" + folderName + fileName))
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\img\\Uploads\\Photos" + folderName + fileName);
                }
                var result = "File Deleted!";
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } 
    }
}
