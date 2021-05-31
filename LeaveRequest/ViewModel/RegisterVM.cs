using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.ViewModel
{
    public class RegisterVM
    {
        public string NIK { get; set; }
        public int IdDepartement { get; set; }
        public string ManagerId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
