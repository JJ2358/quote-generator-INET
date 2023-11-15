using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace QuoteGeneratorAPI.Models
{
    public class QuoteManager
    {
        private const string ConnectionString = "Server=localhost;port=3306;Database=quotes;Uid=root;Pwd=secret;SslMode=none;";

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
                            Id = Convert.ToInt32(reader["id"]),
                            Author = reader["author"].ToString(),
                            QuoteText = reader["QuoteText"].ToString(),
                            
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
                var query = "INSERT INTO tblQuotes (author, quote) VALUES (@Author, @QuoteText)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Author", quote.Author);
                    command.Parameters.AddWithValue("@QuoteText", quote.QuoteText);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateQuote(int id, Quote updatedQuote)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                var query = "UPDATE tblQuotes SET author = @Author, quote = @QuoteText WHERE id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Author", updatedQuote.Author);
                    command.Parameters.AddWithValue("@QuoteText", updatedQuote.QuoteText);
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
                var query = "DELETE FROM tblQuotes WHERE id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        
    }
}
