using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.Mvc4.Models;

namespace Template.Mvc4.Controllers
{
  public class ProductsController : Controller
  {
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    // If you are using Dependency Injection, you can delete the following constructor
    public ProductsController()
      : this(new CategoryRepository(), new ProductRepository())
    {
    }

    public ProductsController(ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
      this._categoryRepository = categoryRepository;
      this._productRepository = productRepository;
    }

    //
    // GET: /Products/

    public ViewResult Index()
    {
      return View(_productRepository.AllIncluding(product => product.Category, product => product.Stores, product => product.Purchases));
    }

    //
    // GET: /Products/Details/5

    public ViewResult Details(int id)
    {
      return View(_productRepository.Find(id));
    }

    //
    // GET: /Products/Create

    public ActionResult Create()
    {
      ViewBag.PossibleCategories = _categoryRepository.All;
      return View();
    }

    //
    // POST: /Products/Create

    [HttpPost]
    public ActionResult Create(Product product)
    {
      if (ModelState.IsValid)
      {
        _productRepository.InsertOrUpdate(product);
        _productRepository.Save();
        return RedirectToAction("Index");
      }
      else
      {
        ViewBag.PossibleCategories = _categoryRepository.All;
        return View();
      }
    }

    //
    // GET: /Products/Edit/5

    public ActionResult Edit(int id)
    {
      ViewBag.PossibleCategories = _categoryRepository.All;
      return View(_productRepository.Find(id));
    }

    //
    // POST: /Products/Edit/5

    [HttpPost]
    public ActionResult Edit(Product product)
    {
      if (ModelState.IsValid)
      {
        _productRepository.InsertOrUpdate(product);
        _productRepository.Save();
        return RedirectToAction("Index");
      }
      else
      {
        ViewBag.PossibleCategories = _categoryRepository.All;
        return View();
      }
    }

    //
    // GET: /Products/Delete/5

    public ActionResult Delete(int id)
    {
      return View(_productRepository.Find(id));
    }

    //
    // POST: /Products/Delete/5

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      _productRepository.Delete(id);
      _productRepository.Save();

      return RedirectToAction("Index");
    }
  }
}

