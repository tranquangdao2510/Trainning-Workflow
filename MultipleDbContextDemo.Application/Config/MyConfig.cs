using AutoMapper;
using MultipleDbContextDemo.Products;
using MultipleDbContextDemo.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleDbContextDemo.Config
{
    public class MyConfig
    {
        public class ProductConfig
        {
            public static void CreateMap(IMapperConfigurationExpression cfg)
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductDto, Product>();
            }
        }

        public MyConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                ProductConfig.CreateMap(cfg);
            });
            var mapper = config.CreateMapper();
        }
    }
}