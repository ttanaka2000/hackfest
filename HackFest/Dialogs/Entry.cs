using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using HackFest.Forms;

namespace HackFest
{
    [Serializable]
    public class Entry : IDialog<object>
    {
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            // return our reply to the user
            var activity = await result;
            while (activity.Equals("間違っている"))
            {
                context.Call(FormDialog.FromForm(EntryCode.BuildForm, FormOptions.PromptInStart), this.MessageReceivedAsync);
            }

            context.Call(FormDialog.FromForm(EntryCode.BuildForm, FormOptions.PromptInStart), this.MessageReceivedAsync2);
        }

        private async Task MessageReceivedAsync2(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result;
            while (activity.Equals("間違っている"))
            {
                context.Call(FormDialog.FromForm(EntryCode.BuildForm, FormOptions.PromptInStart), this.MessageReceivedAsync);
            }
            context.Call(FormDialog.FromForm(EntryCode.BuildForm2, FormOptions.PromptInStart), this.ReturnFromEntryCodeForm);
        }

        public async Task ReturnFromEntryCodeForm(IDialogContext context, IAwaitable<object> result)
        {
            var customer = await result;
            await context.PostAsync($"メニューに遷移します。");
            context.Wait(this.MessageReceivedAsync);
        }
    }
}