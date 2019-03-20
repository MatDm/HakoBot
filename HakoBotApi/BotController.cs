using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HakoBotApi
{
    public class ValuesController : ApiController
    {
        private SatBotContext db = new SatBotContext();

        private static string directLineSecret = "kawp8LrYm_Y.5Edm8WReUh92v8q-WINnAAiN0BQwXCt-KulY7hXTz5w";
        private static string botId = "SatBott";

        // This gives a name to the bot user.
        private static string fromUser = "DirectLineClientSampleUser";

        public string GetAnswer(string question)
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

            var rep = msgT.Last().Text;

            switch (rep)
            {
                case "No QnA Maker answers were found.":
                    sendQuestionHp(rep);
                    rep = "your question has been sent to the helpdesk ";
                    break;
            }
            return rep;
        }

        private void sendQuestionHp(string rep)
        {
            string mail = "gailletpp@gmail.com";
            QuestionForHP question = new QuestionForHP
            {
                ContactEmail = mail,
                Question = rep
            };
            db.QuestionForHP.Add(question);
            db.SaveChanges();
        }

    }
}