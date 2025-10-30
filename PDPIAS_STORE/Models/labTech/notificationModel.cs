using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;

namespace PDPIAS_STORE.Models.labTech
{
    public class notificationModel
    {

        //New Chemical/Glassware Order Status

        public String casNo {  get; set; }
        public String chemicalName { get; set; }
        public String Status { get; set; }
        public String ApprovedBy { get; set; }
        public String Date { get; set; }

        //Low Stock Alerts

        public String ItemName { get; set; }
        public String ItemType { get; set; }
        public String CurrentStock { get; set; }
        public String MinimumRequired { get; set; }
        public String Stock { get; set; }

    }
}