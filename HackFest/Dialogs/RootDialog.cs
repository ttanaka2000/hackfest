using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;
using HackFest.Forms;
using HackFest.Utilitys;

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
        public enum Qaregst
        {
            初めて,初めてではない
        }
        [Serializable]
        public class  Regist
        {
            [Prompt("{&}を行う項目を選んでください{ll}")]
            public Qaregst

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            // return our reply to the user
            context.Call(FormDialog.FromForm(EntryRetailCode.BuildForm, FormOptions.PromptInStart), this.ReturnFromSandwitchForm);
            //１、呼ぶダイアログ　２、呼んだあとどこにとばすか
        }

        public async Task ReturnFromSandwitchForm(IDialogContext context, IAwaitable<object> result)
        {
            var customer = await result;
            await context.PostAsync($"メニューに遷移します。");
            string[] array = new string[2];
            array[0] = "001";
            array[1] = "001";
            string stringJson = AccessEdiDb.AuthVendorId(array);
            await context.PostAsync(stringJson);

            context.Wait(this.MessageReceivedAsync);
        }


    }





}