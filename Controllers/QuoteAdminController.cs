using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QuoteGeneratorAPI.Models;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace QuoteGeneratorAPI.Controllers
{
    public class QuoteAdminController : Controller
    {
        private readonly QuoteManager _quoteManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public QuoteAdminController()
        {
            _quoteManager = new QuoteManager();
        }

        // GET: admin/quotes
        public IActionResult Index()
        {
            var quotes = _quoteManager.GetQuotes();
            return View(quotes);
        }

        // GET: admin/quotes/create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/quotes/create
        [HttpPost]
        public IActionResult Create(Quote quote, IFormFile image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _quoteManager.AddQuote(quote);
                    // Handle image upload if necessary
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Log the error (uncomment ex variable name and write a log.)
            }
            return View(quote);
        }

        // GET: admin/quotes/edit/5
        public IActionResult Edit(int id)
        {
            var quote = _quoteManager.GetQuoteById(id);
            if (quote == null)
            {
                return NotFound();
            }
            return View(quote);
        }

        // POST: admin/quotes/edit/5
        [HttpPost]
        public IActionResult Edit(int id, Quote quote)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _quoteManager.UpdateQuote(id, quote);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Log the error (uncomment ex variable name and write a log.)
            }
            return View(quote);
        }

        // GET: admin/quotes/delete/5
        public IActionResult Delete(int id)
        {
            var quote = _quoteManager.GetQuoteById(id);
            if (quote == null)
            {
                return NotFound();
            }
            return View(quote);
        }

        // POST: admin/quotes/delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _quoteManager.DeleteQuote(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddQuote(Quote quote, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    // Handle image saving here
                    var imagePath = await SaveImage(image);
                    quote.Image = imagePath;
                }

                _quoteManager.AddQuote(quote);
                return RedirectToAction("Index");
            }

            return View(quote);
        }
        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            var filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine("uploads", uniqueFileName);
        }
    }
}
