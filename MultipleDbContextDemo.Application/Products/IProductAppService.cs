using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MultipleDbContextDemo.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleDbContextDemo.Products
{
    public interface IProductAppService : IApplicationService
    {
        // sql thuan
        int CreateProductSql(CreateProductInput input);

        int UpdateProductSql(UpdateProductInput input);

        int DeleteProductSql(int pId);

        List<GetInerJoinProductInput> GetProductInnerJoinSql(GetCategoryInput input);

        List<GetInerJoinProductInput> GetProductLeftJoinSql(GetCategoryInput input);

        // lamba
        int CreateProduct(CreateProductInput input);
    }
}