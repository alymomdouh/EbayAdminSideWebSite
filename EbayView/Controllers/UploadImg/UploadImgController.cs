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
using Azure.Storage.Blobs;
using EbayView.Services;
using Microsoft.Extensions.Options;
//using Microsoft.Azure.Storage;
//using Microsoft.Azure.Storage.Blob;
//using System.Web.Mvc;
//using System.Web.HttpContext.Current;
//using System.Web.Optimization;

namespace EbayView.Controllers.UploadImg
{
    public class UploadImgController : Controller //Microsoft.AspNetCore.Mvc.Controller
    {   // to run add services.AddSingleton<UploadImgController>();
        //static string connectionstring =  "DefaultEndpointsProtocol=https;AccountName=itiprojectuploadingimgs;AccountKey=GF2Yq/tvZe+J7bUExxjRVJ6Qa319TUo3AsbsSY1kfSC4HlyykU4cShguZsnspbjMACFSQYy3QXbq+4Yr5PfgJA==;EndpointSuffix=core.windows.net";
        //static string containerName = "ProductsImages";

        private readonly AzureStorage azureStorage;
         
        private readonly IWebHostEnvironment webHostEnvironment;
        [Obsolete]
        private IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public UploadImgController(IWebHostEnvironment _webHostEnvironment, IHostingEnvironment hostingEnvironment,
            IOptions<AzureStorage> _azureStorage)
        {
             webHostEnvironment = _webHostEnvironment;
            _hostingEnvironment = hostingEnvironment;
            azureStorage = _azureStorage.Value;
        } 
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> UploadLogo(IFormFile file)
        {
            var fileName = "";
            var fullPath = "";
            var webRootPath="";
            try
            {
                if (file.Length > 0)
                {
                    //string webRootPath = _hostingEnvironment.WebRootPath + "\\ProfileImages\\";
                      webRootPath = _hostingEnvironment.WebRootPath + "\\img\\Uploads\\Photos";
                    fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    fullPath = Path.Combine(webRootPath, fileName); 
                    using (FileStream fileStream = System.IO.File.Create(fullPath))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                    }
                }
                await UploadToAzureAsync( file);
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
        private async Task<OkResult> UploadToAzureAsync(IFormFile file)
        {
            //var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient(); 
            //var cloudBlobContainer = cloudBlobClient.GetContainerReference("Productsimages"); 
            //if (await cloudBlobContainer.CreateIfNotExistsAsync())
            //{
            //    await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off });
            //} 
            //var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(file.FileName);
            //cloudBlockBlob.Properties.ContentType = file.ContentType; 
            //await cloudBlockBlob.UploadFromStreamAsync(file.OpenReadStream());

            BlobServiceClient blobServiceClient = new BlobServiceClient(azureStorage.ConnectionString);
            BlobContainerClient containerClient;
            if (blobServiceClient.GetBlobContainerClient(azureStorage.ContainerName) !=null)
            {
                containerClient = blobServiceClient.GetBlobContainerClient(azureStorage.ContainerName);
            } else
            {
                containerClient = blobServiceClient.CreateBlobContainer(azureStorage.ContainerName);
            }
            //var myfiles = Directory.GetFiles(filepath);
            //StreamReader streamReader = new StreamReader(file.OpenReadStream());
            //containerClient.UploadBlob(file.FileName,streamReader.BaseStream,);
            //using (MemoryStream stream = new MemoryStream( System.IO.File.ReadAllBytes(filepath)))
            //{
            //     containerClient.UploadBlob(fileName,stream);
            //}
            //Stream outStream = new MemoryStream();
            //var cloudBlockBlob = containerClient.GetBlockBlobReference(file.FileName);
            //cloudBlockBlob.Properties.ContentType = file.ContentType; 
            //await cloudBlockBlob.UploadFromStreamAsync(file.OpenReadStream());

            using (MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(file.Name)))
            {
                containerClient.UploadBlob(file.Name, stream);
            }

            return Ok();
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
