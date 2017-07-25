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
    public enum VendorCode
    {
        あっている, 間違っている
    }

    [Serializable]
    public class EntryRetailCode
    {
        [Prompt("{&}をひとつお選びください{||}")]
        //小売りコードがあっているか
        public RetailCode? 小売りコード;

        public static IForm<EntryRetailCode> BuildForm()
        {
            return new FormBuilder<EntryRetailCode>()
                //.Message("こんにちは! サンドウィッチショップです。ご注文を承ります！")
                .Field(nameof(小売りコード))
                .Build();
        }
    }
    //小売りコードがあってるかの条件分岐と取引先コードを入力させる

}