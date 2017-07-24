using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace HackFest
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {   

        ////質問項目と回答
        //public enum SandwichOptions
        //{
        //    RoastBeef, BLT, SubwayClub, RoastChicken,
        //    TeriyakiChicken, TurkeyBreast, Ham, Tuna, VeggieDelite
        //}
        //public enum LengthOptions
        //{
        //    Regular, Footlong
        //}
        //public enum BreadOptions
        //{
        //    Flatbread, White, Wheat, Sesame, HoneyOats
        //}
        //public enum ToppingsOptions
        //{
        //    SliceCheese, CreamCheese, Bacon, Tuna, Avocado, Jalapeno, None
        //}
        //public enum VegetableLessOptions
        //{
        //    All, Lettuce, Tomato, Pement, Onion, Pickles, Olives, None
        //}
        //public enum VegetableMoreOptions
        //{
        //    All, Lettuce, Tomato, Pement, Onion, Pickles, Olives, None
        //}
        //public enum SauseOptions
        //{
        //    Caesar, HoneyMustard, WasabiSoy, Basil, Balsamico, Mayonnaise, OilVinegar
        //}


        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        //public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        //{
        //    if (activity.Type == ActivityTypes.Message)
        //    {
        //        await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
        //    }
        //    else
        //    {
        //        HandleSystemMessage(activity);
        //    }
        //    var response = Request.CreateResponse(HttpStatusCode.OK);
        //    return response;
        //}

        public virtual async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity != null)
            {
                if (activity.GetActivityType() == ActivityTypes.Message)
                {
                    await Conversation.SendAsync(activity, () => new EhocDialog());
                }
                else
                {
                    HandleSystemMessage(activity);
                }

            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);

        }

        [Serializable]
        public class EhocDialog : IDialog<object>
        {

            protected int count = 1;
            public async Task StartAsync(IDialogContext context)
            {
                context.Wait(MessageReceivedAsync);

            }
            public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
            {
                var message = await argument;

                if (message.Text == "reset")
                {
                    PromptDialog.Confirm(context, ResetCountAsync,  "リセットしようか");
                }
                else
                {
                    await context.PostAsync(string.Format("会話{0}個目:{1}って言ったよね。",count++,message.Text));
                    context.Wait(MessageReceivedAsync);
                }


            }
            public async Task ResetCountAsync(IDialogContext context, IAwaitable<bool> argument)
            {
                var confirm = await argument;
                if (confirm)
                {
                    this.count = 1;
                    await context.PostAsync($"リセットしたよ");
                }
                else
                {
                    await context.PostAsync($"やめたよ");
                }
                context.Wait(MessageReceivedAsync);
            }


        }



        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}