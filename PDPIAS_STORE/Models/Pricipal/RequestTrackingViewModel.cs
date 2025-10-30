using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDPIAS_STORE.Models.Pricipal
{
    public class RequestTrackingViewModel
    {
           public List<RequestTracking> AllRequests = new List<RequestTracking> ();

    }
    public class RequestTracking
    {
        public int Id{ get; set; }    
        public string RequestedBy { get; set; }

        public string Department { get; set; }

        public string ItemType { get; set; }

        public string ItemName { get; set; }

        public string Quantity { get; set; }

        public string Date { get; set; }

        public string Status { get; set; }  
    }
}