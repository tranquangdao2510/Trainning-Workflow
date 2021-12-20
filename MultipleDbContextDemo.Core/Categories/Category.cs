using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleDbContextDemo.Categories
{
    [Table("Category")]
    public class Category : Entity
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}