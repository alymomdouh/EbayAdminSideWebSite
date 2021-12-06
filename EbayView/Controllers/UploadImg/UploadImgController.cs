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
        private IHostingEnvironment _hostingEnvironment;
        public UploadImgController(IWebHostEnvironment _webHostEnvironment, IHostingEnvironment hostingEnvironment)
        {
             webHostEnvironment = _webHostEnvironment;
            _hostingEnvironment = hostingEnvironment;
        }
        public  JsonResult  UploadImagefun(IFormFile Photo)   //async Task<JsonResult> 
        { 
            var files = Request.Form.Files;
            if (files.Count>0)
            {
                try
                {  //IFormFile file = files[0];
                    foreach (var file in files)
                    {
                         if (file.Length > 0 && file!=null)
                        {
                            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                            //var path = Path.Combine(HttpContext.Current.Server.MapPath()("~/content/images/"), fileName);
                            string webRootPath = webHostEnvironment.WebRootPath;// arrive from pc to wwwroot
                            string contentRootPath = webHostEnvironment.ContentRootPath; //from pc to only project
                            string path = "";
                            path = Path.Combine(webRootPath, "img\\Uploads\\Photos", fileName);
                            //or path = Path.Combine(contentRootPath, "wwwroot", "CSS");
                            using (var fileStream = new FileStream(path, FileMode.Append))
                            {
                                  file.CopyToAsync(fileStream); 
                            } 
                               return Json(new { success = true, imageURL = path, imagename = fileName, responseText = "wellcom" });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message });
                } 
            }
            return Json(new { Success = false, Message = "there is no file send " });
        } 
        public IActionResult OnPostMyUploader(IFormFile MyUploader)
        {
            if (MyUploader != null)
            {
                //string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "mediaUpload");
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img\\Uploads\\Photos");
                string filePath = Path.Combine(uploadsFolder, MyUploader.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    MyUploader.CopyTo(fileStream);
                }
                return new ObjectResult(new { success = true, imageURL = filePath, imagename = MyUploader.FileName, });
            }
            return new ObjectResult(new { status = "fail" });

        } 
        //public async Task<JsonResult> moveimg(IFormFile file)
        //{
        //    try
        //    {
        //        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        //        var path = Path.Combine(HttpContext.Current.Server.MapPath()("~/content/images/"), fileName);
        //        string webRootPath = webHostEnvironment.WebRootPath;// arrive from pc to wwwroot
        //        string contentRootPath = webHostEnvironment.ContentRootPath; //from pc to only project
        //        string path = "";
        //        path = Path.Combine(webRootPath, "img\\Uploads\\Photos", fileName);
        //        or path = Path.Combine(contentRootPath, "wwwroot", "CSS");
        //        using (var fileStream = new FileStream(path, FileMode.Create))
        //        {
        //            Task task = file.CopyToAsync(fileStream);
        //        }
        //        Task.Run
        //        return Json(new { success = true, imageURL = path, imagename = fileName, responseText = "wellcom" });
        //    }
        //    catch (Exception ex)
        //    {

        //        return Json(new { Success = false, Message = ex.Message });
        //    }
        //}

        //[HttpGet]
        //public void  index()
        //{ 
        //}

        [HttpPost]
        public async Task<IActionResult> UploadLogo([FromForm] IFormFile file)
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

        public static string UplodFile(string folderName, IFormFile file)
        {
            try
            {
                // 1 => Get Directory
                // Directory.GetCurrentDirectory() => replace the folder path on the server
                string filePath = Directory.GetCurrentDirectory() + "/wwwroot/files/" + folderName;
                // 2 => Get File Name
                //string cvName = model.Cv.FileName;
                // Some browser send the file name with token to make the name unique
                // So to remove this token and get the file name separate
                // We use [Path.GetFileName(model.Cv.FileName)] => remove any extras added to the file name
                // To make the file name unique we will concat [Guid.NewGuid()] with the file name
                // [Guid.NewGuid()] => It is a unique Id contains 36 digit
                // We will concat it from the left side to avoid override the file extension
                string fileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
                // 3 => Merge Path with File Name
                //string cvFinalPath = cvPath + cvName;
                // To avoid forget any character into the URL
                // We use [Path.Combine()] function
                // [Path.Combine()] => It makes concat between two URLs and put [/] between them if not exist
                string fileFinalPath = Path.Combine(filePath, fileName);
                // 4 => Save File As Streams [Data Overtime]
                using (var Stream = new FileStream(fileFinalPath, FileMode.Create))
                {
                    file.CopyTo(Stream);
                }
                return fileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string DeleteFile(string folderName, string fileName)
        {
            try
            {
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "wwwroot/files/" + folderName + fileName))
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "wwwroot/files/" + folderName + fileName);
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
