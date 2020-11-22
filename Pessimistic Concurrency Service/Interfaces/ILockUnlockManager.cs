using Pessimistic_Concurrency_Service.Enums;
using Pessimistic_Concurrency_Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pessimistic_Concurrency_Service.Interfaces
{
    public interface ILockUnlockManager
    {
        public bool Lock(BaseObject baseObject);
        public bool Unlock(BaseObject baseObject);
        public List<BaseObject> GetHistory();
        public List<BaseObject> GetAll();

    }
}
