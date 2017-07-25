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
    public class EntryCode
    {
        [Prompt("{&}をひとつお選びください{||}")]
        //小売りコードがあっているか
        public RetailCode? 小売りコード;
        public VendorCode? 取引先コード;

        public static IForm<EntryCode> BuildForm()
        {
            return new FormBuilder<EntryCode>()
                .Field(nameof(小売りコード))
                .Build();

        }
        public static IForm<EntryCode> BuildForm2()
        {
            return new FormBuilder<EntryCode>()
                .Field(nameof(取引先コード))
                .Build();

            //while文（取引先コードがDBに存在しなかったらループする）
            //while()

        }

    }




}