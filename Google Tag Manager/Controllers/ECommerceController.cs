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

            return View(tran);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Standard(Transaction tran)
        {
            return View(tran);
        }


        [HttpGet]
        public ActionResult Impressions()
        {
            return new EmptyResult();
        }
    }
}
