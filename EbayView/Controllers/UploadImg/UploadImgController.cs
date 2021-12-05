using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using System.IO;
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
        public UploadImgController(IWebHostEnvironment _webHostEnvironment)
        {
             webHostEnvironment = _webHostEnvironment;
        }
        public  JsonResult  UploadImagefun()   //async Task<JsonResult> 
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

         
    }
}
