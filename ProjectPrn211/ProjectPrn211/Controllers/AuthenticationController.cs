using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectPrn211.Models;
using System.Text.Json;

namespace ProjectPrn211.Controllers
{
    

    public class AuthenticationController : Controller
    {
        private readonly CenimaDBContext _db;

        public AuthenticationController(CenimaDBContext db)
        {
            this._db = db;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Person person)
        {
            if(person != null)
            {
                string email = person.Email;
                string password = person.Password;
                var user = _db.Persons.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
                if (user != null)
                {
                    if(user.IsActive == false)
                    {
                        return NotFound();
                    }
                    else
                    {
                        string action = "";
                        string controller = "";
                        switch (user.Type)
                        {
                            case 1: action = "Index";
                                controller = "Admin";
                                break;
                            case 2: action = "Index";
                                controller = "Home";
                                break;
                        }
                        HttpContext.Session.SetString("Email", JsonSerializer.Serialize(person));
                        return RedirectToAction(action, controller);
                    }
                }
            }
            
            return View();
        }

        public IActionResult Register()
        {
            var genders = _db.Persons.Select(g => new {Gender = g.Gender}).Distinct().ToList();
            ViewBag.Genders = new SelectList(genders, "Gender", "Gender");
            return View();
        }

        [HttpPost]
        public IActionResult Register(Person person)
        {
            var genders = _db.Persons.Select(g => new { Gender = g.Gender }).Distinct().ToList();
            string email = person.Email;
            var user = _db.Persons.FirstOrDefault(x => x.Email.Equals(email));
            if(user != null)
            {
                ViewBag.Genders = new SelectList(genders, "Gender", "Gender");
                ViewBag.ErrorMsg = "Email đã tồn tại";
                return View("Register");
            }
            Person ps = new Person();
            ps.Fullname = person.Fullname;
            ps.Email = person.Email;
            ps.Password = person.Password;
            ps.Gender = person.Gender;
            ps.Type = 2;
            ps.IsActive = true;
            _db.Persons.Add(ps);
            _db.SaveChanges();
            return View("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}
