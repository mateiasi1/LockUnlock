using Pessimistic_Concurrency_Service.Interfaces;
using Pessimistic_Concurrency_Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pessimistic_Concurrency_Service.Managers
{
    public class SystemManager : ISystemManager
    {

        public void Check()
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(1);

            var timer = new System.Threading.Timer(async (e) =>
            {
                LockUnlockManager _lockUnlockManager = new LockUnlockManager();
                List<BaseObject>? list = _lockUnlockManager.GetAll();
                foreach (var item in list ?? new List<BaseObject>())
                {
                    Console.WriteLine("Item ID: " + item.Id + " LockedBy: " + item.LockedBy + " RowId: " + item.RowId + " StartDate: " + item.StartDate + " EndDate: " + item.EndDate);
                }
            }, null, startTimeSpan, periodTimeSpan);
        }
    }
}
