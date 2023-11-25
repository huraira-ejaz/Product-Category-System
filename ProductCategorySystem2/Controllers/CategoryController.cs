using Microsoft.AspNetCore.Mvc;
using AssignmentEAD.Data;
using AssignmentEAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssignmentEAD.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            List<Category> categories = CategoryData.GetAllCategories();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            Category category = CategoryData.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            //added
            var newCategory = new Category();
            return View(newCategory);
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            try
            {
                category.Id = GenerateUniqueId();
                CategoryData.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }
        public IActionResult Edit(int id)
        {
            Category category = CategoryData.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            CategoryData.DeleteProductsInCategory(id);
            try
            {
                category.Id = id;
                CategoryData.UpdateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }
    
        public IActionResult Delete(int id)
        {
            CategoryData.DeleteProductsInCategory(id);
            Category category = CategoryData.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            CategoryData.DeleteCategory(id);

            return RedirectToAction(nameof(Index));
        }
 
        private int GenerateUniqueId()
        {
            List<Category> categories = CategoryData.GetAllCategories();
            return categories.Count > 0 ? categories.Max(c => c.Id) + 1 : 1;
        }
        [HttpGet]
        public IActionResult ViewProducts(int id)
        {
            
            Category category = CategoryData.GetCategoryById(id);

            if (category == null)
            {
                return NotFound(); 
            }

            
            List<Product> products = ProductData.GetProductsByCategory(category.Id);

            return View(products);
        }
    }
}
