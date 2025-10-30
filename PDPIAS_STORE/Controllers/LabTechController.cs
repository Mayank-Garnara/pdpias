using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PDPIAS_STORE.Models.headStoreModel;
using PDPIAS_STORE.Models.labTech;
using PDPIAS_STORE.Models.Pricipal;


namespace PDPIAS_STORE.Controllers
{
    public class labTechController : Controller
    {
       

        // GET: labTech
        public ActionResult Index()
        {
            var indexData = new List<indexViewLtModel>
            {
                new indexViewLtModel { name = "Overview of chemical stock, issued items, low stock alerts." }
            };

            var historyData = new List<IssuedChemicalHistory>
            {
                new IssuedChemicalHistory
                {
                    ChemicalId = 1,
                    ChemicalName = "Head Store Manager",
                    Quantity = "12-23-4",
                    Supplier = "benzene",
                    OrderNo = "50ml",
                    OrderDate = "12-5-2025",
                    ExpiryDate = "12-4-2025",
                    Status = "approve"
                },
                new IssuedChemicalHistory
                {
                    ChemicalId = 1,
                    ChemicalName = "Depertment",
                    Quantity = "71-23-4",
                    Supplier = "p-nitro-benzene",
                    OrderNo = "50ml",
                    OrderDate = "12-5-2025",
                    ExpiryDate = "12-4-2025",
                    Status = "Pending"
                },
                new IssuedChemicalHistory
                {
                    ChemicalId = 1,
                    ChemicalName = "Head Store Manager",
                    Quantity = "78-23-4",
                    Supplier = "quonoline",
                    OrderNo = "50ml",
                    OrderDate = "12-5-2025",
                    ExpiryDate = "12-4-2025",
                    Status = "rejected"
                },
                new IssuedChemicalHistory
                {
                    ChemicalId = 1,
                    ChemicalName = "Depertment",
                    Quantity = "89-23-4",
                    Supplier = "indole",
                    OrderNo = "50ml",
                    OrderDate = "12-5-2025",
                    ExpiryDate = "12-4-2025",
                    Status = "approve"
                }

            };

            var lowStockData = new List<lowStockAlert>
            {
                new lowStockAlert
                {
                    chemicalName = "Benzene",
                    currentStock = "10ml",
                    SeftyStock = "100ml",     
                },
                new lowStockAlert
                {
                    chemicalName = "indole",
                    currentStock = "50gm",
                    SeftyStock = "500mg",
                },
                new lowStockAlert
                {
                    chemicalName = "ammonia",
                    currentStock = "10ml",
                    SeftyStock = "1000ml",
                },
            };

            var viewModel = new LabTechDashboardViewModel
            {
                IndexModels = indexData,
                IssuedChemicalHistories = historyData,
                lowStockAlert = lowStockData
            };

            return View(viewModel);
        }

        public ActionResult ChemicalOrder()
        {
            List<chemicalOrder> chemicalOrder = new List<chemicalOrder>
            {
                new chemicalOrder
                {
                    chemicalID = 1,
                    Supplier = "Head Store Manager",
                    casNo = "12-23-4",
                    chemicalName = "benzene",
                    quantity = "50ml",
                    molecularWeight = 78,
                    orderDate = "12-4-2025",
                    expiryDate = "16-8-2025",
                    orderNo = 1,
                    safetyLevel = "hazard",
                    status = "pending"
                },
                new chemicalOrder
                {
                    chemicalID = 2,
                    Supplier = "Department",
                    casNo = "79-23-4",
                    chemicalName = "p-nitro-benzene",
                    quantity = "100gm",
                    molecularWeight = 120,
                    orderDate = "12-4-2025",
                    expiryDate = "16-8-2025",
                    orderNo = 1,
                    safetyLevel = "low",
                    status = "pending"
                },
                new chemicalOrder
                {
                    chemicalID = 3,
                    Supplier = "Department",
                    casNo = "102-23-4",
                    chemicalName = "acetne",
                    quantity = "500ml",
                    molecularWeight = 56,
                    orderDate = "12-4-2025",
                    expiryDate = "16-8-2025",
                    orderNo = 1,
                    safetyLevel = "medium",
                    status = "Approve"
                }
            };

            //chemicalOrder co = new chemicalOrder();



            return View(chemicalOrder);
        }

        public ActionResult SearchChemical()
        {
            indexViewLtModel lt = new indexViewLtModel();

            return View(lt);
        }

