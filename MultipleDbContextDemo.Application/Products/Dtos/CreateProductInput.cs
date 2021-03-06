using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleDbContextDemo.Products.Dtos
{
    [AutoMapTo(typeof(Product))]
    public class CreateProductInput
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
        public int? CategoryId { get; set; }
    }
}