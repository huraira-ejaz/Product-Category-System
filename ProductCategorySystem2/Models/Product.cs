using System.Collections.Generic;

namespace AssignmentEAD.Models
{
    public class Product
    {
        public int Id { get; set; }         
        public string Name { get; set; }    
        public string Description { get; set; }  
        public decimal Price { get; set; } 

        public List<int> CategoryIds { get; set; } = new List<int>();
        public Product(int id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CategoryIds = new List<int>();
        }

        public Product()
        {
        }
    }
}
