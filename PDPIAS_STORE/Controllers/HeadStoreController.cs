using PDPIAS_STORE.Models.headStoreModel;
using PDPIAS_STORE.Models.headStoreModel.ViewModel;
using PDPIAS_STORE.Models.Pricipal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PDPIAS_STORE.Controllers
{
    public class HeadStoreController : Controller
    {
       private db_pdpiasEntities2 _context = new db_pdpiasEntities2();
        public ActionResult Index()
        {
            return View();
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
        public ActionResult AddChemical()
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

        public ActionResult CurrentGlasswareStock()
        {
            return View();
        }

        

        private void PopulateAllLookups()
        {
            // 1. CHEMICALS (chemical table) -> Binds to chemical_id
            ViewBag.ChemicalList = _context.chemicals
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.name + " (" + c.cas_number + ")"
                })
                .ToList();

            // 2. MEASUREMENT UNIT (measurement_unit table) -> Binds to measurement_unit_id
            ViewBag.MeasurementUnitList = _context.measurement_unit
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.unit
                })
                .ToList();

            // 3. CHEMICAL TYPE (chemical_type table) -> Binds to chemical_type_id
            ViewBag.ChemicalTypeList = _context.chemical_type
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.type
                })
                .ToList();

            // 4. CHEMICAL STATE (chemical_state table) -> Binds to chemical_state_id
            ViewBag.ChemicalStateList = _context.chemical_state
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.chemical_state1
                })
                .ToList();

            // 5. HEAD/STORE MANAGER (store_manager table) -> Binds to head_id
            ViewBag.HeadList = _context.store_manager
                .Select(h => new SelectListItem
                {
                    Value = h.Id.ToString(),
                    Text = h.first_name + " " + h.last_name
                })
                .ToList();
        }


        // --- [HttpGet] AddStock (Form Display) ---
        [HttpGet]
        public ActionResult AddStock()
        {
            PopulateAllLookups();
            // Return the MainChemicalStorage model
            return View(new main_chemical_storage());
        }


        // --- [HttpPost] AddStock (Form Submission & Save) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStock(main_chemical_storage chemicalStorage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Set the creation date before saving
                    chemicalStorage.created_at = DateTime.Now;

                    // EntitySet name 'main_chemical_storage' from EDMX.
                    // NOTE: Your DbContext property name might be different (e.g., MainChemicalStorages)
                    _context.main_chemical_storage.Add(chemicalStorage);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Database error occurred while saving stock: " + ex.Message);
                }
            }

            // Agar validation fail ho, toh ViewBag ko dobara load karein
            PopulateAllLookups();

            return View(chemicalStorage);
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
        [HttpPost]
        public ActionResult AddChemical(Chemical chemical)
        {
            // 1. Check if the model is valid (based on [Required] attributes, etc.)
            if (ModelState.IsValid)
            {


                // 2. Add the new entity to the DbContext
                chemical db_chemicals = new chemical()
                {
                    name = chemical.name,
                    cas_number = chemical.cas_number,
                    image_structure = chemical.image_structure,
                    molicular_formula = chemical.molicular_formula,
                    molicular_weight = chemical.molicular_weight,
                };
                _context.chemicals.Add(db_chemicals);

                // 3. Save the changes to the database
                _context.SaveChanges();

                // 4. Redirect the user to another page (e.g., a list of chemicals)
                return RedirectToAction("Index"); // Assuming you have an Index action
            }

            // If ModelState is not valid, return the form with validation errors
            return View(chemical);
        }
        [HttpPost]
        public ActionResult Create(MainChemicalStorage chemical)
        {
            if (ModelState.IsValid)
            {
                main_chemical_storage db_chemical = new main_chemical_storage()
                {
                    head_id = chemical.head_id,
                    chemical_id = chemical.Id,
                    measurement_unit_id = chemical.measurement_unit_id,
                    original_quantity = chemical.original_quantity,
                    current_quantity = chemical.current_quantity,
                    created_at = chemical.created_at,
                    expiry_date = chemical.expiry_date,
                    manufacture_date =  chemical.manufacture_date,
                    molecular_weight = chemical.molecular_weight,
                    storage_condition = chemical.storage_condition,
                    hazard_level = chemical.hazard_level,
                    supplier_company_name = chemical.supplier_company_name,
                    chemical_type_id = chemical.chemical_type_id,
                    chemical_state_id = chemical.chemical_state_id,
                };

                _context.main_chemical_storage.Add(db_chemical);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("AddChemical", ModelState);
        }
    }
}