using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("tb_m_person")]
    public class Person
    {
        public Person()
        {
            subPerson = new HashSet<Person>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NIK { get; set; }
        public int IdDepartement { get; set; }
        public int? ManagerId { get; set; }
        [JsonIgnore]
        public virtual Person ParentPerson { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        [JsonIgnore]
        public virtual ICollection<Person> subPerson { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual LeaveAllowance LeaveAllowance { get; set; }
        [JsonIgnore]
        public virtual Departement Departement { get; set; }
        [JsonIgnore]
        public virtual ICollection<RequestStatus> RequestStatuses{ get; set; }
    }
}
