using AssignmentEAD.Models;
using System;
using System.Collections.Generic;

namespace AssignmentEAD.Data
{
    public static class ProductData
    {
        private static List<Product> Products = new List<Product>();

        public static List<Product> GetAllProducts()
        {
            return Products;
        }

        public static Product GetProductById(int productId)
        {
            return Products.Find(product => product.Id == productId);
        }

        public static void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }

            Products.Add(product);
        }

        public static void UpdateProduct(Product product)
        {
            Product existingProduct = GetProductById(product.Id);

            if (existingProduct == null)
            {
                throw new ArgumentException("Product not found.", nameof(product));
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.CategoryIds = product.CategoryIds;
        }

        public static void DeleteProduct(int productId)
        {
            Product productToRemove = GetProductById(productId);

            if (productToRemove != null)
            {
                Products.Remove(productToRemove);
            }
        }

        public static void RemoveFromPreviousCategory(int productId)
        {

            Product product = Products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                foreach (var categoryId in product.CategoryIds)
                {
                    Category category = CategoryData.GetCategoryById(categoryId);

                    if (category != null)
                    {
                        category.ProductIds.Remove(productId);
                    }
                }
            }
        }

        public static List<Product> GetProductsByCategory(int categoryId)
        {
            return Products.FindAll(product => product.CategoryIds.Contains(categoryId));
        }
    }
}
