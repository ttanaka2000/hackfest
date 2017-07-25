using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using HackFest.Models;

namespace HackFest.Controllers
{
    public class CustomWebAPIController : ApiController
    {
        [HttpGet]
   
        [Route("api/CustomWebAPI")]
        public async Task<HttpResponseMessage> SendMessage(string uid, string msg)
        {
            try
            {
                if (!string.IsNullOrEmpty(ConversationStarter.fromId))
                {
                    await ConversationStarter.Resume(ConversationStarter.conversationId, ConversationStarter.channelId, uid, msg); 

                    var resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent($"<html><body>メッセージを送信しましたよ。</body></html>", System.Text.Encoding.UTF8, @"text/html");
                    return resp;
                }
                else
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent($"<html><body>最初にBOTに接続し、ユーザーの情報をキャプチャする必要があります。</body></html>", System.Text.Encoding.UTF8, @"text/html");
                    notice.addMsgData("osamum", "Hello world");
                    return resp;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/check")]
        public List<iMsgData> checkMessage(string uId) {
            return notice.enumMsgData(uId);
        }
        
    }
}
