using Microsoft.AspNetCore.Mvc;
using QuoteGeneratorAPI.Models;

namespace QuoteGeneratorAPI.Controllers
{
    public class QuoteAdminController : Controller
    {
        private readonly QuoteManager _quoteManager;

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

        // Other actions for Create, Update, Delete can be added here
        // These would typically return views for forms and handle form submissions
    }
}
