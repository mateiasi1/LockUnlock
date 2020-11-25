using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pessimistic_Concurrency_Service.Enums;
using Pessimistic_Concurrency_Service.Interfaces;
using Pessimistic_Concurrency_Service.Managers;
using Pessimistic_Concurrency_Service.Models;

namespace Pessimistic_Concurrency_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LockUnlockController : ControllerBase
    {
        private readonly ILockUnlockManager _lockUnlockManager;

        public LockUnlockController(ILockUnlockManager lockUnlockManager)
        {
            _lockUnlockManager = lockUnlockManager;
        }

        [HttpPost("lock")]
        public async Task<IActionResult> Lock([FromBody]BaseObject baseObject)
        {
            if (baseObject.ActionType == ActionType.Lock)
            {
                    var res = await Task.Run(() => _lockUnlockManager.Lock(baseObject));
                    return Ok(res);
                
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("unlock")]
        public async Task<IActionResult> UnLock([FromBody]BaseObject baseObject)
        {
            if (baseObject.ActionType == ActionType.Unlock)
            {
                var res = await Task.Run(() => _lockUnlockManager.Unlock(baseObject));
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _lockUnlockManager.GetAll();
            if (res.Count!=0)
            {
                return Ok(res);
            }
            return Ok(new List<BaseObject>());
        }
    }
}
