using PizzaBrightway.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PizzaBrightway
{
    class Program
    {
        /* 
         * Typically you would use the below as Microsoft docs say to use this type
         * of client going forward...
         * But I will use HttpWebRequest instead for the purpose of this test
         
         // private static readonly HttpClient client = new();
        */

        static async Task Main(string[] args)
        {
            var pizzaToppingsList = JsonSerializer.Deserialize<List<PizzaToppings>>(await ProcessPizzaToppingsAsync());
            var toppingsArray = pizzaToppingsList.SelectMany(s => s.Toppings);
            var topTwentyResults = 20;

            // Linq queries for easy traversal
            var groupedToppings = toppingsArray
                .GroupBy(s => s)
                .Select(g => new { Topping = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(topTwentyResults);

            foreach (var item in groupedToppings)
            {
                var topping = item.Topping;
                var count = item.Count;

                Console.WriteLine($"Topping: {topping} | Count: {count}");
            }
        }

        private static async Task<string> ProcessPizzaToppingsAsync()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://brightway.com/CodeTests/pizzas.json");
            request.ContentType = "application/json";

            using HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new(stream);

            return await reader.ReadToEndAsync();
        }
    }
}
