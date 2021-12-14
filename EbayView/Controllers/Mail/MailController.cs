using EbayView.Models.ViewModel.Users;
using EbayView.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EbayView.Controllers.Mail
{
    public class MailController : Controller
    {
        private readonly IMailServices mailServices;
        private static  GetUsserDetailsOutputModel user;
        public MailController(IMailServices _mailServices)
        {
            mailServices = _mailServices;
        }
        public  IActionResult index(GetUsserDetailsOutputModel userinfo)
        {
            //@model EbayView.Models.ViewModel.Users.GetUsserDetailsOutputModel
            user = userinfo;
            return View(userinfo);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            try
            {
                await mailServices.SendEmailAsync(request.ToEmail, request.Subject, request.Body, request.Attachments);
                return Ok();
            }
            catch (Exception ex)
            {
                throw; 
            } 
        }
        //[HttpPost("welcome")]
        //public async Task<IActionResult> SendWelcomeEmail([FromBody] WelcomeRequest dto)
        public async Task<IActionResult> SendWelcomeEmail()
        {
            var filePath = $"{Directory.GetCurrentDirectory()}\\Views\\Mail\\WelcomeTemplate.html";
            var str = new StreamReader(filePath);

            var mailText = str.ReadToEnd();
            str.Close(); 
            mailText = mailText.Replace("[username]", user.UserName).Replace("[email]", user.Email)
                .Replace("[FistName]", user.FistName).Replace("[LastName]", user.LastName);

            await mailServices.SendEmailAsync(user.Email, "Welcome to our ecommerce website ", mailText);
            //return Ok();
            return RedirectToAction("index",user.UserId);
        }
    }
}
