using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
namespace HackFest.Forms
{
    //質問項目と回答
    //小売りコードがあっているか
    public enum RetailCode
    {
        あっている, 間違っている
    }

    [Serializable]
    public class SandwichOrder
    {
        [Prompt("{&}をひとつお選びください{||}")]
        //小売りコードがあっているか
        public RetailCode? 小売りコード;

        public static IForm<SandwichOrder> BuildForm()
        {
            return new FormBuilder<SandwichOrder>()
                //.Message("こんにちは! サンドウィッチショップです。ご注文を承ります！")
                .Field(nameof(小売りコード))
                .Build();
        }
    }
}