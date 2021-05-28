using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("tb_m_account")]
    public class Account
    {
        [Key]
        [ForeignKey("Person")]
        public int NIK { get; set; }
        public string Password { get; set; }
    }
}
