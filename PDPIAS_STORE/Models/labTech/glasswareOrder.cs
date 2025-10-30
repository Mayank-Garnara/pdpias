using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDPIAS_STORE.Models.labTech
{
    public class GlasswareOrder
    {
        public int glasswareID { get; set; }
        public String glasswareName { get; set; }
        public String glasswareSize { get; set; }
        public int current_quantity { get; set; }
        public string imagePath { get; set; }
    }
}