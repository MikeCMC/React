using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using React6.Models;

namespace React6.Controllers
{
    public class SalesController : Controller
    {
        private OnboardEntities db = new OnboardEntities();

        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        // GET Sales
        public JsonResult GetSales()
        {
            try
            {
                var saleList = db.Sales.Select(s => new
                {
                    Id = s.id,
                    dateSold = s.DateSold,
                    customerName = s.Customer.name,
                    productName = s.Product.name,
                    storeName = s.Store.name

                }).ToList();
                return new JsonResult { Data = saleList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetCustomers()
        {
            try
            {
                var Customerdata = db.Customers.Select(p => new { Id = p.id, customerName = p.name }).ToList();

                return new JsonResult { Data = Customerdata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetProducts()
        {
            try
            {
                var ProductsData = db.Products.Select(p => new { id = p.id, productName = p.name }).ToList();

                return new JsonResult { Data = ProductsData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetStores()
        {
            try
            {
                var StoresData = db.Stores.Select(p => new { Id = p.id, storeName = p.name }).ToList();

                return new JsonResult { Data = StoresData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // DELETE Sale
        public JsonResult DeleteSale(int id)
        {
            try
            {
                var sale = db.Sales.Where(s => s.id == id).SingleOrDefault();
                if (sale != null)
                {
                    db.Sales.Remove(sale);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Deletion Falied", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // CREATE Sale
        public JsonResult CreateSale(Sale sale)
        {
            try
            {
                db.Sales.Add(sale);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Sale Create Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // UPDATE Sale
        public JsonResult GetUpdateSale(int id)
        {
            try
            {
                Sale sale = db.Sales.Where(s => s.id == id).SingleOrDefault();
                string value = JsonConvert.SerializeObject(sale, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return new JsonResult { Data = sale, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Sale Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult UpdateSale(Sale sale)
        {
            try
            {
                Sale sa = db.Sales.Where(s => s.id == sale.id).SingleOrDefault();
                sa.customerId = sale.customerId;
                sa.productId = sale.productId;
                sa.storeId = sale.storeId;
                sa.DateSold = sale.DateSold;

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Sale Update Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}