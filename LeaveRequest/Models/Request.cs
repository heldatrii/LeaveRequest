using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("tb_m_request")]
    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int IsDeleted { get; set; }
        public string Status { get; set; }
        [ForeignKey("Tipe")]
        public int TipeId { get; set; }

        [JsonIgnore]
        public virtual Tipe Tipe { get; set; }
        [ForeignKey("Person")]
        public string NIK { get; set; }
        [JsonIgnore]
        public virtual Person Person { get; set; }

    }
}
