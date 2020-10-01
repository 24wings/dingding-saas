using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wings.Saas.Server.Models;
using Wings.Saas.Shared.Areas.Account.Dtos;
using System.Linq;
using Wings.Saas.Shared.Areas.Common;
using Wings.Saas.Shared.Areas.Common.Dtos;

namespace Wings.Saas.Server.Areas.Account
{
  [Route("api/[Controller]/[action]")]
  public class AccountController : ControllerBase
  {
    private readonly ApplicationDbContext _applicationDbContext;
    public AccountController(ApplicationDbContext applicationDbContext) { _applicationDbContext = applicationDbContext; }
    public async Task<Res<LoginOutput>> Login([FromBody] LoginDto input)
    {
      var user = _applicationDbContext.Users.FirstOrDefault(user => user.PassWord == input.Password && user.UserName == input.UserName);
      if (user != null)
      {
        return Res<LoginOutput>.Success<LoginOutput>(new LoginOutput
        {
          UserInfo = new UserInfoOutput
          {
            UserName = user.UserName,
            Id = user.Id
          }
        });
      }
      else
      {
        return Res<LoginOutput>.Error<LoginOutput>(null, "用户名或密码错误");
      }


    }

  }

}