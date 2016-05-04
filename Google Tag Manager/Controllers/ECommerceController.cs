using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google_Tag_Manager.Models;

namespace Google_Tag_Manager.Controllers
{
    public class ECommerceController : Controller
    {
        [HttpGet]
        public ActionResult Standard()
        {
            var tran = new Transaction
            {
                TransactionAffiliation = "Acme Clothing",
                TransactionTotal = 53.26m,
                TransactionTax = 1.29m,
                TransactionShipping = 5m
            };

            tran.TransactionProducts.Add(new Product
            {
                Sku = "DD44",
                Name = "T-Shirt",
                Category = "Apparel",
                Price = 11.99m,
                Quantity = 1

            });

            tran.TransactionProducts.Add(new Product
            {
                Sku = "AA1243544",
                Name = "Socks",
                Category = "Apparel",
                Price = 9.99m,
                Quantity = 2
            });

            tran.TransactionProducts.Add(new Product
            {
                Sku = "DD23444",
                Name = "Fluffy Pink Bunnies",
                Category = "Party Toys",
                Price = 5m,
                Quantity = 3
            });

            Session["Transactions"] = tran;
            return View(tran);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Standard(Transaction tran)
        {
            return View(tran);
        }


        [HttpGet]
        public ActionResult Products()
        {
            var trans = (Transaction)Session["Transactions"];
            var products = trans.TransactionProducts.Select(
              (p, i) => new EnhancedProduct
              {
                  Id = $"P{DateTime.Now.Year}-{i + 1}",
                  Brand = "Google",
                  Category = p.Category,
                  Coupon = $"SUMMER_SALE{i + 1}",
                  Name = p.Name,
                  Price = p.Price,
                  Variant = $"Color{i + 1}",
                  Quantity = p.Quantity,
                  Position = i + 1
              }).ToList();

            Session["EnhancedProduct"] = products;
            return View(products);
        }

        [HttpGet]
        public ActionResult ProductClick(string id)
        {
            var products = (List<EnhancedProduct>)Session["EnhancedProduct"];
            var theProduct = products.Single(x => x.Id == id);
            return View(theProduct);
        }

        [HttpGet]
        public ActionResult AddToCart()
        {
            return new EmptyResult();
        }



    }
}
