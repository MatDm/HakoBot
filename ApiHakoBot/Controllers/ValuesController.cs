using ApiHakoBot.SendMail;
using Microsoft.Bot.Connector.DirectLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiHakoBot.Controllers
{
    
    public class ValuesController : ApiController
    {
        private static string directLineSecret = "6Lkm0P_iA38.8Oq485y1rht4t0Q_nCSIdOIyfSoguKXiS6Bu2IBGqkM";
        private static string botId = "test3botsample";

        private static string fromUser = "DirectLineClientSampleUser";

        [HttpGet]
        public List<string> GetAnswer(string question)
        {

            DirectLineClient client = new DirectLineClient(directLineSecret);
            Conversation conversation = client.Conversations.StartConversation();

            Activity message = new Activity
            {
                From = new ChannelAccount(fromUser),
                Text = question,
                Type = ActivityTypes.Message
            };

            client.Conversations.PostActivity(conversation.ConversationId, message);

            var msg = client.Conversations.GetActivities(conversation.ConversationId);

            var msgT = from m in msg.Activities
                       where m.From.Id == botId
                       select m;

            var rep = msgT.Last().Text;

            string badrequest = "No QnA Maker answers were found. This example uses a QnA Maker Knowledge Base that focuses on smart light bulbs. \r\n                                To see QnA Maker in action, ask the bot questions like 'Why won't it turn on?' or 'I need help'.";

            if (rep == badrequest)
            {
                rep = "I'm sorry but i can't answer your question. But i'm working on it ! Try again later. If you want to send your question by mail to be reviewed type '' @mail : Your Question. ''";
            }

            if(question.StartsWith("@mail"))
            {
                //SendQuestion sendQuestion = new SendQuestion();
                //sendQuestion.PostRequest(question, "https://smtphakobot2.azurewebsites.net/mail/get");
                rep = "Your question has been sent by mail to our support and will be reviewed very soon !";
                var sendmail = new WebClient();
                var content = sendmail.DownloadString("https://smtphakobot2.azurewebsites.net/mail/get?message="+question);
            }

            char[] delimiterChars = { '$' };
            var returnlist = rep.Split(delimiterChars).ToList();

            return returnlist;

        }
    }
}
