using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wings.Saas.Shared.Areas.Account.Services;
using Wings.Saas.Shared.Areas.Common.Services;
using Wings.Saas.Shared.Areas.Worker.Services;

namespace Wings.Saas.Client
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("app");

      builder.Services.AddAntDesign();
      builder.Services.AddHttpClient();
      builder.Services.AddScoped<HttpService>();
      builder.Services.AddScoped<AccountService>();
      builder.Services.AddScoped<WorkerService>();
      builder.Services.AddScoped<OcrTaskService>();
      builder.Services.AddScoped<KeySecretService>();


            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

      await builder.Build().RunAsync();
    }
  }
}