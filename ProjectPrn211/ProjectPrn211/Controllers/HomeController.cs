using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPrn211.Models;
using System.Diagnostics;

namespace ProjectPrn211.Controllers
{
    public class HomeController : Controller
    {
        private readonly CenimaDBContext _db;


        public HomeController(CenimaDBContext db, IHttpContextAccessor httpContextAccessor)
        {
            this._db = db;
        }

     

        public IActionResult Index()
        {
            var listGenre =_db.Genres.ToList();
            var listMovie = _db.Movies.Include(l => l.Rates).Include(g => g.Genre).ToList();
            ViewBag.Genre = listGenre;
            return View(listMovie);
        }

        public IActionResult Filter([FromQuery] string actionName)
        {
            var listGenre = _db.Genres.ToList();
            var listMovie = _db.Movies.Include(l => l.Rates).Include(g => g.Genre)
                            .Where(n => n.Genre.Description == actionName).ToList();
            ViewBag.Genre = listGenre;
            return View("Index",listMovie);
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            var listGenre = _db.Genres.ToList();
            var listMovie = _db.Movies.Include(l => l.Rates).Include(g => g.Genre)
                            .Where(n => n.Title.Contains(search)).ToList();
            ViewBag.Genre = listGenre;
            return View("Index", listMovie);
        }


    }
}