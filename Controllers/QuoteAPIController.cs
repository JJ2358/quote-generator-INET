using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QuoteGeneratorAPI.Models;
using System.Collections.Generic;

namespace QuoteGeneratorAPI.Controllers
{
    [ApiController]
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
        [Route("api/quotes")]
        public ActionResult<IEnumerable<Quote>> Get()
        {
            return Ok(_quoteManager.GetQuotes());
        }

        // GET: api/quotes/5
        [HttpGet]
        [Route("api/quotes/{id}")]
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
        [Route("api/quotes")]
        public IActionResult Post([FromBody] Quote quote)
        {
            _quoteManager.AddQuote(quote);
            return Ok();
        }

        // PUT: api/quotes/5
        [HttpPut]
        [Route("api/quotes/{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            _quoteManager.UpdateQuote(id, quote);
            return Ok();
        }

        // DELETE: api/quotes/5
        [HttpDelete]
        [Route("api/quotes/{id}")]
        public IActionResult Delete(int id)
        {
            _quoteManager.DeleteQuote(id);
            return Ok();
        }
    }
}
