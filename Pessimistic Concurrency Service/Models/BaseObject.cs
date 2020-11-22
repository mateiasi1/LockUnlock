using Pessimistic_Concurrency_Service.Enums;
using Pessimistic_Concurrency_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pessimistic_Concurrency_Service.Models
{
    public class BaseObject
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LockedBy { get; set; }
        public  ActionType ActionType { get; set; }
    }
}
