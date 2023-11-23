using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QuoteGeneratorAPI.Models;
using System.Collections.Generic;

namespace QuoteGeneratorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Controller route prefix
    [DisableCors]
    public class QuoteAPIController : ControllerBase
    {
        private readonly QuoteManager _quoteManager;

        public QuoteAPIController()
        {
            _quoteManager = new QuoteManager();
        }

        // GET: api/quotes
        [HttpGet]
        public ActionResult<IEnumerable<Quote>> Get()
        {
            return Ok(_quoteManager.GetQuotes());
        }

        // GET: api/quotes/5
        [HttpGet("{id}")] 
        public ActionResult<Quote> Get(int id)
        {
            var quote = _quoteManager.GetQuoteById(id);
            if (quote == null)
            {
                return NotFound();
            }
            return Ok(quote);
        }

        // POST: api/quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            _quoteManager.AddQuote(quote);
            return Ok();
        }

        // PUT: api/quotes/5
        [HttpPut("{id}")] // Simplified route template
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            _quoteManager.UpdateQuote(id, quote);
            return Ok();
        }

        // DELETE: api/quotes/5
        [HttpDelete("{id}")] // Simplified route template
        public IActionResult Delete(int id)
        {
            _quoteManager.DeleteQuote(id);
            return Ok();
        }
    }
}
