using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("tb_m_requestType")]
    public class RequestType
    {
        public int IdRequest { get; set; }

        [JsonIgnore]
        public virtual Request Request { get; set; }
        public int IdType { get; set; }

        [JsonIgnore]
        public virtual Tipe Tipe { get; set; }
    }
}
