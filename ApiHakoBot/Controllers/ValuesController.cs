using ApiHakoBot.Context;
using ApiHakoBot.Entities;
using ApiHakoBot.Repository;
using ApiHakoBot.Tools;
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
        private HakoBotDbContext db = new HakoBotDbContext();
        private static string fromUser = "DirectLineClientSampleUser";
        Converter converter = new Converter();

        [HttpGet]
        public List<string> GetAnswer(string question)
        {
            
            QuestionRepository<Question> questionRepository = new QuestionRepository<Question>(db);


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
                rep = "I'm sorry but i can't answer your question. But i'm working on it ! Try again later.";
                
                questionRepository.Add(converter.StringToQuestionType(question));
                db.SaveChanges();
            }

            char[] delimiterChars = { '$' };
            var returnlist = rep.Split(delimiterChars).ToList();
            

            return returnlist;

        }
    }
}
