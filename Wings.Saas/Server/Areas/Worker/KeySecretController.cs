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
    public class KeySecretController : ControllerBase
    {


        public readonly ApplicationDbContext _applicationDbContext;

        public KeySecretController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Res<List<KeySecretOutput>>> Load()
        {
            var rtn = await _applicationDbContext.KeySecrets
                .Select(secret => new KeySecretOutput()
                {
                    AccountName = secret.AccountName,
                    Id = secret.Id,
                    Key = secret.Key,
                    Secret = secret.Secret,
                    CreateDateTime = secret.CreateDateTime,
                    AccountPassword = secret.AccountPassword
                }).ToListAsync();
            return Res<List<OcrTaskOutput>>.Success(rtn);
        }

        public async Task<Res<bool>> Update([FromBody] KeySecretOutput input)
        {
            var rtn = await _applicationDbContext.KeySecrets.FirstOrDefaultAsync(ocr => ocr.Id == input.Id);
            if (rtn != null)
            {
                rtn.AccountName = input.AccountName;
                rtn.Key = input.Key;
                rtn.Secret = input.Secret;
                rtn.AccountPassword = input.AccountPassword;
                _applicationDbContext.KeySecrets.Update(rtn);
                await _applicationDbContext.SaveChangesAsync();
                return Res<bool>.Success(true);

            }
            else
            {
                return Res<bool>.Success(true, "不存在");
            }

        }

        public async Task<Res<bool>> Add([FromBody] KeySecretOutput input)
        {

            _applicationDbContext.KeySecrets.Add(new KeySecret()
            {
                AccountName = input.AccountName,
                AccountPassword = input.AccountPassword,
                Key = input.Key,
                Secret = input.Secret,




            });
            await _applicationDbContext.SaveChangesAsync();
            return Res<bool>.Success(true, "");
        }

        public async Task<Res<bool>> Remove(int id)
        {
            var rtn = await _applicationDbContext.KeySecrets.FirstOrDefaultAsync(ocr => ocr.Id == id);
            _applicationDbContext.KeySecrets.Remove(rtn);
            await _applicationDbContext.SaveChangesAsync();
            return Res<bool>.Success(true);
        }
    }
}
