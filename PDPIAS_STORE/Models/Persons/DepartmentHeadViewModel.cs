using System;
using System.Collections.Generic;
using System.Linq;

namespace PDPIAS_management.Models.Persons
{
    public class DepartmentHeadViewModel_
    {
        public int Id { get; set; }

        
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string contact_no { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int department_id { get; set; }
        public bool is_active { get; set; }
        public byte status { get; set; }
        public System.DateTime created_at { get; set; }
        public string profile_pic { get; set; }

    }
}