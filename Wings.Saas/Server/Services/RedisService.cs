using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Wings.Saas.Server.Models;
using Wings.Saas.Shared;
using Wings.Saas.Shared.Areas.Worker.Dtos;

namespace Wings.Saas.Server.Services
{



    public class RedisService
    {
        public ConnectionMultiplexer redis { get; set; }
        public IConfiguration Configuration { get; }

        public ApplicationDbContext applicationDbContext { get; set; }

        public RedisService(IConfiguration _Configuration, ApplicationDbContext _applicationDbContext)
        {
            Configuration = _Configuration;
            applicationDbContext = _applicationDbContext;
            redis = ConnectionMultiplexer.Connect(_Configuration.GetConnectionString("RedisConnection"));
        }
        /// <summary>
        /// scan task create
        /// </summary>
        /// <param name="scanTask"></param>
        /// <returns></returns>
        public async Task<bool> CreateScanTaskProcess(ScanTask scanTask)
        {
            var exsit = database.KeyExists(scanTask.TaskName + "-scan-process");
            if (exsit)
            {
                database.KeyDelete(scanTask.TaskName + "-scan-process");

            }

            var taskList = new List<RedisValue>();
            for (var i = scanTask.Start; i < scanTask.End; i++)
            {
                taskList.Add(new RedisValue(i.ToString()));
            }
            await database.ListRightPushAsync(scanTask.TaskName + "-scan-process", taskList.ToArray());
            Console.WriteLine("add scan-task-process length:" + (scanTask.End - scanTask.Start).ToString() + " items");

            return true;

        }

        public async Task<long> GetTaskCount(ScanTaskOutput taskOutput)
        {
            var exsit = database.KeyExists(taskOutput.TaskName + "-scan-process");
            if (exsit)
            {
                return database.ListLength(taskOutput.TaskName + "-scan-process");
            }
            else
            {
                return 0L;
            }

        }

        public async Task<bool> clearScanTaskProcess(ScanTask scanTask)
        {
            var processListKeyExsit = database.KeyExists(scanTask.TaskName + "scan-process");
            var finishedListKeyExsit = database.KeyExists(scanTask.TaskName + "scan-finished");
            if (processListKeyExsit)
            {
                database.KeyDelete(scanTask.TaskName + "-scan-process");

            }

            if (finishedListKeyExsit)
            {
                database.KeyDelete(scanTask.TaskName + "-scan-finished");
            }

            return true;
        }

        public IDatabase database { get { return redis.GetDatabase(0); } }

        //public async Task<bool> clearTaskProcess(OcrTask ocrTask)
        //{
        //    var processListKeyExsit = database.KeyExists(ocrTask.TaskName + "-process");
        //    var finishedListKeyExsit = database.KeyExists(ocrTask.TaskName + "-finished");
        //    if (processListKeyExsit)
        //    {
        //        database.KeyDelete(ocrTask.TaskName + "-process");

        //    }

        //    if (finishedListKeyExsit)
        //    {
        //        database.KeyDelete(ocrTask.TaskName + "-finished");
        //    }

        //    return true;
        //}

        public async Task<bool> createTaskProcess(OcrTask ocrTask)
        {
            var exsit = database.KeyExists(ocrTask.TaskName + "-process");
            if (exsit)
            {

            }
            else
            {
                var taskList = new List<RedisValue>();
                for (var i = ocrTask.Start; i < ocrTask.End; i++)
                {
                    taskList.Add(new RedisValue(i.ToString()));
                }
                await database.ListRightPushAsync(ocrTask.TaskName + "-process", taskList.ToArray());
                Console.WriteLine("add task-process length:" + (ocrTask.End - ocrTask.Start).ToString() + " items");
            }
            return true;

        }

        public async Task<TaskItem> getTask()
        {
            var task = await applicationDbContext.OcrTasks.Where(ocrTask => ocrTask.Status == OcrTaskStatus.Process).FirstOrDefaultAsync();
            if (task != null)
            {
                var nextUrl = database.ListLeftPop(task.TaskName + "-process");
                if (nextUrl.HasValue)
                {
                    return new TaskItem() { No = int.Parse(nextUrl.ToString()), Key = task.Key, Secret = task.Secret, TaskId = task.Id, TaskName = task.TaskName };
                }
                else
                {
                    task.Status = OcrTaskStatus.Finished;
                    await applicationDbContext.SaveChangesAsync();
                    return null;
                }
            }
            else
            {
                return null;
            }





        }

        public async Task<List<string>> getScanTask(int num)
        {
            var task = await applicationDbContext.ScanTasks.Where(ocrTask => ocrTask.Status == ScanTaskStatus.Process).FirstOrDefaultAsync();
            if (task != null)
            {
                var processName = task.TaskName + "-scan-process";
                var values = await database.ListRangeAsync(processName, 0, num - 1);
                await database.ListTrimAsync(processName, num - 1, -1);
                var result = values.Where(v => v.HasValue).Select(v => v.ToString()).ToList();
                if (database.ListLength(processName) == 0)
                {
                    task.Status = ScanTaskStatus.Finished;
                    await applicationDbContext.SaveChangesAsync();
                }

                return result;

            }
            else
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// 获取任务处理中数量
        /// </summary>
        /// <param name="taskName"></param>
        /// <returns></returns>
        public long getOcrTaskProccessNumber(string taskName)
        {
            return database.ListLength(taskName + "-process");
        }

        /// <summary>
        /// 获取任务完成数量
        /// </summary>
        /// <param name="taskName"></param>
        /// <returns></returns>
        public long getOcrTaskFinishedNumber(string taskName)
        {
            return database.ListLength(taskName + "-finished");
        }
    }


}
