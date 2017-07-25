using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;
using HackFest.Forms;
using HackFest.Models;

namespace HackFest.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        //質問項目と回答
        public enum Qaregist
        {
            初めて,初めてではない
        }

        [Serializable]
        public class Regist
        {
            [Prompt("{&}を行う項目を選んでください{||}")]
            public Qaregist? 登録;

            public static IForm<Regist> BuildForm()
            {
                return new FormBuilder<Regist>()
                    .Message("こんにちは,セゾン情報システムズ様ですね！ ")
                    .Field(nameof(登録))
                    .Build();
            }

        }

        /*[Serializable]
        public class  Regist
        {
            [Prompt("{&}を行う項目を選んでください{ll}")]
            public Qaregst? 登録;

            public static IForm<Regist> BuildForm()
            {
                return new FormBuilder<Regist>()
                    .Message("こんにちは")
                    .Field(nameof(登録))
                    }
            }*/
        //[Serializable]
        //public class Regist
        //{
        //    [Prompt("{&}を行う項目を選んでください{ll}")]
        //    public Qaregst

        //}
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            // return our reply to the user
            context.Call(FormDialog.FromForm(EntryRetailCode.BuildForm, FormOptions.PromptInStart), this.ReturnFromSandwitchForm);
            //１、呼ぶダイアログ　２、呼んだあとどこにとばすか

            var message = await result;

            //We need to keep this data so we know who to send the message to. Assume this would be stored somewhere, e.g. an Azure Table
            ConversationStarter.toId = message.From.Id;
            ConversationStarter.toName = message.From.Name;
            ConversationStarter.fromId = message.Recipient.Id;
            ConversationStarter.fromName = message.Recipient.Name;
            ConversationStarter.serviceUrl = message.ServiceUrl;
            ConversationStarter.channelId = message.ChannelId;
            ConversationStarter.conversationId = message.Conversation.Id;
        }

        public async Task ReturnFromSandwitchForm(IDialogContext context, IAwaitable<object> result)
        {
            var customer = await result;
            await context.PostAsync($"メニューに遷移します。");

            string[] array = new string[2];
            array[0] = "001";
            array[1] = "001";
            VendorData vendordata = new VendorData(array);
            vendordata.AuthVendorId();
            await context.PostAsync(vendordata.responseJson);

            context.Wait(this.MessageReceivedAsync);
        }


    }
    
}