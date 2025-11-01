using PDPIAS_STORE.Models.Pricipal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace PDPIAS_STORE.Controllers
{
    public class PrincipalController : Controller
    {
        public principal principalData;
        private db_pdpiasEntities2 _context = new db_pdpiasEntities2();
       
        

        public ActionResult Index()
        {
            principalData = Session["userData"] as principal;

            IndexViewModel indexViewModel = new IndexViewModel
            {
                TotalChamicalRequestes = 1500,
                TotalGlassWareRequest = 150,
                PendingRequest = 10,
                LabTechnician = 10,
                Months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun" },
                DepartmentInvetoeys = new List<DepartmentInvetoey>(),
                PendingRequests = new List<PendingRequests>()
                {
                    
                     new PendingRequests()
                    {
                        SrNo = 1,
                        Item = "Hydrochloric Acid",
                        Department = "Chemistry",
                        Status = "Pending",
                    }, new PendingRequests()
                    {
                        SrNo = 2,
                        Item = "Hydrochloric Acid",
                        Department = "Chemistry",
                        Status = "Approved",
                    }, new PendingRequests()
                    {
                        SrNo = 3,
                        Item = "Hydrochloric Acid",
                        Department = "Chemistry",
                        Status = "Rejected",
                    },
                },
                MonthyCharts = new List<MonthyChart>
                {
                    
                    new MonthyChart
                    {
                        label = "Chemicals",
                        data = new int[] { 65, 59, 80, 81, 56, 72 },
                        borderColor = "#4f46e5",
                        backgroundColor = "rgba(79, 70, 229, 0.1)",
                        tension = 0.3f,
                        fill = true
                    },
                    new MonthyChart
                    {
                        label = "Glassware",
                        data = new int[] { 28, 48, 40, 19, 36, 27 },
                        borderColor = "#10b981",
                        backgroundColor = "rgba(16, 185, 129, 0.1)",
                        tension = 0.3f,
                        fill = true
                    }
                }

            };

            DepartmentInvetoey dvm = new DepartmentInvetoey
            {
                Name = "Temp",
                Chemicals = 10,
                GlassWare = 20
            };

            indexViewModel.DepartmentInvetoeys.Add(dvm);
            indexViewModel.DepartmentInvetoeys.Add(dvm);
            indexViewModel.DepartmentInvetoeys.Add(dvm);
            indexViewModel.DepartmentInvetoeys.Add(dvm);

            return View(indexViewModel);
        }

        public ActionResult ChemicalTracking()
        {
            return View();
        }

        public ActionResult GlasswareTracking()
        {
            return View();
        }
        public ActionResult RequestTracking()
        {
            RequestTrackingViewModel requestTrackingViewModel = new RequestTrackingViewModel();
            requestTrackingViewModel.AllRequests = new List<RequestTracking>();

            RequestTracking request = new RequestTracking()
            {
                Id = 101,
                RequestedBy = "Kavit",
                Department = "Chemistry",
                ItemType = "Chemical", 
                ItemName = "Hydrochloric Acid",
                Quantity = "\t200 ml",
                Date = "10-Apr-2025",
                Status = "Pending"
            };

            requestTrackingViewModel.AllRequests.Add(request);
            requestTrackingViewModel.AllRequests.Add(request);
            requestTrackingViewModel.AllRequests.Add(request);
            requestTrackingViewModel.AllRequests.Add(request);
            requestTrackingViewModel.AllRequests.Add(request);

            return View(requestTrackingViewModel);
        }

        public ActionResult ChemicalDetails()
        {
            return View();
        }

        public ActionResult GlasswareDetails()
        {
            return View();
        }
        public ActionResult CurrentChemicalStock()
        {
            return View();
        }
        public ActionResult CurrentGlassWareStock()
        {
            return View();
        }
        public ActionResult AddStock()
        {
            return View();
        }

        #region user management
        public ActionResult PendingUsers()
        {

            UserManagementViewModel userManagementViewModel = new UserManagementViewModel();
            userManagementViewModel.AllRequests = new List<AllRequest> ();


            var labTechRequests = _context.lab_technician
            .Where(lt => lt.status == 0) // Filter: Database status is 0
            .Select(lt => new AllRequest
            {
                // Map properties to your AllRequest class
                Id = lt.Id,
                Name = lt.first_name + " " + lt.last_name,
                Email = lt.email,
                Role = "Lab Technician",
                Department = lt.department.department_name, 
                Stauts = "Pending" // Set the C# 'Stauts' property to a descriptive string
            });

            // --- 2. Select from store_manager where status = 0 ---
            var storeManagerRequests = _context.store_manager
                .Where(sm => sm.status == 0) // Filter: Database status is 0
                .Select(sm => new AllRequest
                {
                    // Map properties to your AllRequest class
                    Id = sm.Id,
                    Name = sm.first_name + " " + sm.last_name,
                    Email = sm.email,
                    Role = "Store Manager",
                    Department = "-",
                    Stauts = "Pending"
                });

            // --- 3. Select from department_head where status = 0 ---
            var deptHeadRequests = _context.department_head
                .Where(dh => dh.status == 0) // Filter: Database status is 0
                .Select(dh => new AllRequest
                {
                    // Map properties to your AllRequest class
                    Id = dh.Id,
                    Name = dh.first_name + " " + dh.last_name,
                    Email = dh.email,
                    Role = "Department Head",
                    Department = dh.department.department_name,
                    Stauts = "Pending"
                });

            // --- 4. Combine all the results using Concat() ---
            var combinedRequests = labTechRequests
                .Concat(storeManagerRequests)
                .Concat(deptHeadRequests)
                .ToList(); // Executes the query


            userManagementViewModel.AllRequests.AddRange(combinedRequests);

            return View("UserManagement",userManagementViewModel);
        }

        public ActionResult ApprovedUsers()
        {

            UserManagementViewModel userManagementViewModel = new UserManagementViewModel();
            userManagementViewModel.AllRequests = new List<AllRequest>();


            var labTechRequests = _context.lab_technician
            .Where(lt => lt.status == 1)
            .Select(lt => new AllRequest
            {
                
                Id = lt.Id,
                Name = lt.first_name + " " + lt.last_name,
                Email = lt.email,
                Role = "Lab Technician",
                Department = lt.department.department_name,
                Stauts = "Approved" 
            });

            
            var storeManagerRequests = _context.store_manager
                .Where(sm => sm.status == 1) 
                .Select(sm => new AllRequest
                {
                    
                    Id = sm.Id,
                    Name = sm.first_name + " " + sm.last_name,
                    Email = sm.email,
                    Role = "Store Manager",
                    Department = "-",
                    Stauts = "Approved"
                });

           
            var deptHeadRequests = _context.department_head
                .Where(dh => dh.status == 1) 
                .Select(dh => new AllRequest
                {
                    
                    Id = dh.Id,
                    Name = dh.first_name + " " + dh.last_name,
                    Email = dh.email,
                    Role = "Department Head",
                    Department = dh.department.department_name,
                    Stauts = "Approved"
                });

            
            var combinedRequests = labTechRequests
                .Concat(storeManagerRequests)
                .Concat(deptHeadRequests)
                .ToList(); 


            userManagementViewModel.AllRequests.AddRange(combinedRequests);

            return View("UserManagement",userManagementViewModel);
        }

        public ActionResult BlockedUsers()
        {

            UserManagementViewModel userManagementViewModel = new UserManagementViewModel();
            userManagementViewModel.AllRequests = new List<AllRequest>();


            var labTechRequests = _context.lab_technician
            .Where(lt => lt.status == 2) // Filter: Database status is 0
            .Select(lt => new AllRequest
            {
                // Map properties to your AllRequest class
                Id = lt.Id,
                Name = lt.first_name + " " + lt.last_name,
                Email = lt.email,
                Role = "Lab Technician",
                Department = lt.department.department_name,
                Stauts = "Blocked" // Set the C# 'Stauts' property to a descriptive string
            });

            // --- 2. Select from store_manager where status = 0 ---
            var storeManagerRequests = _context.store_manager
                .Where(sm => sm.status == 2) // Filter: Database status is 0
                .Select(sm => new AllRequest
                {
                    // Map properties to your AllRequest class
                    Id = sm.Id,
                    Name = sm.first_name + " " + sm.last_name,
                    Email = sm.email,
                    Role = "Store Manager",
                    Department = "-",
                    Stauts = "Blocked"
                });

            // --- 3. Select from department_head where status = 0 ---
            var deptHeadRequests = _context.department_head
                .Where(dh => dh.status == 2) // Filter: Database status is 0
                .Select(dh => new AllRequest
                {
                    // Map properties to your AllRequest class
                    Id = dh.Id,
                    Name = dh.first_name + " " +dh.last_name,
                    Email = dh.email,
                    Role = "Department Head",
                    Department = dh.department.department_name,
                    Stauts = "Blocked"
                });

            // --- 4. Combine all the results using Concat() ---
            var combinedRequests = labTechRequests
                .Concat(storeManagerRequests)
                .Concat(deptHeadRequests)
                .ToList(); // Executes the query


            userManagementViewModel.AllRequests.AddRange(combinedRequests);

            return View("UserManagement",userManagementViewModel);
        }


        

        #endregion
        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            return View();
        }

    }
}