using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PizzaBrightway.Models
{
    public class PizzaToppings
    {
        [JsonPropertyName("toppings")]
        public List<string> Toppings { get; set; }
    }
}
