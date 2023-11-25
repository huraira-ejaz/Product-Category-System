using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using AssignmentEAD.Data;
using AssignmentEAD.Models;

namespace AssignmentEAD.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            List<Product> products = ProductData.GetAllProducts();
            return View(products);
        }


        public IActionResult Details(int id)
        {
            Product product = ProductData.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            List<Category> categories = CategoryData.GetAllCategories();

            if (categories.Count > 0)
            {
                ViewBag.Categories = CategoryData.GetAllCategories();


                var newProduct = new Product();

                return View(newProduct);



            }
            else
            {
                ViewBag.ErrorOccurred = true;
                ViewBag.Categories = CategoryData.GetAllCategories();
                var newProduct = new Product();

                return View(newProduct);
            }
            

        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                product.Id = GenerateUniqueId();
                ProductData.AddProduct(product);
                foreach (int categoryId in product.CategoryIds)
                {
                    CategoryData.AddProductToCategory(categoryId, product.Id);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Categories = CategoryData.GetAllCategories();
                return View(product);
            }
        }

        public IActionResult Edit(int id)
        {
            ProductData.RemoveFromPreviousCategory(id);
            Product product = ProductData.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = CategoryData.GetAllCategories();
            return View(product);
        }
     
        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            try
            {
                product.Id = id;
                ProductData.UpdateProduct(product);
                foreach (int categoryId in product.CategoryIds)
                {
                    CategoryData.AddProductToCategory(categoryId, product.Id);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Categories = CategoryData.GetAllCategories();
                return View(product);
            }
        }
        
        public IActionResult Delete(int id)
        {
            Product product = ProductData.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductData.DeleteProduct(id);

            return RedirectToAction(nameof(Index));
        }
        private int GenerateUniqueId()
        {
            List<Product> products = ProductData.GetAllProducts();
            return products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
        }
    }
}
