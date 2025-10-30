using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDPIAS_STORE.Models.labTech
{
    public class chemicalOrder
    {
        public int chemicalID { get; set; }
        public String Supplier { get; set; }
        public String casNo { get; set; }
        public String chemicalName { get; set; }
        public String quantity { get; set; }
        public int molecularWeight { get; set; }
        public String orderDate { get; set; }
        public String expiryDate { get; set; }
        public int orderNo { get; set; }
        public String safetyLevel { get; set; }
        public String status { get; set; }
    }
}