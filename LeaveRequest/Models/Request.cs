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
        [JsonIgnore]
        public virtual ICollection<RequestStatus> RequestStatuses { get; set; }

        [JsonIgnore]
        public virtual IList<RequestType> RequestTypes { get; set; }

    }
}
