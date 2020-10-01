using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Wings.Saas.Shared;
using Wings.Saas.Shared.Areas.Worker.Dtos;

namespace Worker
{
    

        public class WordResult
        {

            public string words { get; set; }



        }
        public class CommonOcrResult
        {
            public long log_id { get; set; }
            public int words_result_num { get; set; }
            public List<WordResult> words_result { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int status { get; set; }
            public bool isCanceld { get; set; }
            public int id { get; set; }

        }

        /// <summary>
        /// 
        /// </summary>
        public class AccessTokenReponse
        {
            /// <summary>
            /// 
            /// </summary>
            public string access_token { get; set; }

        }

        public class OcrService
        {

            /// <summary>
            /// 获取token
            /// </summary>
            /// <param name="cid"></param>
            /// <param name="cSecret"></param>
            /// <returns></returns>
            public string GetToken(string cid, string cSecret)
            {

                return AccessToken.getAccessToken(cid, cSecret);
            }

            public async Task<TaskItem> Parse(TaskItem taskItem)
            {
                var tokenStr = GetToken(taskItem.Key, taskItem.Secret);
                var rtn = JsonConvert.DeserializeObject<AccessTokenReponse>(tokenStr);
                var token = rtn.access_token;
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post,
                    "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic" + "?access_token=" + token);
                request.Headers.Add("ContentType", "application/x-www-form-urlencoded");
                request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("url", "http://dingding1234.airuanjian.vip/" + taskItem.No + ".png"),
                new KeyValuePair<string, string>("language_type", "CHN_ENG"),
            });
                var rtn2 = client.SendAsync(request);
                var str = await rtn2.Result.Content.ReadAsStringAsync();
                var commonOcrResponse = JsonConvert.DeserializeObject<CommonOcrResult>(str);

                if (commonOcrResponse.words_result == null) return null;
                if (commonOcrResponse.words_result.Count > 0)
                {
                    taskItem.OcrResult = commonOcrResponse.words_result.Last().words;
                }
                return taskItem;

            }
        }
    

}
