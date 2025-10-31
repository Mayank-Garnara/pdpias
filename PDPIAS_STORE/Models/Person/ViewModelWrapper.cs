using PDPIAS_management.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDPIAS_management.Models.Person
{
    public class ViewModelWrapper
    {
        public DepartmentHeadViewModel DepartmentHead { get; set; }
        public HeadStoreManagerViewModel HeadStore { get; set; }
        public LabTechnicianViewModel LabTechnician { get; set; }

        public String SelectedRole { get; set; }
    }
}