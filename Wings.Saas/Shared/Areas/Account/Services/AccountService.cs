using System.Net.Http;
using System.Threading.Tasks;
using Wings.Saas.Shared.Areas.Account.Dtos;
using Wings.Saas.Shared.Areas.Common;
using Wings.Saas.Shared.Areas.Common.Dtos;
using Wings.Saas.Shared.Areas.Common.Services;

namespace Wings.Saas.Shared.Areas.Account.Services

{

  public class AccountService
  {
    private HttpService _httpService { get; set; }
    public AccountService(HttpService httpService) { _httpService = httpService; }

    public async Task<Res<LoginOutput>> Login(string userName, string password)
    {

      return await _httpService.PostJson<Res<LoginOutput>>("/api/Account/Login", new LoginDto { UserName = userName, Password = password });
    }
  }
}