using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPrn211.Models;


namespace ProjectPrn211.Controllers
{
    public class AdminController : Controller
    {
        private readonly CenimaDBContext _db;

        public AdminController(CenimaDBContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            var person = _db.Persons.ToList();
            return View(person);
        }

        public IActionResult Deactive(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var person = _db.Persons.Where(p => p.PersonId == id).FirstOrDefault();
            person.IsActive = false;
            _db.SaveChanges();
            var personList = _db.Persons.ToList();
            return View("Index", personList);
        }

        public IActionResult Active(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var person = _db.Persons.Where(p => p.PersonId == id).FirstOrDefault();
            person.IsActive = true;
            _db.SaveChanges();
            var personList = _db.Persons.ToList();
            return View("Index", personList);
        }

        public IActionResult ManageMovie()
        {
            var movie = _db.Movies.ToList();
            return View(movie);
        }

        public IActionResult ManageGenre()
        {
            var genre = _db.Genres.ToList();
            return View(genre);
        }

        public IActionResult AddMovie()
        {
            var genres = _db.Genres.ToList();
            ViewData["Genres"] = genres;
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(Movie mv)
        {
            _db.Movies.Add(mv);
            _db.SaveChanges();
            return RedirectToAction(nameof(ManageMovie));
        }

        public IActionResult AddGenre()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddGenre(Genre gen)
        {
            _db.Genres.Add(gen);
            _db.SaveChanges();
            return RedirectToAction(nameof(ManageGenre));
        }

        public IActionResult DeleteMovie(int id)
        {
            _db.Remove(_db.Movies.Find(id));
            _db.SaveChanges();
            var movieList = _db.Movies.ToList();
            return View("ManageMovie", movieList);
        }

        public IActionResult DeleteGenre(int id)
        {
            _db.Remove(_db.Genres.Find(id));
            _db.SaveChanges();
            var movieGenre = _db.Genres.ToList(); 
            return View("ManageGenre", movieGenre);
        }

        public IActionResult EditMovie(int id)
        {
            var movieEdit = _db.Movies.Find(id);
            return View(movieEdit);
        }

        [HttpPost]
        public IActionResult EditMovie(Movie mv)
        {
            if (mv == null)
            {
                return NotFound();
            }
            var movieEdit = _db.Movies.Find(mv.MovieId);
            movieEdit.Title = mv.Title.Trim();
            movieEdit.Year = mv.Year;
            movieEdit.Description = mv.Description.Trim();
            movieEdit.Image = mv.Image.Trim();
            movieEdit.GenreId = mv.GenreId;
            _db.SaveChanges();
            var movieList = _db.Movies.ToList();
            return View("ManageMovie", movieList);
        }

        public IActionResult EditGenre(int id)
        {
            var genreEdit = _db.Genres.Find(id);
            return View(genreEdit);
        }

        [HttpPost]
        public IActionResult EditGenre(Genre gen)
        {
            if (gen == null)
            {
                return NotFound();
            }
            var genEdit = _db.Genres.Find(gen.GenreId);
            genEdit.Description = gen.Description.Trim();
            _db.SaveChanges();
            var genreList = _db.Genres.ToList();
            return View("ManageGenre", genreList);
        }
    }
}
