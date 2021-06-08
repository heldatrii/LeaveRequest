using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.ViewModel
{
    public class ApplyDetailIDVM
    {
        public string NIK { get; set; }
        public int IdDepartement { get; set; }
        public string DepartementName { get; set; }
        public int ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public int IdRequest { get; set; }
        public int RequestId { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int IdType { get; set; }
        public string Type { get; set; }
    }
}
