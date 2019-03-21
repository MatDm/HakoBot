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
    [Route("api/Mail")]
    public class MailController : Controller
    {
        private SmtpClient smtpClient;
        public MailController(SmtpClient smtpClient)
        {
            this.smtpClient = smtpClient;
        }


        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await smtpClient.SendMailAsync(new MailMessage(
                from : "hakobot27@gmail.com",
                to: "hakobot27@gmail.com",
                subject: "Test message subject",
                body: "Test message body"
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