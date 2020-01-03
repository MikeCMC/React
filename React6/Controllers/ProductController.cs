using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using React6.Models;

namespace React6.Controllers
{
    public class ProductController : Controller
    {

        private OnboardEntities db = new OnboardEntities();

        public ActionResult Index()
        {
            return View();
        }

        // GET Products
        public JsonResult GetProductList()
        {
            try
            {
                var productList = db.Products.ToList();

                return Json(productList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // DELETE Product
        public JsonResult DeleteProduct(int id)
        {
            try
            {
                var product = db.Products.Where(p => p.id == id).SingleOrDefault();
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Deletion Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // CREATE Product
        public JsonResult CreateProduct(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Product Create Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // UPDATE Product
        public JsonResult GetUpdateProduct(int id)
        {
            try
            {
                  Product product = db.Products.Where(x => x.id == id).SingleOrDefault();
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Product Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult UpdateProduct(Product product)
        {
            try
            {
                Product prod = db.Products.Where(p => p.id == product.id).SingleOrDefault();
                prod.name = product.name;
                prod.price = product.price;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Product Update Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}