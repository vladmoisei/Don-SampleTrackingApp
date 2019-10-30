using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Don_SampleTrackingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Don_SampleTrackingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly RaportareDbContext _context;
        public HomeController(RaportareDbContext context)
        {
            _context = context;
        }
            
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string UserName, string Password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == UserName);
            if (user == null)
            {
                ViewBag.UserName = "";
                return View();
            }
            else if (user.Password != Password)
            {
                ViewBag.Password = "";
                return View();
            }

            // Salvam data user in session (pentru a utiliza in celelalte view-uri)
            HttpContext.Session.SetString("Id", user.UserId.ToString());
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("Nume", user.Nume);
            HttpContext.Session.SetString("Prenume", user.Prenume);
            HttpContext.Session.SetString("Rol", user.Rol.ToString());
            HttpContext.Session.SetString("IsEnable", user.IsEnable.ToString());

            //ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            //return Content(ViewBag.IsAdmin);
            //return RedirectToAction("Cuprins/" + user.UserName, "Home");
            return RedirectToAction("Index", "ProbaModels");





            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
