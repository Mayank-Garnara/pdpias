using PDPIAS_management.Features;
using PDPIAS_management.Models.Person;
using PDPIAS_management.Models.Persons;
using PDPIAS_STORE;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;


namespace PDPIAS_management.Controllers
{
    public class RegistrationController : Controller
    {
        public RegistrationController()
        {
            var list = db.departments.Select(
                d => new SelectListItem
                {
                    Text = d.department_name,
                    Value = d.Id.ToString()
                }
            ).ToList();

            ViewBag.Departments = list;
        }

        db_pdpiasEntities2 db = new db_pdpiasEntities2();

        public class DepartmentPasser
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }

        // GET: Registration
        public ActionResult Index()
        {
            var list = db.departments.Select(
                d => new SelectListItem{
                    Text = d.department_name ,
                    Value = d.Id.ToString()
                }
            ).ToList();

            ViewBag.Departments = list;

            return View("Registration");
        }

        [HttpPost]
        //Regiter head
        public ActionResult RegisterHead(ViewModelWrapper departmentHeadViewModel)
        {
            
            if (ModelState.IsValid)
            {
                TempData["RegisterData"] = departmentHeadViewModel;
                

                string userEmail = departmentHeadViewModel.DepartmentHead.Email;  // <-- Receiver's email
                string otp = SendOtp.GenerateOtp();

                SendOtp.SendOtpEmail(userEmail, otp);  // <-- Passing receiver email here

                Session["OTP"] = otp;

                return View("ValidateOtp" , departmentHeadViewModel);
            }
            else
            {
                var list = db.departments.Select(
                d => new SelectListItem{
                    Text = d.department_name ,
                    Value = d.Id.ToString()
                }
                ).ToList();
                ViewBag.Departments = list;
                return View("Registration" , departmentHeadViewModel);
            }
        }

        public ActionResult RegisterLabTechnician(ViewModelWrapper labTechnicianViewModel)
        {
            if (ModelState.IsValid)
            {
                TempData["RegisterData"] = labTechnicianViewModel;

                string userEmail = labTechnicianViewModel.LabTechnician.Email;  // <-- Receiver's email
                string otp = SendOtp.GenerateOtp();

                SendOtp.SendOtpEmail(userEmail, otp);  // <-- Passing receiver email here

                Session["OTP"] = otp;

                return View("ValidateOtp", labTechnicianViewModel);
            }
            else
            {
                return View("Registration", labTechnicianViewModel);
            }
        }

        public ActionResult RegisterHeadStoreManager(ViewModelWrapper headStoreManagerViewModel)
        {
            if (ModelState.IsValid)
            {
                TempData["RegisterData"] = headStoreManagerViewModel;


                string userEmail = headStoreManagerViewModel.HeadStore.Email;  // <-- Receiver's email
                string otp = SendOtp.GenerateOtp();

                SendOtp.SendOtpEmail(userEmail, otp);  // <-- Passing receiver email here

                Session["OTP"] = otp;

                return View("ValidateOtp", headStoreManagerViewModel);
            }
            else
            {
                var list = db.departments.Select(
                d => new SelectListItem
                {
                    Text = d.department_name,
                    Value = d.Id.ToString()
                }
                ).ToList();
                ViewBag.Departments = list;
                return View("Registration", headStoreManagerViewModel);
            }
        }


