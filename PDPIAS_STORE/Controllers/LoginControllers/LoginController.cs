using PDPIAS_management.Features;
using PDPIAS_management.Models;
using PDPIAS_STORE;
using PDPIAS_STORE.Models.Pricipal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PDPIAS_management.Controllers
{
    public class LoginController : Controller
    {
        db_pdpiasEntities2 dbContext = new db_pdpiasEntities2();
        // GET: Login -- it will redirect user to login page
        public ActionResult Index()
        {
            return View("Login");
        }

        //for authentication - login
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                var userList = dbContext.users.FirstOrDefault(
                    each=>each.email == loginViewModel.email
                    );

                if(userList != null)
                {
                    String role = userList.role;
                    if(role != null)
                    {
                        if (role.Equals("principal"))
                        {
                            var principal = dbContext.principals.FirstOrDefault(
                                each => each.email == loginViewModel.email
                                );
                            if (principal != null && BCryptConverter.Varify(loginViewModel.password, principal.password))
                            {
                                Session["userData"] = principal;
                                return RedirectToAction("Index", "Principal");
                            }
                            else
                            {
                                ViewBag.LoginMessage = "Userid or password not match";
                                return View("Login" , loginViewModel);
                            }
                        }
                        else if (role.Equals("head_store_manager"))
                        {
                            var head_store_manager = dbContext.store_manager.FirstOrDefault(
                                each => each.email == loginViewModel.email
                                );
                            if (head_store_manager != null && BCryptConverter.Varify(loginViewModel.password, head_store_manager.password))
                            {
                                Session["userData"] = head_store_manager;
                                return RedirectToAction("Index","HeadStore");
                                //redirect user from here to dashboard
                            }
                            else
                            {
                                ViewBag.LoginMessage = "Userid or password not match";
                                return View("Login" , loginViewModel);
                            }
                        }
                        else if (role.Equals("department_head"))
                        {
                            var department_head = dbContext.department_head.FirstOrDefault(
                                each => each.email == loginViewModel.email
                                );
                            if (department_head != null && BCryptConverter.Varify(loginViewModel.password , department_head.password))
                            {
                                Session["userData"] = department_head;
                                return RedirectToAction("Index","DepartmentHead");
                                //redirect user from here to dashboard
                            }
                            else
                            {
                                ViewBag.LoginMessage = "Userid or password not match";
                                return View("Login", loginViewModel);
                            }
                        }
                        else if (role.Equals("lab_technician"))
                        {
                            var department_head = dbContext.lab_technician.FirstOrDefault(
                                each => each.email == loginViewModel.email
                                );
                            if (department_head != null && BCryptConverter.Varify(loginViewModel.password, department_head.password))
                            {
                                Session["userData"] = department_head;
                                return RedirectToAction("Index", "Welcome");
                                //redirect user from here to perticullar dashboard
                            }
                            else
                            {
                                ViewBag.LoginMessage = "Userid or password not match";
                                return View("Login", loginViewModel);
                            }
                        }
                        else
                        {
                            ViewBag.LoginMessage = "Something went wrong please try again";
                            return View("Login", loginViewModel);
                        }
                    }
                    else
                    {
                        ViewBag.LoginMessage = "Something went wrong ! please try again";
                        return View("Login", loginViewModel);
                    }
                }
                else
                {
                    ViewBag.LoginMessage = "Username or password not match";
                    return View("Login", loginViewModel);
                }
            }
            else
            {
                ViewBag.LoginMessage = "Please enter valid principalData";
                return View("Login", loginViewModel);
            }
        }
    }
}