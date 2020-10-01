using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wings.Saas.Shared.Areas.Common.Dtos;
using Wings.Saas.Shared.Areas.Common.Services;
using Wings.Saas.Shared.Areas.Worker.Dtos;

namespace Wings.Saas.Shared.Areas.Worker.Services
{
   public class KeySecretService
    {
        private HttpService _httpService { get; set; }
        public KeySecretService(HttpService httpService) { _httpService = httpService; }

        public async Task<Res<List<KeySecretOutput>>> Load()
        {

            return await _httpService.Get<Res<List<KeySecretOutput>>>("/api/Worker/KeySecret/Load");
        }




        public async Task<Res<object>> Add(KeySecretOutput input)
        {
            return await _httpService.PostJson<Res<object>>("/api/Worker/KeySecret/Add", input);
        }
        public async Task<Res<object>> Update(KeySecretOutput input)
        {
            return await _httpService.PostJson<Res<object>>("/api/Worker/KeySecret/Update", input);
        }
        public async Task<Res<object>> Remove(int id)
        {
            return await _httpService.Get<Res<object>>("/api/Worker/KeySecret/Remove?id=" + id);
        }
    }
}
