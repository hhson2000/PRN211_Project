using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPrn211.Models;
using System.Text.Json;

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

        [HttpPost]
        public IActionResult Review(string rating, string comment, string MovieId)
        {
            var user = (Person)JsonSerializer.Deserialize<Person>(HttpContext.Session.GetString("Email"));
            var name = user.Email;
            var infor = _db.Persons.Select(c => new { Id = c.PersonId, Email = c.Email }).Where(c => c.Email == name).FirstOrDefault();
            _db.Add(new Rate
            {
                MovieId = Convert.ToInt32(MovieId),
                PersonId = infor.Id,
                Comment = comment,
                NumericRating = Convert.ToDouble(rating)
            });
            _db.SaveChanges();
            var detail = _db.Movies.Where(d => d.MovieId == Convert.ToInt32(MovieId)).Include(l => l.Rates).ThenInclude(c => c.Person)
                .Include(c => c.Genre).FirstOrDefault();
            return View("MovieDetail", detail);
        }
    }
}
