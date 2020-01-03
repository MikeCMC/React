using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using React6.Models;

namespace React6.Controllers
{
    public class StoreController : Controller
    {

        private OnboardEntities db = new OnboardEntities();

        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        // GET Stores
        public JsonResult GetStore()
        {
            try
            {
                var storeList = db.Stores.Select(x => new { x.id, x.name, x.address }).ToList();
                return new JsonResult { Data = storeList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // DELETE Store
        public JsonResult DeleteStore(int id)
        {
            try
            {
                var store = db.Stores.Where(s => s.id == id).SingleOrDefault();
                if (store != null)
                {
                    db.Stores.Remove(store);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Deletion Falied", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // CREATE Store
        public JsonResult CreateStore(Store store)
        {
            try
            {
                db.Stores.Add(store);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Store Create Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // UPDATE Store
        public JsonResult GetUpdateStore(int id)
        {
            try
            {
                Store store = db.Stores.Where(s => s.id == id).SingleOrDefault();
                return new JsonResult { Data = store, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Store Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult UpdateStore(Store store)
        {
            try
            {
                Store st = db.Stores.Where(s => s.id == store.id).SingleOrDefault();
                st.name = store.name;
                st.address = store.address;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Store Update Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}