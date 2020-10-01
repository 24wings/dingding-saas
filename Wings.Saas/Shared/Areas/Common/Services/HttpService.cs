using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wings.Saas.Shared.Areas.Common.Services
{
  public class HttpService
  {
    private HttpClient _httpClient { get; set; }
    public HttpService(HttpClient httpClient) { _httpClient = httpClient; }

    public async Task<T> Get<T>(string uri)
    {
      using (var res = await _httpClient.GetAsync(AppConsts.remoteServiceBaseUrl + uri))
      {
        return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());
      }
    }

    public async Task<T> PostJson<T>(string url, object data)
    {
      var jsonContent = new StringContent(JsonConvert.SerializeObject(data));
      jsonContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
      using (var res = await _httpClient.PostAsync(AppConsts.remoteServiceBaseUrl + url, jsonContent))
      {
        return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());
      }
    }

  }
}