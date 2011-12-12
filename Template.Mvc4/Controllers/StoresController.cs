using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.Mvc4.Models;

namespace Template.Mvc4.Controllers
{   
    public class StoresController : Controller
    {
		private readonly IStoreRepository storeRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public StoresController() : this(new StoreRepository())
        {
        }

        public StoresController(IStoreRepository storeRepository)
        {
			this.storeRepository = storeRepository;
        }

        //
        // GET: /Stores/

        public ViewResult Index()
        {
            return View(storeRepository.AllIncluding(store => store.Products));
        }

        //
        // GET: /Stores/Details/5

        public ViewResult Details(int id)
        {
            return View(storeRepository.Find(id));
        }

        //
        // GET: /Stores/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Stores/Create

        [HttpPost]
        public ActionResult Create(Store store)
        {
            if (ModelState.IsValid) {
                storeRepository.InsertOrUpdate(store);
                storeRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Stores/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(storeRepository.Find(id));
        }

        //
        // POST: /Stores/Edit/5

        [HttpPost]
        public ActionResult Edit(Store store)
        {
            if (ModelState.IsValid) {
                storeRepository.InsertOrUpdate(store);
                storeRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Stores/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(storeRepository.Find(id));
        }

        //
        // POST: /Stores/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            storeRepository.Delete(id);
            storeRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

