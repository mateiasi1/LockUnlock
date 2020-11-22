using System;
using System.Collections.Generic;
using System.Text;

namespace Documents_Editor
{
    public class RequestObject
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LockedBy { get; set; }
        public ActionType ActionType { get; set; }
    }
}
