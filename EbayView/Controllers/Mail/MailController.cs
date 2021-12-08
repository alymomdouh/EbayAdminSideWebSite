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
        public MailController(IMailServices _mailServices)
        {
            mailServices = _mailServices;
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
        [HttpPost("welcome")]
        public async Task<IActionResult> SendWelcomeEmail([FromBody] WelcomeRequest dto)
        {
            var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\WelcomeTemplate.html";
            var str = new StreamReader(filePath);

            var mailText = str.ReadToEnd();
            str.Close();

            mailText = mailText.Replace("[username]", dto.UserName).Replace("[email]", dto.Email);

            await mailServices.SendEmailAsync(dto.Email, "Welcome to our channel", mailText);
            return Ok();
        }
    }
}
