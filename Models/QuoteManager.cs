using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QuoteGeneratorAPI.Models
{
    public class QuoteManager
    {
        private const string ConnectionString = "Server=localhost;port=3306;Database=quotes;Uid=root;Pwd=secret;SslMode=none;";
        private const string ImageFolderPath = "wwwroot/images"; // Adjust as needed

        public List<Quote> GetQuotes()
        {
            var quotes = new List<Quote>();
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Quotes";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var quote = new Quote
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Author = reader["Author"].ToString(),
                            QuoteText = reader["QuoteText"].ToString(),
                            Permalink = reader["Permalink"].ToString(),
                            Image = reader["Image"].ToString()
                        };
                        quotes.Add(quote);
                    }
                }
            }
            return quotes;
        }

        public void AddQuote(Quote quote)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                var query = "INSERT INTO Quotes (Author, QuoteText, Permalink, Image) VALUES (@Author, @QuoteText, @Permalink, @Image)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Author", quote.Author);
                    command.Parameters.AddWithValue("@QuoteText", quote.QuoteText);
                    command.Parameters.AddWithValue("@Permalink", quote.Permalink);
                    command.Parameters.AddWithValue("@Image", quote.Image);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Quote GetQuoteById(int id)
        {
            Quote quote = null;
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Quotes WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            quote = new Quote
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Author = reader["Author"].ToString(),
                                QuoteText = reader["QuoteText"].ToString(),
                                Permalink = reader["Permalink"].ToString(),
                                Image = reader["Image"].ToString()
                            };
                        }
                    }
                }
            }
            return quote;
        }

        public void UpdateQuote(int id, Quote updatedQuote)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                var query = "UPDATE Quotes SET Author = @Author, QuoteText = @QuoteText, Permalink = @Permalink, Image = @Image WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Author", updatedQuote.Author);
                    command.Parameters.AddWithValue("@QuoteText", updatedQuote.QuoteText);
                    command.Parameters.AddWithValue("@Permalink", updatedQuote.Permalink);
                    command.Parameters.AddWithValue("@Image", updatedQuote.Image);
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteQuote(int id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                var query = "DELETE FROM Quotes WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return null;

            var fileName = Path.GetFileNameWithoutExtension(image.FileName);
            var extension = Path.GetExtension(image.FileName);
            var newFileName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}{extension}";
            var imagePath = Path.Combine(ImageFolderPath, newFileName);
            var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), imagePath);

            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)); // Ensure the directory exists

            using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return imagePath.Replace("wwwroot", ""); // Returns the relative path for web access
        }
    }
}
