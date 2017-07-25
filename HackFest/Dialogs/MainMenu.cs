using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;


namespace HackFest.Dialogs
{
    public class MainMenu
    {
        //質問項目と回答
        public enum QAOptions
        {
            最新の件数, 問い合わせ, パスワード初期化
        }
        public enum LengthOptions
        {
            使い方に関するQA, 例外処理に対するフォロー要望, 個別カスタマイズに関するQA
        }
        public enum BreadOptions
        {
            仕様, トラブル
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
            [Prompt("{&}を行う項目を選んでください{||}")]
            public QAOptions? 問い合わせ;

            public LengthOptions? 質問;
            public BreadOptions? パンの種類;
            [Prompt("{&}(複数選択もOK)をお選びください{||}")]
            public List<ToppingsOptions> 追加するオプション;
            public List<VegetableLessOptions> 抜きたい野菜;
            public List<VegetableMoreOptions> 増やしたい野菜;
            public List<SauseOptions> ソース;

            public static IForm<SandwichOrder> BuildForm()
            {
                return new FormBuilder<SandwichOrder>()
                    .Message("こんにちは,セゾン情報システムズ様ですね！ ")
                    .Field(nameof(問い合わせ))
                    .Field(nameof(質問))
                    .Field(nameof(パンの種類))
                    .Field(nameof(追加するオプション))
                    .Field(nameof(抜きたい野菜))
                    .Field(nameof(増やしたい野菜))
                    .Field(nameof(ソース))
                    //.Confirm("注文はこちらでよろしいでしょうか？ (宜しければ 1:はい, 変更する場合は 2:いいえ を送信してください) ----- {サンドイッチの種類}、{サイズ}サイズ＆{パンの種類} (追加オプション:{追加するオプション}、野菜抜き:{抜きたい野菜}、野菜増量:{増やしたい野菜}、{ソース}ソース)")
                    .Message("ご注文完了です。")
                    .Build();
            }

        }
    }
}