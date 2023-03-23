using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPrn211.Models;

namespace ProjectPrn211.Controllers
{
    public class MovieController : Controller
    {
        private readonly CenimaDBContext _db;
        public MovieController(CenimaDBContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MovieDetail(int id)
        {
            var detail = _db.Movies.Where(d => d.MovieId == id).Include(l => l.Rates).ThenInclude(c => c.Person)
                .Include(c => c.Genre).FirstOrDefault();
            return View(detail);
        }
    }
}
