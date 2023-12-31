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
        [StringLength(100, ErrorMessage = "The Permalink must be a maximum length of 100 characters.")]
        public string? Permalink { get; set; }

        
        public string? Image { get; set; }
    }

}
