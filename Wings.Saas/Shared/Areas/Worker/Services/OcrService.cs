using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wings.Saas.Shared.Areas.Common.Dtos;
using Wings.Saas.Shared.Areas.Common.Services;
using Wings.Saas.Shared.Areas.Worker.Dtos;

namespace Wings.Saas.Shared.Areas.Worker.Services
{
   public class OcrTaskService
    {
        private HttpService _httpService { get; set; }
        public OcrTaskService(HttpService httpService) { _httpService = httpService; }

        public async Task<Res<List<OcrTaskOutput>>> Load()
        {

            return await _httpService.Get<Res<List<OcrTaskOutput>>>("/api/Worker/OcrTask/Load");
        }




        public async Task<Res<object>> Add(OcrTaskOutput input)
        {
            return await _httpService.PostJson<Res<object>>("/api/Worker/OcrTask/Add", input);
        }
        public async Task<Res<object>> Update(OcrTaskOutput input)
        {
            return await _httpService.PostJson<Res<object>>("/api/Worker/OcrTask/Update", input);
        }
        public async Task<Res<object>> Remove(int id)
        {
            return await _httpService.Get<Res<object>>("/api/Worker/OcrTask/Remove?id=" + id);
        }

    }
}
