using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    public class LeaveAllowance
    {
        public int Id { get; set; }
        public int LeaveAllow { get; set; }
        public int UsedLeaveAllow { get; set; }
    }
}