        public ActionResult ValidateOtp(String FullOtp)
        {
            ViewModelWrapper UserData = (ViewModelWrapper) TempData["RegisterData"];
            if(UserData != null)
            {
                //checks which is the user is this
                String WhichUser = "";
                if(UserData.LabTechnician != null)
                {
                    WhichUser = "LabTech";
                }
                
                else if (UserData.DepartmentHead != null)
                {
                    WhichUser = "DepartmentHead";
                }
                
                else if(UserData.HeadStore != null)
                {
                    WhichUser = "HeadStore";
                }

                
                if (WhichUser == "")
                {
                    return View("Registration");
                }
                
                else
                {
                    if (Session["OTP"] != null)
                    {
                        if (Session["OTP"].Equals(FullOtp))
                        {
                            if (WhichUser.Equals("LabTech")){
                                try
                                {
                                    string filePath = null;
                                    string uniqueFileName = "";
                                    if (UserData.LabTechnician.ProfilePic != null && UserData.LabTechnician.ProfilePic.ContentLength > 0)
                                    {
                                        // Ensure the uploads folder exists
                                        string uploadFolder = Server.MapPath("~/uploads/DepartmentHead/");
                                        if (!Directory.Exists(uploadFolder))
                                        {
                                            Directory.CreateDirectory(uploadFolder);
                                        }

                                        // Get the file name and path
                                        string fileName = Path.GetFileName(UserData.LabTechnician.ProfilePic.FileName);
                                        if (Path.GetExtension(fileName) == ".jpeg" || Path.GetExtension(fileName) == ".jpg" || Path.GetExtension(fileName) == ".png")
                                        {
                                            uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                                            filePath = Path.Combine(uploadFolder, uniqueFileName);

                                            // Save the file to the server
                                            UserData.LabTechnician.ProfilePic.SaveAs(filePath);
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("ProfilePic", "Only .jpeg , .jpg , .png are allowed");
                                            return View("Registration", UserData.LabTechnician);
                                        }
                                    }

                                    lab_technician newTechnician = new lab_technician()
                                    {
                                        contact_no = UserData.LabTechnician.ContactNo,
                                        first_name = UserData.LabTechnician.FirstName,
                                        last_name = UserData.LabTechnician.LastName,
                                        email = UserData.LabTechnician.Email,
                                        password = BCryptConverter.EncryptPassword(UserData.LabTechnician.Password),
                                        profile_pic = uniqueFileName,
                                        department_id = UserData.LabTechnician.department_id,
                                        created_at = DateTime.Now, // IMPORTANT!
                                        is_active = false,
                                        status = 0
                                    };


                                    db.lab_technician.Add(newTechnician);

                                    db.SaveChanges();
                                    int newId = newTechnician.Id;

                                    db.users.Add(new user()
                                    {

                                        email = newTechnician.email,
                                        role = "lab_technician",
                                        user_id = newId
                                    });

                                    db.SaveChanges();
                                    return RedirectToAction("Index","Login");
                                }
                                catch (DbUpdateException ex)
                                {
                                    if (ex.InnerException?.Message.Contains("UNIQUE") == true ||
                                        ex.InnerException?.Message.Contains("duplicate") == true)
                                    {
                                        // Handle unique constraint violation
                                        ModelState.AddModelError("Email", "This email is already registered.");
                                    }
                                    else
                                    {
                                        // Other DB errors
                                        ModelState.AddModelError("", "An error occurred while saving principalData.");
                                    }
                                    return View("Registration", UserData.LabTechnician);
                                }
                                //for picture exception 
                                catch (NotImplementedException ex)
                                {
                                    ModelState.AddModelError("ProfilePic", "Please upload valid photo");
                                    return View("Registration", UserData.LabTechnician);
                                }
                                //for password incryption error
                                catch (ArgumentException ex)
                                {
                                    ModelState.AddModelError("Password", "Please enter valid password");
                                    return View("Registration", UserData.LabTechnician);
                                }
                            }
                            
                            else if (WhichUser.Equals("DepartmentHead"))
                            {
                                var list = db.departments.Select(
                                    d => new SelectListItem
                                    {
                                        Text = d.department_name,
                                        Value = d.Id.ToString()
                                    }
                                 ).ToList();


                                ViewBag.Departments = list;
                                string filePath = null;
                                string uniqueFileName = "";
                                try
                                {
                                    
                                    if (UserData.DepartmentHead.ProfilePic != null && UserData.DepartmentHead.ProfilePic.ContentLength > 0)
                                    {
                                        
                                        // Ensure the uploads folder exists
                                        string uploadFolder = Server.MapPath("~/uploads/DepartmentHead/");
                                        if (!Directory.Exists(uploadFolder))
                                        {
                                            Directory.CreateDirectory(uploadFolder);
                                        }

                                        // Get the file name and path
                                        string fileName = Path.GetFileName(UserData.DepartmentHead.ProfilePic.FileName);
                                        if (Path.GetExtension(fileName) == ".jpeg" || Path.GetExtension(fileName) == ".jpg" || Path.GetExtension(fileName) == ".png")
                                        {
                                            string extension = Path.GetExtension(fileName); // e.g., ".jpg"

                                            // Generate a timestamp + random string
                                            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");     // 14 chars
                                            string random = Path.GetRandomFileName().Replace(".", "")       // Random string
                                                              .Substring(0, 8);                              // 8 chars

                                            uniqueFileName = $"{timestamp}_{random}{extension}";     // Total: ~27–30 chars

                                            // Combine with the upload folder path
                                            filePath = Path.Combine(uploadFolder, uniqueFileName);

                                            // Save the file
                                            UserData.DepartmentHead.ProfilePic.SaveAs(filePath);

                                            // Save the file to the server
                                            UserData.DepartmentHead.ProfilePic.SaveAs(filePath);
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("ProfilePic", "Only .jpeg , .jpg , .png are allowed");
                                            return View("Registration", UserData.DepartmentHead);
                                        }
                                    }

                                    department_head newDepartmentHead = new department_head()
                                    {
                                        first_name = UserData.DepartmentHead.FirstName,
                                        last_name = UserData.DepartmentHead.LastName,
                                        contact_no = UserData.DepartmentHead.ContactNo,
                                        email = UserData.DepartmentHead.Email,
                                        password = BCryptConverter.EncryptPassword(UserData.DepartmentHead.Password),
                                        profile_pic = uniqueFileName,
                                        department_id = UserData.DepartmentHead.DepartmentId,
                                        created_at = DateTime.Now, // IMPORTANT!
                                        is_active = false,
                                        status = 0
                                    };

                                    
                                    db.department_head.Add(newDepartmentHead);
                                    
                                    db.SaveChanges();
                                    
                                    int newId = newDepartmentHead.Id;

                                    db.users.Add(new user()
                                    {

                                        email = newDepartmentHead.email,
                                        role = "department_head",
                                        user_id = newId
                                    });

                                    db.SaveChanges();
                                    
                                    return RedirectToAction("Index" , "Login");
                                }
                                catch (DbUpdateException ex)
                                {
                                    if (ex.InnerException.InnerException?.Message.Contains("UNIQUE") == true ||
                                        ex.InnerException?.Message.Contains("duplicate") == true)
                                    {
                                        System.IO.File.Delete(filePath);
                                        ViewBag.Departments = list;
                                        ViewBag.Tab = "DepartmentHead";
                                        ModelState.AddModelError("DepartmentHead.Email", "This email is already registered.");
                                    }
                                    else
                                    {
                                        // Other DB errors
                                        ViewBag.Departments = list;
                                        ViewBag.Tab = "DepartmentHead";
                                        ModelState.AddModelError("DepartmentHead.Email", "An error occurred while saving principalData.");
                                    }
                                    return View("Registration", UserData);
                                }
                                //for picture exception 
                                catch (NotImplementedException ex)
                                {
                                    ViewBag.Departments = list;
                                    ModelState.AddModelError("ProfilePic", "Please upload valid photo");
                                    return View("Registration", UserData);
                                }
                                //for password incryption error
                                catch (ArgumentException ex)
                                {
                                    ViewBag.Departments = list;
                                    ModelState.AddModelError("Password", "Please enter valid password");
                                    return View("Registration", UserData);
                                }
                            }
                            
                            else if (WhichUser.Equals("HeadStore"))
                            {
                                try
                                {
                                    string filePath = null;
                                    string uniqueFileName = "";
                                    if (UserData.HeadStore.ProfilePic != null && UserData.HeadStore.ProfilePic.ContentLength > 0)
                                    {
                                        // Ensure the uploads folder exists
                                        string uploadFolder = Server.MapPath("~/uploads/StoreManager/");
                                        if (!Directory.Exists(uploadFolder))
                                        {
                                            Directory.CreateDirectory(uploadFolder);
                                        }

                                        // Get the file name and path
                                        string fileName = Path.GetFileName(UserData.HeadStore.ProfilePic.FileName);
                                        if (Path.GetExtension(fileName) == ".jpeg" || Path.GetExtension(fileName) == ".jpg" || Path.GetExtension(fileName) == ".png")
                                        {
                                            uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                                            filePath = Path.Combine(uploadFolder, uniqueFileName);
                                            // Save the file to the server
                                            try
                                            {
                                                 UserData.HeadStore.ProfilePic.SaveAs(filePath);
                                            }
                                            catch (Exception)
                                            {
                                                ModelState.AddModelError("ProfilePic", "Something went wrong , please try again ");
                                                return View("Registration");
                                            }
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("ProfilePic", "Only .jpeg , .jpg , .png are allowed");
                                            return View("Registration", UserData.HeadStore);
                                        }
                                    }

                                    store_manager newStoreHead = new store_manager()
                                    {
                                        contact_no = UserData.HeadStore.ContactNo,
                                        first_name = UserData.HeadStore.FirstName,
                                        last_name = UserData.HeadStore.LastName,
                                        email = UserData.HeadStore.Email,
                                        password = BCryptConverter.EncryptPassword(UserData.HeadStore.Password),
                                        profile_pic = uniqueFileName,
                                        created_at = DateTime.Now, // IMPORTANT!
                                        is_active = false,
                                        status = 0
                                    };

                                    db.store_manager.Add(newStoreHead);
                                    db.SaveChanges();

                                    int newId = newStoreHead.Id;

                                    db.users.Add(new user()
                                    {

                                        email = newStoreHead.email,
                                        role = "head_store_manager",
                                        user_id = newId
                                    });

                                    db.SaveChanges();

                                    return RedirectToAction("Index" , "Login");
                                }
                                catch (DbUpdateException ex)
                                {
                                    if (ex.InnerException?.Message.Contains("UNIQUE") == true ||
                                        ex.InnerException?.Message.Contains("duplicate") == true)
                                    {
                                        // Handle unique constraint violation
                                        ModelState.AddModelError("Email", "This email is already registered.");
                                    }
                                    else
                                    {
                                        // Other DB errors
                                        ModelState.AddModelError("", "An error occurred while saving principalData.");
                                    }
                                    return View("Registration", UserData.HeadStore);
                                }
                                //for picture exception 
                                catch (NotImplementedException ex)
                                {
                                    ModelState.AddModelError("ProfilePic", "Please upload valid photo");
                                    return View("Registration", UserData.HeadStore);
                                }
                                //for password incryption error
                                catch (ArgumentException ex)
                                {
                                    ModelState.AddModelError("Password", "Please enter valid password");
                                    return View("Registration", UserData.HeadStore);
                                }
                            }
                            
                            else
                            {
                                ModelState.AddModelError("ServerError", "Something went wrong please try agian !");
                                return View("ValidateOtp");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("FullOtp", "Please enter valid otp");
                            return View("ValidateOtp");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("FullOtp", "Please enter valid otp");
                        return View("ValidateOtp");
                    }
                }
            }
            else
            {
                return View("Registration");
            }
            //return View("Registration", UserData);
        }
    }
}