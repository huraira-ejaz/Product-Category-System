using System.Collections.Generic;

namespace AssignmentEAD.Models
{
    public class Category
    {
        public int Id { get; set; }        
        public string Name { get; set; } 
        public List<int> ProductIds { get; set; } = new List<int>();
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
            ProductIds = new List<int>();
        }

        public Category()
        {
        }
    }
}
