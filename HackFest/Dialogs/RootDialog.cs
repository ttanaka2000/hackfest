using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;
using HackFest.Forms;

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
            context.Wait(this.MessageReceivedAsync);
        }


    }





}