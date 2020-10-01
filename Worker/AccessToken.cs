using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Worker
{
    public static class AccessToken

    {
        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
        // 返回token示例
        public static String TOKEN = "24.adda70c11b9786206253ddb70affdc46.2592000.1493524354.282335-1234567";

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static String clientId = "iMsxqdYtb02hfpRDZgcjF5cG";
        // 百度云中开通对应服务应用的 Secret Key
        private static String clientSecret = "PbYgYul8CruPbTdK1H01aWmvpeVxuCBQ";
        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public static String getAccessToken(string cId, string cSecret)
        {
            var authHost = "https://aip.baidubce.com/oauth/2.0/token";
            var client = new HttpClient();
            var paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", cId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", cSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            return result;
        }
    }

}