        public ActionResult GlasswareOrder()
        {
            List<GlasswareOrder> glasswareList = new List<GlasswareOrder>
            {
                new GlasswareOrder
                {
                    glasswareID = 1,
                    glasswareName = "Test Tube",
                    glasswareSize = "500ml",
                    current_quantity = 500,
                    imagePath = "~/assets/glasswareImages/testtube.jpg"
                },
                new GlasswareOrder
                {
                    glasswareID = 2,
                    glasswareName = "Beaker",
                    glasswareSize = "250ml",
                    current_quantity = 300,
                    imagePath = "~/assets/glasswareImages/beaker.jpg"
                },
                new GlasswareOrder
                {
                    glasswareID = 3,
                    glasswareName = "Flask",
                    glasswareSize = "1000ml",
                    current_quantity = 200,
                    imagePath = "~/assets/glasswareImages/flask.jpg"
                },
                new GlasswareOrder
                {
                    glasswareID = 2,
                    glasswareName = "pettry dish",
                    glasswareSize = "250ml",
                    current_quantity = 300,
                    imagePath = "~/assets/glasswareImages/pettryDish.png"
                },
                new GlasswareOrder
                {
                    glasswareID = 3,
                    glasswareName = "Measuring Celinder",
                    glasswareSize = "1000ml",
                    current_quantity = 200,
                    imagePath = "~/assets/glasswareImages/celinder.png"
                },
                new GlasswareOrder
                {
                    glasswareID = 3,
                    glasswareName = "Appendrof",
                    glasswareSize = "1000ml",
                    current_quantity = 200,
                    imagePath = "~/assets/glasswareImages/appendrof.png"
                },
                new GlasswareOrder
                {
                    glasswareID = 3,
                    glasswareName = "Tlc",
                    glasswareSize = "1000ml",
                    current_quantity = 200,
                    imagePath = "~/assets/glasswareImages/tlc.png"
                }
                // Add more items as needed
            };
            return View(glasswareList);
        }


        public ActionResult NotificationsAlerts()
        {
            List<notificationModel> notificationsAlerts = new List<notificationModel>
            {
                new notificationModel
                {
                    casNo = "12-45-8",
                    chemicalName = "benzene",
                    Status = "pending",
                    ApprovedBy = "head store manager",
                    Date = "12-9-2025",
                    ItemName = "benzene",
                    ItemType = "chemical",
                    CurrentStock = "10ml",
                    MinimumRequired = "500ml",
                    Stock = "low stock"
                },
                new notificationModel
                {
                    casNo = "126-89-7",
                    chemicalName = "p-nitro-benzene",
                    Status = "approved",
                    ApprovedBy = "department",
                    Date = "15-9-2025",
                    ItemName = "p-nitro-benzene",
                    ItemType = "chemical",
                    CurrentStock = "10ml",
                    MinimumRequired = "500ml",
                    Stock = "high stock"
                },
                new notificationModel
                {
                    casNo = "152-45-8",
                    chemicalName = "acetone",
                    Status = "pending",
                    ApprovedBy = "head store manager",
                    Date = "5-5-2025",
                    ItemName = "acetone",
                    ItemType = "chemical",
                    CurrentStock = "10ml",
                    MinimumRequired = "500ml",
                    Stock = "high stock"
                },
                new notificationModel
                {
                    casNo = "160-50-12",
                    chemicalName = "pyridine",
                    Status = "approved",
                    ApprovedBy = "department",
                    Date = "15-5-2025",
                    ItemName = "pyridine",
                    ItemType = "chemical",
                    CurrentStock = "50ml",
                    MinimumRequired = "100ml",
                    Stock = "low stock"
                },
                new notificationModel
                {
                    casNo = "152-45-8",
                    chemicalName = "acetone",
                    Status = "pending",
                    ApprovedBy = "head store manager",
                    Date = "5-5-2025",
                    ItemName = "acetone",
                    ItemType = "chemical",
                    CurrentStock = "10ml",
                    MinimumRequired = "500ml",
                    Stock = "high stock"
                },
                new notificationModel
                {
                    casNo = "160-50-12",
                    chemicalName = "pyridine",
                    Status = "approved",
                    ApprovedBy = "department",
                    Date = "15-5-2025",
                    ItemName = "pyridine",
                    ItemType = "chemical",
                    CurrentStock = "50ml",
                    MinimumRequired = "100ml",
                    Stock = "low stock"
                }
            };
            return View(notificationsAlerts);
        }

        public ActionResult ProfileSettings()
        {
            indexViewLtModel lt = new indexViewLtModel();

            lt.name = "Update personal profile or change password.";

            return View(lt);
        }

        public ActionResult LogOut()
        {
            indexViewLtModel lt = new indexViewLtModel();

            lt.name = "Log out from the system.";

            return View(lt);
        }


        
    }
}