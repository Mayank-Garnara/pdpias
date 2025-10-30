using Microsoft.Ajax.Utilities;
using PDPIAS_STORE;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;

namespace PDPIAS_STORE.Models.labTech
{
    public class LabTechDashboardViewModel
    {
        public List<indexViewLtModel> IndexModels { get; set; }
        public List<IssuedChemicalHistory> IssuedChemicalHistories { get; set; }
        public List<lowStockAlert> lowStockAlert { get; set; }
    }

    public class indexViewLtModel
    {
        public string name { get; set; }
    }

    public class IssuedChemicalHistory
    {
        public int ChemicalId { get; set; }
        public string ChemicalName { get; set; }
        public string Quantity { get; set; }
        public string Supplier { get; set; }
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
    }

    public class lowStockAlert
    {
        public String chemicalName { get; set; }
        public String currentStock { get; set; }
        public String SeftyStock { get; set; }
    }
}


//1.Issued Chemical History
//Lab Technician ne ab tak kis-kis ko chemicals issue kiye hain, uska record.

//Filter by: Date, Chemical Name, Issued To.

//2. Low Stock Alert
//Agar koi chemical ya glassware threshold se kam hai toh uska alert.

//Auto alert + Manual check option.

//3. Return/Used Chemical Entry
//Lab Technician yeh enter kar sake ki koi chemical use ho gaya ya return hua.

//Quantity used, remaining, and remarks.

//4. View Chemical Inventory
//Saari available chemicals ki list (quantity, price, expiry date, etc.)

//5. Glassware Return/Breakage Entry
//Lab users agar glassware return karte hain ya break ho jata hai toh uska record.

//6. Request Status Tracking
//Jo chemical ya glassware order kiya gaya hai, uska approval/rejection status.

//7. Generate Report / Export Data
//Daily/Weekly/Monthly chemical issue report export kar sake (PDF/Excel).

//8. Add New Chemical Entry (if allowed)
//Agar permission ho, toh Lab Tech new chemical entry kar sake stock me.

//9. Supplier Info View
//Har chemical ka supplier information dekh sake for follow-up.

//10. Notification Center
//System-generated notifications (expiry, approval, stock alert) ka centralized view.