using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Project.Data;
using Movie_Project.Models;



namespace Movie_Project.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieDBContext _context;
        private readonly IWebHostEnvironment _environment;

        public MovieController(MovieDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        // GET: Movies
        public IActionResult Index(int page = 1)
        {

            var movieDBContext = _context.Movies.Include(m => m.gener);
            return View(movieDBContext.Skip(((page - 1) * 10)).Take(10).ToList());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.gener)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        //[Authorize]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("IDAdmin") != null)
            {
                ViewData["GenerId"] = new SelectList(_context.Geners, "Id", "genre");
                ViewData["GenerName"] = new SelectList(_context.Geners, "Id", "genre");

                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Login");
            }
            
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize]       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,Image,actors,FilmProducer,Description,YearOfRelease,rating,Duartion,WatchLink,DownloadeLink,GenerId")] Movie movie, IFormFile img_file)
        {
            string path = Path.Combine(_environment.WebRootPath, "Img"); // wwwroot/Img/
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (img_file != null)
            {
                path = Path.Combine(path, img_file.FileName); // for exmple : /Img/Photoname.png
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await img_file.CopyToAsync(stream);
                    //ViewBag.Message = string.Format("<b>{0}</b> uploaded.</br>", img_file.FileName.ToString());
                    movie.Image = img_file.FileName;

                }
            }
            else
            {
                movie.Image = "default.jpeg"; // to save the default image path in database.
            }
            try
            {
                _context.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("Index", "Movie");
            }
            catch (Exception ex) { ViewBag.exc = ex.Message; }
            ViewData["GenerId"] = new SelectList(_context.Geners, "Id", "genre", movie.GenerId);
            //ViewData["GenerName"] = new SelectList(_context.Geners, "genre", "genrename", movie.GenerName);

            return View();
            //if (ModelState.IsValid)
            //{

            //    _context.Add(movie);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction("Index", "Movie");
            //}
            //ViewData["GenerId"] = new SelectList(_context.Geners, "Id", "genre", movie.GenerId);
            //return View(movie);


        }

        // GET: Movies/Edit/5
        //[Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("IDAdmin") != null)
            {
                if (id == null || _context.Movies == null)
                {
                    return NotFound();
                }

                var movie = await _context.Movies.FindAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }
                ViewData["GenerId"] = new SelectList(_context.Geners, "Id", "genre", movie.GenerId);
                return View(movie);
            }
            else
            {
                return RedirectToAction("LogIn", "Login");
            }
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,Image,actors,FilmProducer,Description,YearOfRelease,rating,Duartion,WatchLink,DownloadeLink,GenerId")] Movie movie) //IFormFile img_file
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }
            //string path = Path.Combine(_environment.WebRootPath, "Img");
            //if (img_file != null)
            //{
            //    path = Path.Combine(path, img_file.FileName); // for exmple : /Img/Photoname.png
            //    using (var stream = new FileStream(path, FileMode.Create))
            //    {
            //        await img_file.CopyToAsync(stream);
            //        //ViewBag.Message = string.Format("<b>{0}</b> uploaded.</br>", img_file.FileName.ToString());
            //        movie.Image = img_file.FileName;

            //    }
            //}
            //else
            //{
            //    movie.Image = "default.jpeg"; // to save the default image path in database.
            //}
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexAdmin","Movie");
            }
            ViewData["GenerId"] = new SelectList(_context.Geners, "Id", "genre", movie.GenerId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        //[Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("IDAdmin") != null)
            {
                if (id == null || _context.Movies == null)
                {
                    return NotFound();
                }

                var movie = await _context.Movies
                    .Include(m => m.gener)
                    .FirstOrDefaultAsync(m => m.MovieId == id);
                if (movie == null)
                {
                    return NotFound();
                }

                return View(movie);
            }
            else
            {
                return RedirectToAction("LogIn", "Login");
            }
        }

        // POST: Movies/Delete/5
        //[Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'MovieDBContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("IndexAdmin","Movie");
        }

        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }



        //public IActionResult Search()
        //{

        //    return View();

        //}
        ////filter Movies with Year
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Search(string Title,int YearOfRelease, string Gener)
        //{
        //    var search =  _context.Movies.ToList();
        //    TempData["Title"] =" ";
        //    TempData["Year"] = " ";
        //    TempData["Gener"] = " ";
        //    if (Title != null)
        //    {
        //         search = search.Where(x => x.Title.Contains(Title)).ToList();
        //        TempData["Title"] = Title;

        //    }
        //    if (YearOfRelease != null)
        //    {
        //         search = search.Where(x=>x.YearOfRelease==YearOfRelease).ToList();
        //        TempData["Year"] = YearOfRelease;
        //    }
        //    if(Gener!=null)
        //    {
        //        var selectedgener = getGener(Gener);
        //        int generID = selectedgener.Id;

        //        search = search.Where(x => x.GenerId == generID).ToList();
        //        TempData["Gener"] = Gener;
        //    }
        //    return View(search);

        public IActionResult Search()
        {

            return View();

        }
        //filter Movies with Year
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string Title)
        {

            var search = _context.Movies.Where(x => x.Title.Contains(Title)).ToList();
            TempData["Title"] = Title;

            return View(search);

        }


        public IActionResult FilterByYear()
        {

            return View();

        }
        //filter Movies with Year
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilterByYear(int YearOfRelease)
        {

            var filter1 = _context.Movies.Where(x => x.YearOfRelease == YearOfRelease).ToList();
            TempData["Year"] = YearOfRelease;
            return View(filter1);

        }
        public IActionResult FilterByGener()
        {
            return View();
        }
        //filter Movies with Gener
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilterByGener(string Gener)
        {
            try
            {
                var selectedgener = getGener(Gener);
                int generID = selectedgener.Id;

                var filter2 = _context.Movies.Where(x => x.GenerId == generID).ToList();
                TempData["Gener"] = Gener;
                return View(filter2);
            }catch (Exception ex) 
            {
                ViewBag.exc = ex.Message;

            }


            return View();

        }

        private Gener getGener(string Gener)
        {

            var gener = _context.Geners.FirstOrDefault(u => u.genre == Gener);

            //var geners = _context.Geners.Where(x=>x.genre==Gener);

            return gener;
        }

        //[Authorize]
        public async Task<IActionResult> IndexAdmin(int page = 1)
        {
            if (HttpContext.Session.GetInt32("IDAdmin") != null)
            {
                var movieDBContext = _context.Movies.Include(m => m.gener);
                return View(movieDBContext.Skip(((page - 1) * 10)).Take(10).ToList());

                //return _context.Movies != null ?
                //              View(await _context.Movies.ToListAsync()) :
                //              Problem("Entity set 'MovieDBContext.Movies'  is null.");
                //return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Login");
            }
        }
        //[Authorize]
        public async Task<IActionResult> IndexUser(int page = 1)
        {
            if (HttpContext.Session.GetInt32("IDUser") != null)
            {
                var movieDBContext = _context.Movies.Include(m => m.gener);
                return View(movieDBContext.Skip(((page - 1) * 10)).Take(10).ToList());
                //return _context.Movies != null ?
                //              View(await _context.Movies.ToListAsync()) :
                //              Problem("Entity set 'MovieDBContext.Movies'  is null.");
                //return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Login");
            }

        }

    }

}
