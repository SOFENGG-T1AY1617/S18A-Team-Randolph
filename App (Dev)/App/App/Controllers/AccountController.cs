using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;
using System.Web.Routing;

namespace App.Controllers
{
    public class AccountController : Controller
    {
        AccountManager manager = new AccountManager();

        public ActionResult Index()
        {
            // working
            // must go to home index
            return RedirectToAction("Index", "Home");
        }

        public ActionResult login()
        {
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        public ActionResult regerror()
        {
            return View();
        }

        public ActionResult error()
        {
            return View();
        }

        [HttpPost]
        public ActionResult verify(string email, string password)
        {
            ViewBag.String = "email: " + email + " password: " + password;
            

            var acc = manager.getAccount(email, password);

            // Means acc does not exist 
            if (acc == null)
            {
                return RedirectToAction("error", "Account");
            }
            else
            {
                Session["user"] = acc;
                return RedirectToAction("order", "Transaction");

            }
        }

        [HttpPost]
        public ActionResult save(string idNumber, string lastName, string firstName, string middleName, string gender, string birthday, string citizenship, 
            string birthPlace, string address,string phoneNumber, string altPhoneNumber, string email, string altEmail, string password, string rePassword)
        {
            int errorCtr = 0;

            Account acc = new Account();

            if (!(idNumber == ""))
            {
                acc.idNumber = idNumber;
            }

            if(!(lastName == ""))
            {
                acc.lastName = lastName;
            } else
            {
                errorCtr++;
                ViewBag.lastNameError = "Please enter your last name";
            }
            
            if(!(firstName == ""))
            {
                acc.firstName = firstName;
            } else
            {
                errorCtr++;
                ViewBag.firstNameError = "Please enter your first name";
            }
            
            if(!(middleName == ""))
            {
                acc.middleName = middleName;
            } else
            {
                errorCtr++;
                ViewBag.middleNameError = "Please enter your middle name";
            }

            if (!(gender == ""))
            {
                if (gender == "Male")
                    acc.gender = 'M';
                else acc.gender = 'F';
            } else
            {
                errorCtr++;
                ViewBag.genderError = "Please select your gender";
            }

            if(!(birthday == ""))
            {
                string year = birthday[0] + "" + birthday[1] + "" + birthday[2] + "" + birthday[3] + "";
                string month = birthday[5] + "" + birthday[6] + "";
                string day = birthday[8] + "" + birthday[9] + "";

                acc.birthYear = Int32.Parse(year);
                acc.birthMonth = Int32.Parse(month);
                acc.birthDay = Int32.Parse(day);
            } else
            {
                errorCtr++;
                ViewBag.birthdayError = "Please select your birthday";
            }

            if (!(citizenship == ""))
            {
                acc.citizenship = citizenship;
            } else
            {
                errorCtr++;
                ViewBag.citizenshipError = "Please select your citizenship";
            }

            if (!(email == ""))
            {
                acc.email = email;
            } else
            {
                errorCtr++;
                ViewBag.emailError = "Please enter your email address";
            }

            acc.alternateEmail = altEmail;

            if (!(password == ""))
            {
                if (password == rePassword)
                {
                    acc.password = password;
                } else
                {
                    errorCtr++;
                    ViewBag.passwordError = "Password do not match";
                }
            } else
            {
                errorCtr++;
                ViewBag.passwordError = "Please enter your password";
            }

            if (!(birthPlace == ""))
            {
                acc.placeOfBirth = birthPlace;
            } else
            {
                errorCtr++;
                ViewBag.birthPlaceError = "Please enter your birthplace";
            }
            
            if (!(address == ""))
            {
                acc.currentAddress = address;
            } else
            {
                errorCtr++;
                ViewBag.addressError = "Please enter your address";
            }
            
            if (!(phoneNumber == ""))
            {
                acc.phoneNo = phoneNumber;
            } else
            {
                errorCtr++;
                ViewBag.phoneNumError = "Please enter your phone number";
            }

            acc.alternatePhoneNo = altPhoneNumber;    
            
            if (errorCtr == 0)
            {
                acc = manager.saveAccount(acc);
                Session["user"] = acc;
                return RedirectToAction("Order", "Transaction"); // go to next step
            } else
            {
                return View("register", acc);
            }
        }
    }
}