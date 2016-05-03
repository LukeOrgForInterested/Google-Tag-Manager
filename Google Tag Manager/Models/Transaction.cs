using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Google_Tag_Manager.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public string TransactionAffiliation { get; set; }
        public decimal TransactionTotal { get; set; }
        public decimal TransactionShipping { get; set; }
        public decimal TransactionTax { get; set; }
        public IList<Product> TransactionProducts { get; set; }

        public Transaction()
        {
            TransactionId = $"T{DateTime.Now.ToString("yyMMddHHmmssfff")}";
            TransactionProducts = new List<Product>();
        }

        public string ToJson()
        {
            var defaultSetting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(this, defaultSetting);
        }
    }

    public class Product
    {
        [EmailAddress]
        public string Name { get; set; }
        public string Sku { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}