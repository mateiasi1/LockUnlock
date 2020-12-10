using Pessimistic_Concurrency_Service.Enums;
using Pessimistic_Concurrency_Service.Interfaces;
using Pessimistic_Concurrency_Service.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pessimistic_Concurrency_Service.Managers
{
    public class LockUnlockManager : ILockUnlockManager
    {
        public static List<BaseObject> history = new List<BaseObject>();
        public static List<BaseObject> lockedObjects = new List<BaseObject>();
        private static readonly object _locker = new object();
        public static Status Status = Status.Unchanged;
        public LockUnlockManager()
        {
        }

        public List<BaseObject> GetHistory()
        {
            return history;
        }

        public bool Lock(BaseObject baseObject)
        {
            lock (_locker)
            {
                foreach (var item in lockedObjects)
                {
                    if (item.RowId == baseObject.RowId)
                    {
                        Thread.Sleep(4000);

                        Console.WriteLine("The record with the id=" + item.RowId + " is locked!");
                        return false;
                    }
                }
                baseObject.StartDate = DateTime.Now;
                Thread.Sleep(4000);
                lockedObjects.Add(baseObject);
                Console.WriteLine("The record is locked successfully!");
                Status = Status.Changed;
                return true;
            }
        }

        public bool Unlock(BaseObject baseObject)
        {
            lock (_locker)
            {
                foreach (var item in lockedObjects)
                {
                    if (item.RowId == baseObject.RowId && item.EndDate == null || item.EndDate <= DateTime.Now)
                    {
                        item.EndDate = DateTime.Now;
                        lockedObjects.Remove(item);
                        history.Add(item);
                        Thread.Sleep(4000);
                        Console.WriteLine("The record was unlocked successfully!");
                        Status = Status.Changed;
                        return true;
                    }

                }
                Thread.Sleep(4000);
                Console.WriteLine("The record is not locked!");
                return false;
            }
        }

        public List<BaseObject> GetAll()
        {
            if (Status == Status.Changed)
            {
                Status = Status.Unchanged;
                return lockedObjects;
            }
          
            return new List<BaseObject>();
        }
    }
    
}
