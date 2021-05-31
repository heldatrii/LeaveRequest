using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("tb_m_leaveAllowance")]
    public class LeaveAllowance
    {
        [Key]
        [ForeignKey("Person")]
        public string NIK { get; set; }
        public int LeaveAllow { get; set; }
        public int UsedLeaveAllow { get; set; }
        [JsonIgnore]
        public virtual Person Person { get; set; }
    }
}
