using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("tb_m_tipe")]
    public class Tipe
    {
        [Key]
        public int TipeId { get; set; }
        public string NameTipe { get; set; }
        public string TypeKind { get; set; }

        [JsonIgnore]
        public virtual ICollection<Request> Requests { get; set; }

    }
}
