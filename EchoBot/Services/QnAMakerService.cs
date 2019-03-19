using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.AI.QnA;
using Microsoft.Bot.Configuration;

namespace EchoBot.Services
{
    public class QnaMakerServices : IQnaMakerServices
    {
        private const string Name = "QnABot";
        public QnaMakerServices(BotConfiguration config)
        {
            QnaMaker = Init(config);
        }

        public QnAMaker QnaMaker { get; }

        private QnAMaker Init(BotConfiguration config)
        {
            var service = config.Services.FirstOrDefault(s => s.Type == ServiceTypes.QnA && s.Name == Name);
            var qna = (QnAMakerService)service;
            if (qna == null)
            {
                throw new InvalidOperationException("The QnA service is not configured correctly in your '.bot' file.");
            }
            if (string.IsNullOrWhiteSpace(qna.KbId))
            {
                throw new InvalidOperationException("The QnA KnowledgeBaseId ('kbId') is required to run this sample.Please update your '.bot' file.");
            }
            if (string.IsNullOrWhiteSpace(qna.EndpointKey))
            {
                throw new InvalidOperationException("The QnA EndpointKey ('endpointKey') is required to run this sample.Please update your '.bot' file.");
            }
            if (string.IsNullOrWhiteSpace(qna.Hostname))
            {
                throw new InvalidOperationException("The QnA Host ('hostname') is required to run this sample.Please update your '.bot' file.");
            }
            var qnaEndpoint = new QnAMakerEndpoint()
            {
                KnowledgeBaseId = qna.KbId,
                EndpointKey = qna.EndpointKey,
                Host = qna.Hostname,
            };
            var qnaMaker = new QnAMaker(qnaEndpoint);
            return qnaMaker;
        }
    }
}
