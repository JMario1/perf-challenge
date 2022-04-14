using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saif.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name {get; set;}

        public string? Price {get; set;}

        public string? Description {get; set;}

        public Category? Category {get; set;}
        
        public int CategoryId {get; set;}
    }
}