using Abp.Application.Services;
using MultipleDbContextDemo.Products.Dtos;
using System.Collections.Generic;

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

        int UpdateProduct(UpdateProductInput input);

        int DeleteProduct(int proId);

        List<GetInerJoinProductInput> GetInnerJoinProduct();

        List<GetInerJoinProductInput> GetLeftJoinProduct();

        // linq
        List<GetInerJoinProductInput> GetInerJoinProdductLinq();

        List<GetInerJoinProductInput> GetLeftJoinProdductLinq();
    }
}