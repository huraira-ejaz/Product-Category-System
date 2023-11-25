using AssignmentEAD.Models;
using System;
using System.Collections.Generic;

namespace AssignmentEAD.Data
{
    public static class CategoryData
    {
        private static List<Category> Categories = new List<Category>();

        public static List<Category> GetAllCategories()
        {
            return Categories;
        }

        public static Category GetCategoryById(int categoryId)
        {
            return Categories.Find(category => category.Id == categoryId);
        }

        public static void AddCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");
            }

            Categories.Add(category);
        }

        public static void UpdateCategory(Category category)
        {
            Category existingCategory = GetCategoryById(category.Id);

            if (existingCategory == null)
            {
                throw new ArgumentException("Category not found.", nameof(category));
            }

            existingCategory.Name = category.Name;
        }

        public static void DeleteCategory(int categoryId)
        {
            Category categoryToRemove = GetCategoryById(categoryId);

            if (categoryToRemove != null)
            {
                Categories.Remove(categoryToRemove);

           
                foreach (Product product in ProductData.GetAllProducts())
                {
                    product.CategoryIds.Remove(categoryId);
                }
            }
        }
        public static void AddProductToCategory(int categoryId, int productId)
        {
            Category category = GetCategoryById(categoryId);

            if (category != null)
            {
                category.ProductIds.Add(productId);
            }
        }

        public static void AddCategoryToProduct(int productId, int categoryId)
        {
            Product product = ProductData.GetProductById(productId);

            if (product != null)
            {
                product.CategoryIds.Add(categoryId);
            }
        }
        public static List<Category> GetCategoriesByProduct(int productId)
        {
            List<Category> categories = new List<Category>();

       
            foreach (var category in Categories)
            {
         
                if (category.ProductIds.Contains(productId))
                {
                    categories.Add(category);
                }
            }

            return categories;
        }
        public static void DeleteProductsInCategory(int categoryId)
        {
 
            Category category = Categories.FirstOrDefault(c => c.Id == categoryId);

            if (category != null)
            {
                foreach (var productId in category.ProductIds)
                {
                    ProductData.DeleteProduct(productId);
                }
            }
        }
        public static List<Product> GetProductsByCategory(int categoryId)
        {
            return ProductData.GetProductsByCategory(categoryId);
        }
    }
}
