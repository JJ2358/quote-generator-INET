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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Quote quote)
        {
            _logger.LogInformation("Attempting to create a new quote with Author: {Author} and Text: {Text}", quote.Author, quote.QuoteText);

            if (ModelState.IsValid)
            {
                try
                {
                    _quoteManager.AddQuote(quote);
                    TempData["Message"] = $"Quote '{quote.Author} - {quote.QuoteText}' has been added.";
                    _logger.LogInformation("Quote successfully added.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while adding a quote");
                    TempData["Error"] = "An error occurred while adding the quote.";
                }
                
                return RedirectToAction(nameof(IndexAfterSubmit));
            }
            else
            {
                _logger.LogWarning("Model state is invalid. Quote not added.");
                TempData["Error"] = "Invalid quote data. Please check your input.";
                return RedirectToAction(nameof(Index));
            }
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
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var quote = _quoteManager.GetQuoteById(id);
                if (quote != null)
                {
                    _quoteManager.DeleteQuote(id);
                    TempData["Message"] = $"Quote '{quote.Author} - {quote.QuoteText}' has been deleted.";
                }
                else
                {
                    TempData["Error"] = "Quote not found.";
                }
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuote(Quote quote, IFormFile image)
        {
            // Basic logging to see the incoming data
            _logger.LogInformation($"Received quote: {quote.Author}, {quote.QuoteText}");

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle image upload if present
                    if (image != null && image.Length > 0)
                    {
                        var imagePath = await SaveImage(image);
                        quote.Image = imagePath;
                    }

                    // Add the quote to the database
                    _quoteManager.AddQuote(quote);
                    TempData["Message"] = $"Quote '{quote.Author} - {quote.QuoteText}' has been added.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred in AddQuote method");
                    ModelState.AddModelError("", "An error occurred while adding the quote.");
                }
            }
            else
            {
                // Log specific model state errors
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError($"Model state error: {error.ErrorMessage}");
                    }
                }
                TempData["Error"] = "Invalid quote data. Please check your input.";
            }

            return RedirectToAction(nameof(Index));
        }



        private async Task<string> SaveImage(IFormFile image)
        {
            var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var filePath = Path.Combine(uploadPath, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return Path.Combine("uploads", uniqueFileName);
        }

        public IActionResult IndexAfterSubmit()
        {
            var quotes = _quoteManager.GetQuotes();
           return RedirectToAction(nameof(Index));

        }
    }
}