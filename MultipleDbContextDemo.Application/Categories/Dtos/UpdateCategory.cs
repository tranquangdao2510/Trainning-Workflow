using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleDbContextDemo.Categories.Dtos
{
    public class UpdateCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}