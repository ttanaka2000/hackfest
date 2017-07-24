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

        //質問項目と回答
        public enum SandwichOptions
        {
            RoastBeef, BLT, SubwayClub, RoastChicken,
            TeriyakiChicken, TurkeyBreast, Ham, Tuna, VeggieDelite
        }
        public enum LengthOptions
        {
            Regular, Footlong
        }
        public enum BreadOptions
        {
            Flatbread, White, Wheat, Sesame, HoneyOats
        }
        public enum ToppingsOptions
        {
            SliceCheese, CreamCheese, Bacon, Tuna, Avocado, Jalapeno, None
        }
        public enum VegetableLessOptions
        {
            All, Lettuce, Tomato, Pement, Onion, Pickles, Olives, None
        }
        public enum VegetableMoreOptions
        {
            All, Lettuce, Tomato, Pement, Onion, Pickles, Olives, None
        }
        public enum SauseOptions
        {
            Caesar, HoneyMustard, WasabiSoy, Basil, Balsamico, Mayonnaise, OilVinegar
        }


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


        [Serializable]
        public class SandwichOrder
        {
            [Prompt("{&}をひとつお選びください{||}")]
            public SandwichOptions? サンドイッチの種類;
            public LengthOptions? サイズ;
            public BreadOptions? パンの種類;
            [Prompt("{&}(複数選択もOK)をお選びください{||}")]
            public List<ToppingsOptions> 追加するオプション;
            public List<VegetableLessOptions> 抜きたい野菜;
            public List<VegetableMoreOptions> 増やしたい野菜;
            public List<SauseOptions> ソース;

            public static IForm<SandwichOrder> BuildForm()
            {
                return new FormBuilder<SandwichOrder>()
                    .Message("こんにちは! サンドウィッチショップです。ご注文を承ります！")
                    .Field(nameof(サンドイッチの種類))
                    .Field(nameof(サイズ))
                    .Field(nameof(パンの種類))
                    .Field(nameof(追加するオプション))
                    .Field(nameof(抜きたい野菜))
                    .Field(nameof(増やしたい野菜))
                    .Field(nameof(ソース))
                    .Confirm("注文はこちらでよろしいでしょうか？ (宜しければ 1:はい, 変更する場合は 2:いいえ を送信してください) ----- {サンドイッチの種類}、{サイズ}サイズ＆{パンの種類} (追加オプション:{追加するオプション}、野菜抜き:{抜きたい野菜}、野菜増量:{増やしたい野菜}、{ソース}ソース)")
                    .Message("ご注文完了です。")
                    .Build();
            }

          }

        internal static IDialog<SandwichOrder> MakeRootDialog()
        {
            return Chain.From(() =>
                FormDialog.FromForm(SandwichOrder.BuildForm));
        }

            public virtual async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity != null)
            {
                if (activity.GetActivityType() == ActivityTypes.Message)
                {
                    await Conversation.SendAsync(activity, MakeRootDialog);
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