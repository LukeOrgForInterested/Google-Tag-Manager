using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    }

    public class Product
    {
        public string Name { get; set; }
        public string Sku { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }


    public class Impression
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string List { get; set; }
        public string Category { get; set; }
        public string Variant { get; set; }
        public int Position { get; set; }
        public decimal Price { get; set; }
    }

    public class EnhancedProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Variant { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Coupon { get; set; }
        public int Position { get; set; }

        public Impression Convert()
        {
            return new Impression
            {
                Brand = Brand,
                Category = Category,
                Id = Id,
                Name = Name,
                Position = Position,
                Price = Price,
                Variant = Variant,
                List = $"Search Results{Position}"
            };
        }
    }

    public enum EAction
    {
        Click,
        Detail,
        Add,
        Remove,
        Checkout,
        [JsonProperty("checkout_option")]
        CheckoutOption,
        Purchase,
        Refund,
        [JsonProperty("promo_click")]
        PromoClick
    }


    public static class ECommerceJsonHelper
    {
        public static string DetailWithImpression(this EnhancedProduct product)
        {
            var data = new
            {
                Ecommerce = new
                {
                    Impressions = new List<Impression> { product.Convert() },
                    Detail = new
                    {
                        ActionField = new { },
                        Products = new List<EnhancedProduct> { product }
                    }
                }
            };

            return data.ToJsonString();
        }


        public static string Impression(this IEnumerable<EnhancedProduct> products, string currencyCode = "USD")
        {
            var data = new
            {
                Ecommerce = new
                {
                    Impressions = products.Select(x => x.Convert()).ToList(),
                    CurrencyCode = currencyCode
                }
            };

            return data.ToJsonString();
        }

        public static void Clicks()
        {
            var t = new
            {
                Event = "productClick",
                Ecommerce = new
                {
                    Click = new
                    {
                        ActionField = new { },
                        Products = new List<EnhancedProduct>()
                    }
                }
            };
        }

        public static string ToJsonString(this object source)
        {
            var defaultSetting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(source, defaultSetting);
        }
    }
}