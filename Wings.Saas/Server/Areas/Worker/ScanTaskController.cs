using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wings.Saas.Server.Models;
using Wings.Saas.Shared;
using System.Linq;
using Wings.Saas.Server.Services;
using Wings.Saas.Shared.Areas.Common.Dtos;
using Wings.Saas.Shared.Areas.Worker.Dtos;
using ScanTaskStatus = Wings.Saas.Shared.Areas.Worker.Dtos.ScanTaskStatus;

namespace Wings.Saas.Server.Areas.Worker
{
    [Route("api/Worker/[Controller]/[action]")]
    public class ScanTaskController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ScanTaskService _scanTaskService;
        public readonly RedisService _redisService;
        public ScanTaskController(ApplicationDbContext applicationDbContext, ScanTaskService scanTaskService, RedisService redisService)
        {
            _applicationDbContext = applicationDbContext;
            _scanTaskService = scanTaskService;
            _redisService = redisService;
        }
        [HttpGet]
        public Res<List<ScanTaskOutput>> Load()
        {
            var rtn = _applicationDbContext.ScanTasks.Select(scan => new ScanTaskOutput { Id = scan.Id, Start = scan.Start, End = scan.End, Status = scan.Status, TaskName = scan.TaskName }).ToList();
            //var result = new List<ScanTaskOutput>() { new ScanTaskOutput { Id = 1, TaskName = "A", Start = 0 } };
            return Res<List<ScanTaskOutput>>.Success(rtn);
        }
        [HttpPost]
        public async Task<Res<bool>> Add([FromBody] ScanTaskOutput input)
        {
            await _applicationDbContext.ScanTasks.AddAsync(new ScanTask()
            { TaskName = input.TaskName, Start = input.Start, End = input.End });
            await _applicationDbContext.SaveChangesAsync();

            return Res<object>.Success(true);
        }

        public async Task<Res<bool>> Remove(int id)
        {
            var data = _applicationDbContext.ScanTasks.FirstOrDefault(scan => scan.Id == id);
            _applicationDbContext.ScanTasks.Remove(data);
            await _applicationDbContext.SaveChangesAsync();
            return Res<bool>.Success(true);

        }
        [HttpPost]
        public async Task<Res<bool>> Update([FromBody] ScanTaskOutput input)
        {
            var scan = _applicationDbContext.ScanTasks.FirstOrDefault(scan => scan.Id == input.Id);
            if (scan != null)
            {
                scan.Start = input.Start;
                scan.TaskName = input.TaskName;
                scan.End = input.End;
                _applicationDbContext.ScanTasks.Update(scan);
                await _applicationDbContext.SaveChangesAsync();
                return Res<bool>.Success(true);
            }
            else
            {
                return Res<bool>.Error(false, "not found");
            }
        }

        public async Task<Res<bool>> Start(int id)
        {
            var scan = _applicationDbContext.ScanTasks.FirstOrDefault(scan => scan.Id == id);
            if (scan != null)
            {
                await _scanTaskService.ScanTaskStart(scan);
            }

            return Res<bool>.Success(true);

        }


        public async Task<Res<long>> Detail([FromBody] ScanTaskOutput input)
        {
            var count = await _scanTaskService.GetTaskCount(input);
            return Res<long>.Success(count);
        }




        public async Task<List<string>> GetTask()
        {
            return await _scanTaskService.GetTask(1000);
        }

    }
}