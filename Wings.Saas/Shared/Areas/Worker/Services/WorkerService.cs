using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Wings.Saas.Shared.Areas.Account.Dtos;
using Wings.Saas.Shared.Areas.Common;
using Wings.Saas.Shared.Areas.Common.Dtos;
using Wings.Saas.Shared.Areas.Common.Services;
using Wings.Saas.Shared.Areas.Worker.Dtos;

namespace Wings.Saas.Shared.Areas.Worker.Services

{

    public class WorkerService
    {
        private HttpService _httpService { get; set; }
        public WorkerService(HttpService httpService) { _httpService = httpService; }

        public async Task<Res<List<ScanTaskOutput>>> Load()
        {

            return await _httpService.Get<Res<List<ScanTaskOutput>>>("/api/Worker/ScanTask/Load");
        }

      


        public async Task<Res<object>> Add(ScanTaskOutput input)
        {
            return await _httpService.PostJson<Res<object>>("/api/Worker/ScanTask/Add", input);
        }
        public async Task<Res<object>> Update(ScanTaskOutput input)
        {
            return await _httpService.PostJson<Res<object>>("/api/Worker/ScanTask/Update", input);
        }
        public async Task<Res<object>> Remove(int  id)
        {
            return await _httpService.Get<Res<object>>("/api/Worker/ScanTask/Remove?id="+ id);
        }

        public async Task<Res<object>> Start(int id)
        {
            return await _httpService.Get<Res<object>>("/api/Worker/ScanTask/Start?id=" + id);
        }

        public async Task<Res<long>> Detail(ScanTaskOutput input)
        {
            return await _httpService.PostJson<Res<long>>("/api/Worker/ScanTask/Detail",input);
        }


    }
}