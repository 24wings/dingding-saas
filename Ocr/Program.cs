using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ocr.Services;

namespace Ocr
{

   



    public class CommonOcrRespnse
    {
        /// <summary>
        /// 结果
        /// </summary>
        public CommonOcrResult result { get; set; }
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
    public class WordResult
    {

        public string words { get; set; }



    }



    /// <summary>
    /// ocr识别结果
    /// </summary>
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
    class TaskData
    {
        public int start { get; set; }
        public int end { get; set; }
        public string key { get; set; }
        public string secret { get; set; }
        public string taskName { get; set; }
    }

    class Program
    {

        DatabaseContext dbContext { get;  } = new DatabaseContext();
        public static string OcrCorpAfterFix { get; set; } = "?x-oss-process=image/crop,x_50,y_150,w_300,h_100";

        static void Main(string[] args)
        {


            TaskData data;
            if (args.Length > 0)
            {
            
                  data=  JsonConvert.DeserializeObject<TaskData>(File.ReadAllText(args.First()));



            }
            else
            {
              data=  JsonConvert.DeserializeObject<TaskData>(File.ReadAllText("task1.json"));

            }

           



            Console.WriteLine("Hello World!");
            var ocr = new Program();
            var tokenStr = getToken(data.key, data.secret);
            var rtn = JsonConvert.DeserializeObject<AccessTokenReponse>(tokenStr);
            var token = rtn.access_token;
            ocr.parseAll(token, data.start, data.end,data.taskName).ContinueWith(rtn =>
            {
                Console.WriteLine(rtn);
                
                  //var newGroup = new Group() { no = i, title = String.Join(",", rtn.Result.words_result.Select(r => r.words).ToList()) };


                return rtn;

            });
            Console.ReadLine();

        }


//        public async Task<object> parse(string cid, string cSecret)
//        {

//            var tokenStr = this.getToken(cid, cSecret);
//            var rtn = JsonConvert.DeserializeObject<AccessTokenReponse>(tokenStr);
//            var token = rtn.access_token;
//            var client = new HttpClient();
//            var request = new HttpRequestMessage(HttpMethod.Post, "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic" + "?access_token=" + token);
//            request.Headers.Add("ContentType", "application/x-www-form-urlencoded");
//            request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>() {
//            new KeyValuePair<string,string>("url", "http://dingding1314.oss-cn-beijing.aliyuncs.com/10000003.png?x-oss-process=image/crop,x_50,y_150,w_300,h_100"),
//new KeyValuePair<string,string>("language_type", "CHN_ENG"),
//            });
//            var rtn2 = await client.SendAsync(request);
//            var commonOcrResponse = JsonConvert.DeserializeObject<CommonOcrRespnse>(await rtn2.Content.ReadAsStringAsync());
//            return commonOcrResponse;
//        }


        public static string getToken(string cid, string cSecret)
        {

            return AccessToken.getAccessToken(cid, cSecret);
        }





        public async Task<bool> parseAll(string token, int start = 2336473, int end = 23400000,string taskName="test")
        {

            var now = DateTime.Now;
            var client = new HttpClient();
            var result = new List<Group>();
            if (start < end)
            {
              var data=  new List<int>();
                for (var i = start; i < end; i++)
                {
                    data.Add(i);
                }
                data.AsParallel().ForAll(async i =>
                {
                    Group newGroup;
                    
                        var request = new HttpRequestMessage(HttpMethod.Post, "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic" + "?access_token=" + token);
                        request.Headers.Add("ContentType", "application/x-www-form-urlencoded");
                        var url = $"http://dingding1314.oss-cn-beijing.aliyuncs.com/{i}.png?x-oss-process=image/crop,x_50,y_280,w_250,h_200";
                        request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>() {
            new KeyValuePair<string,string>("url", url),
new KeyValuePair<string,string>("language_type", "CHN_ENG")
            });
                        var rtn2 = client.SendAsync(request);
                        var str = await rtn2.Result.Content.ReadAsStringAsync();
                        var commonOcrResponse = JsonConvert.DeserializeObject<CommonOcrResult>(str);
                        if (commonOcrResponse.words_result != null)
                        {
                            if (commonOcrResponse.words_result.Count > 0)
                            {
                                 newGroup = new Group() { no = i, title = String.Join(",", commonOcrResponse.words_result.Select(r => r.words).ToList()) };
                                //this.databaseContext.groups.Add(newGroup);
                                //await  dbContext.groups.AddAsync(newGroup);
                               // await File.AppendAllTextAsync(taskName + ".json", JsonConvert.SerializeObject(newGroup));
                                try
                                {
                                    //await dbContext.SaveChangesAsync();

                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }

                                Console.WriteLine(newGroup);
                            }
                            else
                            {
                                 newGroup = new Group() { no = i, title = "暂无结果" };
                                //this.databaseContext.groups.Add(newGroup);
                                //await  dbContext.groups.AddAsync(newGroup);
                              //  await File.AppendAllTextAsync(taskName + ".json", JsonConvert.SerializeObject(newGroup));
                            }
                            //this.databaseContext.SaveChanges();
                        }
                        else
                        {
                             newGroup = new Group() { no = i, title = "暂无结果" };
                            //this.databaseContext.groups.Add(newGroup);
                            //await  dbContext.groups.AddAsync(newGroup);
                        }
                    result.Add(newGroup);
                    if (result.Count == 100)
                    {
                        var exsitResult = result.Take(100).Select(item => new Group() { id = item.id, no = item.no, title = item.title, createAt = DateTime.Now });
                        await File.AppendAllTextAsync(taskName + ".json", JsonConvert.SerializeObject(exsitResult));

                        result = result.Skip(100).ToList();
                    }
              
                    

                    Console.WriteLine(DateTime.Now - now);
                        Console.WriteLine("start:" + start + ",current:" + i + ",end:" + end+",result count:"+result.Count);

                    
                });

                    //for (var i = start; i < end; i++)
                
                return true;
            }
            else
            {
                return false;
            }
        }



    
}


}
