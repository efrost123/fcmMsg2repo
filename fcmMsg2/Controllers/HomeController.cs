using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using fcmMsg2.Helpers;
using System.Threading.Tasks;

namespace fcmMsg2.Controllers
{
    public class HomeController : Controller
    {
        private IFcm fcm;

        public HomeController(IFcm fcm)
        {
            this.fcm = fcm;
        }

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> sendMessage(string messageText, bool includeData = false)
        {
            var result = await fcm.MessageNotificationAsync("efrost", "Custom title", messageText, includeData);

            if (result == true)
                return View();

            return (View("ErrorSending"));

            /*WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAA62v12yw:APA91bGvb_DW-FZJeGl_3TT9IpTj0mWXH0CmdTVfhsSScXFg6hylSiYMnq3xqn_Iy_sgCbzz6r8IonxHovV34I994kn2L2REjJ60-9TxEhmnCb7DrS6SJYsO5bmekdCwPZqUSoDkF9y6"));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "1011128589100"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = "fae9khXaGrg:APA91bGRthK7GYvZ7Vd5dh-QZFsOJJtYXk1RTsYJbPueNIOu44is2bpr7qsBh8P-FnngcQUBZiUJ44BZM5a0mzkMN99rvsy4eUxds5T4gnIrfEJByWV3KjyxhktEQ_8jBXnFQeujpLs-",
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = messageText,
                    title = "Test",
                    badge = 1
                },
            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }

            return View();*/
        }
    }
}