using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableReservationSystem.Models.Interfaces;

namespace TableReservationSystem.Models
{
    public class Role : IRole
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!; 
    }
}
