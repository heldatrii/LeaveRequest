﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("tb_m_role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string NameRole { get; set; }

        [JsonIgnore]
        public virtual IList<RoleAccount> RoleAccounts { get; set; }
    }
}
