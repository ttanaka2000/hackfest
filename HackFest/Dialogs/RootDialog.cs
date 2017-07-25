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

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            // return our reply to the user
            context.Call(FormDialog.FromForm(SandwichOrder.BuildForm, FormOptions.PromptInStart), this.ReturnFromSandwitchForm);

        }

        public async Task ReturnFromSandwitchForm(IDialogContext context, IAwaitable<object> result)
        {
            string[] array = new string[2];
            array[0] = "001";
            array[1] = "001";
            string stringJson = AccessEdiDb.AuthVendorId(array);
            await context.PostAsync(stringJson);

            context.Wait(this.MessageReceivedAsync);
        }


    }





}