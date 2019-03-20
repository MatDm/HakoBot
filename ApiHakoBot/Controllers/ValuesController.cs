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

        // This gives a name to the bot user.
        private static string fromUser = "DirectLineClientSampleUser";


        [HttpGet]
        public List<string> GetAnswer(string question)
        {
            //Ajouter gros if formulaire pour detecter si on se trouve dans un chat bot ou le remplissage d'un formulaire
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

            //var rep = msgT.Last().Text;

            var rep = msgT.Last().Text;
            char[] delimiterChars = { '$' };
            var returnlist = rep.Split(delimiterChars).ToList();

            return returnlist;

            //switch (rep)
            //{
            //    case "No QnA Maker answers were found.":
            //        sendQuestionHp(rep);
            //        rep = "your question has been sent to the helpdesk ";
            //        break;
            //}
            //return rep;
        }
    }
}
