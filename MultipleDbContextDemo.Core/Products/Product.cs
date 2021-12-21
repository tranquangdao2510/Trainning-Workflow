using Abp.Domain.Entities;
using MultipleDbContextDemo.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleDbContextDemo.Products
{
    [Table("Product")]
    public class Product : Entity
    {
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public bool? Active { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category AssignCategory { get; set; }

        public int CategoryId { get; set; }
    }
}