using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Wings.Saas.Server.Models;
using Wings.Saas.Shared.Areas.Worker.Dtos;

namespace Wings.Saas.Server.Services
{
 
    public class ScanTaskService
    {
        public readonly ApplicationDbContext applicationDbContext;
        public readonly RedisService redisService;

        public ScanTaskService(ApplicationDbContext _applicationDbContext, RedisService _redisService)
        {
            applicationDbContext = _applicationDbContext;
            redisService = _redisService;

        }

        public async Task<bool> ScanTaskStart(ScanTask scanTask)
        {
            scanTask.Status = ScanTaskStatus.Process;
            await applicationDbContext.SaveChangesAsync();
            return await redisService.CreateScanTaskProcess(scanTask);
        }

        public async Task<bool> ClearScanTask(ScanTask scanTask)
        {
            scanTask.Status = ScanTaskStatus.Cancel;
            await applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveScanTask(ScanTask scanTask)
        {
            applicationDbContext.Remove(scanTask);
            await applicationDbContext.SaveChangesAsync();
            return true;

        }

        public async Task<List<string>> GetTask(int num)
        {
            var task = await applicationDbContext.ScanTasks.Where(ocrTask => ocrTask.Status == ScanTaskStatus.Process)
                .FirstOrDefaultAsync();
            if (task != null)
            {
                return await redisService.getScanTask(num);
            }
            else
            {
                return new List<string>();
            }

        }

        public async  Task<long> GetTaskCount(ScanTaskOutput input)
        {
           return await redisService.GetTaskCount(input);
        }
    }

}
