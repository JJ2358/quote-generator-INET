using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QuoteGeneratorAPI.Models;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace QuoteGeneratorAPI.Controllers
{
    public class QuoteAdminController : Controller
    {
        private readonly QuoteManager _quoteManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<QuoteAdminController> _logger;

        public QuoteAdminController(ILogger<QuoteAdminController> logger)
        {
            _quoteManager = new QuoteManager();
            _logger = logger;
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
        [Route("admin/quotes/create")]
        public async Task<IActionResult> Create(Quote quote, IFormFile image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (image != null)
                    {
                        var imagePath = await _quoteManager.SaveImage(image);
                        quote.Image = imagePath;
                    }
                    _quoteManager.AddQuote(quote);
                    TempData["Message"] = "Quote added successfully";
                    return RedirectToAction("Index"); // Redirect to Index action
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Create method");
                ModelState.AddModelError("", "An error occurred while creating the quote.");
            }
            return RedirectToAction("Index"); // Redirect to Index even if there's an error
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
                _logger.LogError(ex, "Error occurred in Edit method"); // Log the error
                ModelState.AddModelError("", "An error occurred while editing the quote.");
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

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _quoteManager.DeleteQuote(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in DeleteConfirmed method"); // Log the error
                // Redirect to an error view or display a message
                return RedirectToAction("Error", new { message = "An error occurred while deleting the quote." });
            }
        }

        [HttpPost]
        [Route("admin/quotes/add")] // Unique route for AddQuote
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

        public IActionResult IndexAfterSubmit()
        {
            var quotes = _quoteManager.GetQuotes();
            return View("Index", quotes);
        }
    }
}