// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using EchoBot.Services;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace EchoBot
{
    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service.  Transient lifetime services are created
    /// each time they're requested. For each Activity received, a new instance of this
    /// class is created. Objects that are expensive to construct, or have a lifetime
    /// beyond the single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public class HakoBotBot : IBot
    {
        private readonly HakoBot _accessors;
        private readonly ILogger _logger;
        private readonly IQnaMakerServices _qnaMakerServices;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="conversationState">The managed conversation state.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory"/> that is hooked to the Azure App Service provider.</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1#windows-eventlog-provider"/>
        public HakoBotBot(ConversationState conversationState, ILoggerFactory loggerFactory, IQnaMakerServices qnaMakerServices)
        {
            _qnaMakerServices = qnaMakerServices;
            if (conversationState == null)
            {
                throw new System.ArgumentNullException(nameof(conversationState));
            }

            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _accessors = new HakoBot(conversationState)
            {
                CounterState = conversationState.CreateProperty<CounterState>(HakoBot.CounterStateName),
            };

            _logger = loggerFactory.CreateLogger<HakoBotBot>();
            _logger.LogTrace("Turn start.");
        }

        /// <summary>
        /// Every conversation turn for our Echo Bot will call this method.
        /// There are no dialogs used, since it's "single turn" processing, meaning a single
        /// request and response.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>
        /// <seealso cref="IMiddleware"/>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                var response = await _qnaMakerServices.QnaMaker.GetAnswersAsync(turnContext);
                if (response != null && response.Length > 0)
                {
                    await turnContext.SendActivityAsync(response[0].Answer);
                    //await turnContext.SendActivityAsync("seconde ligne"); 
                    //using (var client = new HttpClient())
                    //{
                    //    client.BaseAddress = new Uri("https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/164eea70-ddab-473c-8563-36fe74e8c196?verbose=true&timezoneOffset=-360&subscription-key=0a43c2db650042eebeb818a7416369ad&q=");

                    //    client.DefaultRequestHeaders.Accept.Clear();
                    //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //    await turnContext.SendActivityAsync(response[0].Answer);
                    //}
                }
                else
                {
                    var msg = @"No QnA Maker answers were found. This example uses a QnA Maker Knowledge Base that focuses on smart light bulbs. 
                                To see QnA Maker in action, ask the bot questions like 'Why won't it turn on?' or 'I need help'.";
                    await turnContext.SendActivityAsync(msg);
                }

            }
            else
            {
                //await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            }
        }
    }
}
