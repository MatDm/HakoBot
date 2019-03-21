using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SMTPHakoBot.Controllers
{
    [Produces("application/json")]
    [Route("mail")]
    public class MailController : Controller
    {
        private SmtpClient smtpClient;
        public MailController(SmtpClient smtpClient)
        {
            this.smtpClient = smtpClient;
        }



        [Route("get")]
        public async Task<IActionResult> Get(string message)
        {
            await smtpClient.SendMailAsync(new MailMessage(
                from: "hakobot27@gmail.com",
                to: "hakobot27@gmail.com",
                subject: "NEW QUESTION",
                body: message
                ));

            return Ok("OK");
        }
         

        protected override void Dispose(bool disposing)
        {
            smtpClient.Dispose();
            base.Dispose(disposing);
        }
    }
}