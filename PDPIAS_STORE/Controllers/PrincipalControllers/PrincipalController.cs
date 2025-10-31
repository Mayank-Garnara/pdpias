
using PDPIAS_STORE.Models.Pricipal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PDPIAS_STORE.Controllers
{
    public class Principal_Controller : Controller
    {
        // GET: Principal
        public ActionResult Index()
        {

            IndexViewModel indexViewModel = new IndexViewModel
            {
                TotalChamicalRequestes = 1550,
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





            //indexViewModel.MonthyCharts.Add(monthyChart);
            //indexViewModel.MonthyCharts.Add(monthyChart);
            //indexViewModel.MonthyCharts.Add(monthyChart);
            //indexViewModel.MonthyCharts.Add(monthyChart);




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
            return View();
        }

        public ActionResult ChemicalDetails()
        {
            return View();
        }

        public ActionResult GlasswareDetails()
        {
            return View();
        }

        public ActionResult UserManagement()
        {
            return View();
        }

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