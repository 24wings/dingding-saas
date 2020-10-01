using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wings.Saas.Server.Models;
using Wings.Saas.Shared.Areas.Common.Dtos;
using Wings.Saas.Shared.Areas.Worker.Dtos;

namespace Wings.Saas.Server.Areas.Worker
{
    [Route("api/Worker/[Controller]/[action]")]
    public class OcrTaskController:ControllerBase
    {
        public readonly ApplicationDbContext _applicationDbContext;

        public OcrTaskController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;}

        public async Task<Res<List<OcrTaskOutput>>> Load()
        {
          var rtn= await _applicationDbContext.OcrTasks.Select(ocr=>new OcrTaskOutput
          {
              Id = ocr.Id,
              TaskName = ocr.TaskName,
              Start = ocr.Start,End = ocr.End,Status = ocr.Status,
              Key = ocr.Key,
              Secret = ocr.Secret

          }).ToListAsync();
          return Res<List<OcrTaskOutput>>.Success(rtn);
        }

        public async Task<Res<bool>> Update([FromBody]OcrTaskOutput input)
        {
            var rtn = await _applicationDbContext.OcrTasks.FirstOrDefaultAsync(ocr=>ocr.Id==input.Id);
            if (rtn != null)
            {
                rtn.Start = input.Start;
                rtn.End = input.End;
                rtn.Status = input.Status;
                rtn.TaskName = input.TaskName;
                rtn.Key = input.Key;
                rtn.Secret = input.Secret;
                 _applicationDbContext.OcrTasks.Update(rtn);
                 await _applicationDbContext.SaveChangesAsync();
                 return Res<bool>.Success(true);

            }
            else
            {
                return Res<bool>.Success(true,"不存在");
            }
            
        }

        public async Task<Res<bool>> Add([FromBody] OcrTaskOutput input)
        {

            _applicationDbContext.OcrTasks.Add(new OcrTask()
                {TaskName = input.TaskName, Status = input.Status, Start = input.Start, End = input.End,Key = input.Key,Secret = input.Secret});
            await _applicationDbContext.SaveChangesAsync();
            return Res<bool>.Success(true,"");
        }

        public async Task<Res<bool>> Remove(int id)
        {
            var rtn = await _applicationDbContext.OcrTasks.FirstOrDefaultAsync(ocr => ocr.Id == id);
            _applicationDbContext.OcrTasks.Remove(rtn);
            await _applicationDbContext.SaveChangesAsync();
            return Res<bool>.Success(true);
        }
    }
}
