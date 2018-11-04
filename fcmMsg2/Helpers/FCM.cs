using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using log4net;

namespace fcmMsg2.Helpers
{
    public class FCM : IFcm
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(FCM));

        private readonly string c_serverKey = "AAAA62v12yw:APA91bGvb_DW-FZJeGl_3TT9IpTj0mWXH0CmdTVfhsSScXFg6hylSiYMnq3xqn_Iy_sgCbzz6r8IonxHovV34I994kn2L2REjJ60-9TxEhmnCb7DrS6SJYsO5bmekdCwPZqUSoDkF9y6";
        private readonly string c_senderId = "1011128589100";

        private readonly Dictionary<string, string> knownClients = new Dictionary<string, string>()
        {
            { "efrost", "fae9khXaGrg:APA91bGRthK7GYvZ7Vd5dh-QZFsOJJtYXk1RTsYJbPueNIOu44is2bpr7qsBh8P-FnngcQUBZiUJ44BZM5a0mzkMN99rvsy4eUxds5T4gnIrfEJByWV3KjyxhktEQ_8jBXnFQeujpLs-" }
        };

        public async Task<bool> MessageNotificationAsync(string to, string title, string body, bool includeData)
        {
            try
            {
                if (knownClients.ContainsKey(to))
                {

                    // Get the server key from FCM console
                    var serverKey = string.Format("key={0}", c_serverKey);

                    // Get the sender id from FCM console
                    var senderId = string.Format("id={0}", c_senderId);

                    var recipient = knownClients[to];

                    var jsonBody = includeData ? GetPayloadJson(recipient, title, body) : GetPayloadNodataJson(recipient, title, body);

                    using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                    {
                        httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                        httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                        httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                        using (var httpClient = new HttpClient())
                        {
                            var result = await httpClient.SendAsync(httpRequest);

                            if (result.IsSuccessStatusCode)
                            {
                                return true;
                            }
                            else
                            {
                                // Use result.StatusCode to handle failure
                                // Your custom error handler here
                                _logger.Error($"Error sending notification. Status Code: {result.StatusCode}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception thrown in Notify Service: {ex}");
            }

            return false;
        }

        private string GetPayloadNodataJson(string recipient, string title, string body)
        {
            var payload = new
            {
                to = recipient,
                priority = "high",
                content_available = true,
                time_to_live = 60,
                notification = new
                {
                    body,
                    title,
                    badge = 1
                }
            };

            // Using Newtonsoft.Json
            return JsonConvert.SerializeObject(payload);
        }

        private string GetPayloadJson(string recipient, string title, string body)
        {
            var payload = new
            {
                to = recipient,
                priority = "high",
                content_available = true,
                time_to_live = 60,
                notification = new
                {
                    body,
                    title,
                    badge = 1
                },
                data = new
                {
                    testKey = "data value 1",
                    testKey2 = "data value 2",
                    testKey3 = "data value 3"
                }
            };

            // Using Newtonsoft.Json
            return JsonConvert.SerializeObject(payload);
        }
    }
}