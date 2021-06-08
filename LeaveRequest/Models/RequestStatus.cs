using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("tb_m_requestStatus")]
    public class RequestStatus
    {
        public string NIK { get; set; }
        [JsonIgnore]
        public virtual Person Person { get; set; }
        public int RequestId { get; set; }
        [JsonIgnore]
        public virtual Request Request { get; set; }
        public string Status { get; set; }

    }
}
