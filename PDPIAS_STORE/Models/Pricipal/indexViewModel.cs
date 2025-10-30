using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDPIAS_STORE.Models.Pricipal
{
    public class IndexViewModel
    {
        public int TotalChamicalRequestes { get; set; }

        public int TotalGlassWareRequest { get; set; }

        public int PendingRequest { get; set; }

        public int LabTechnician { get; set; }

        public List<DepartmentInvetoey> DepartmentInvetoeys { get; set; }

        public string[] Months { get; set; }
        public List<MonthyChart> MonthyCharts { get; set; }

        public List<PendingRequests> PendingRequests { get; set; }





    }
    public class DepartmentInvetoey
    {
        public string Name { get; set; }
        public int Chemicals { get; set; }
        public int GlassWare { get; set; }

    }

    public class MonthyChart
    {

        public string label { get; set; }
        public int[] data {  get; set; }
        public string borderColor { get; set; }
        public string backgroundColor { get; set; }

        public float tension { get; set; }

        public bool fill { get; set; }


    }

    public class PendingRequests
    {
        public int SrNo { get; set; }
        public string Item { get; set; }

        public string Department { get; set; }
        public string Status { get; set; }

    }
}