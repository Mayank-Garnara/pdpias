using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDPIAS_STORE.Models.Pricipal
{
    public class UserManagementViewModel
    {
        public List<AllRequest> AllRequests { get; set;  }
    }

    public class AllRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public string Stauts { get; set; }
    }
}