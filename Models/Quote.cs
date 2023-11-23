using System.ComponentModel.DataAnnotations;

namespace QuoteGeneratorAPI.Models
{
    public class Quote
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Author { get; set; }
        public string QuoteText { get; set; }

        [MaxLength(100)]
        // [Url(ErrorMessage = "The Permalink must be a valid URL.")]
        public string Permalink { get; set; } = "";

        [MaxLength(100)]
        public string Image { get; set; } = "";
    }

}
