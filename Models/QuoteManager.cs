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
                var query = "SELECT * FROM tblQuotes";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var quote = new Quote
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Author = reader["author"].ToString(),
                            QuoteText = reader["quote"].ToString(),
                            
                        };
                        quotes.Add(quote);
                    }
                }
            }
            return quotes;
        }

        
    }
}
