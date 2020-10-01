using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PInvoke;
using Wings.Saas.Shared.Areas.Worker.Dtos;

namespace Worker
{


    class Config
    {
        public bool ocr { get; set; }
        public bool scan { get; set; }
    }
    class Program
    {
        readonly static string Ip = "http://60.205.231.166:5001";
        //readonly static string Ip = "http://localhost:5000";
        static async Task Main(string[] args)
        {
            try
            {
                await Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Thread.Sleep(2 * 60 * 1000);
                Start();
            }
            Console.ReadLine();
        }

        public static async Task Start()
        {
            var configString = await File.ReadAllTextAsync("config.json");
            var config = JsonConvert.DeserializeObject<Config>(configString);
            //ocr
            if (!config.scan)
            {
                var httpClient = new HttpClient();
                while (true)
                {
                    var ocrService = new OcrService();
                    var result = await httpClient.GetAsync(Ip + "/api/OcrTask/GetTask");
                    var resultString = await result.Content.ReadAsStringAsync();
                    if (resultString != null)
                    {
                        var item = JsonConvert.DeserializeObject<TaskItem>(resultString);
                        item = await ocrService.Parse(item);
                        using (HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(item)))
                        {
                            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                            await httpClient.PostAsync(Ip + "/api/OcrTask/PostTask", httpContent);
                        }
                    }
                    else
                    {
                        Thread.Sleep(1000 * 10);
                    }




                }


            }
            //scan
            else
            {
                var httpClient = new HttpClient();
                var scanWorker = new ScanWorker();

                while (true)
                {
                    scanWorker.relogin();
                    Thread.Sleep(5000);
                    ResizeDingdingWindow();
                    var result = await httpClient.GetAsync(Ip + "/api/Worker/ScanTask/GetTask");
                    var data = await result.Content.ReadAsStringAsync();
                    var codes = JsonConvert.DeserializeObject<List<string>>(data);
                    Thread.Sleep(5000);
                    foreach (var code in codes)
                    {
                        scanWorker.doOne(int.Parse(code));
                    }
                    Thread.Sleep(2000);

                }

            }
        }

        public static void ResizeDingdingWindow()
        {
            var d = User32.FindWindow(null, "钉钉");
            User32.SetWindowPos(d, IntPtr.Zero, 0, 0, 1400, 900, User32.SetWindowPosFlags.SWP_SHOWWINDOW);

        }
    }

}
