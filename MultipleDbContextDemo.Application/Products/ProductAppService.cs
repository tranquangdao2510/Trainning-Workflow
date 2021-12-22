using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Abp.ObjectMapping;
using Abp.UI;
using MultipleDbContextDemo.Categories;
using MultipleDbContextDemo.EntityFramework;
using MultipleDbContextDemo.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleDbContextDemo.Products
{
    public class ProductAppService : MultipleDbContextDemoAppServiceBase, IProductAppService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IDbContextProvider<MySecondDbContext> _mySecondDbContext;

        public ProductAppService(IRepository<Product> productRepository, IObjectMapper objectMapper, IDbContextProvider<MySecondDbContext> mySecondDbContext, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _objectMapper = objectMapper;
            _mySecondDbContext = mySecondDbContext;
        }

        // bieu thuc lam ba
        public int CreateProduct(CreateProductInput input)
        {
            try
            {
                var product = _objectMapper.Map<Product>(input);
                _productRepository.InsertAsync(product);
                return product.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateProduct(UpdateProductInput input)
        {
            try
            {
                var product = _productRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
                if (product.Id > 0)
                {
                    product.ProductName = input.ProductName;
                    product.CategoryId = input.CategoryId;
                    product.Quantity = input.Quantity;
                    product.Active = input.Active;
                    //var pro = _objectMapper.Map<Product>(product);
                    var p = _productRepository.UpdateAsync(product);
                    return p.Id;
                }
                else
                {
                    throw new UserFriendlyException("Update does not success");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteProduct(int proId)
        {
            try
            {
                var product = _productRepository.GetAll().Where(x => x.Id == proId).FirstOrDefault();
                if (product.Id != 0)
                {
                    var pro = _productRepository.DeleteAsync(product);
                    return pro.Id;
                }
                else
                {
                    throw new UserFriendlyException("Delete does not success");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetInerJoinProductInput> GetInnerJoinProduct()
        {
            try
            {
                var product = _categoryRepository.GetAll()
                    .Join(_productRepository.GetAll(), c => c.Id, p => p.CategoryId, (c, p) => new { Product = p, Category = c })
                    .Select(x => new GetInerJoinProductInput { Id = x.Product.Id, CategoryId = x.Product.CategoryId, ProductName = x.Product.ProductName, CategoryName = x.Category.Name }).ToList();

                var pro = _objectMapper.Map<List<GetInerJoinProductInput>>(product);
                return pro;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetInerJoinProductInput> GetLeftJoinProduct()
        {
            try
            {
                var product = _categoryRepository.GetAll()
                    .GroupJoin(_productRepository.GetAll(), c => c.Id, p => p.CategoryId, (c, p) => new { Products = p, Category = c })
                    .SelectMany(cp => cp.Products.DefaultIfEmpty(), (cp, p) => new { cp = cp.Category, ProductName = p.ProductName, Id = p == null ? 0 : p.Id }).ToList();

                var prod = product.Select(x => new GetInerJoinProductInput { CategoryId = x.cp.Id, CategoryName = x.cp.Name, ProductName = x.ProductName, Id = x.Id }).ToList();

                var pro = _objectMapper.Map<List<GetInerJoinProductInput>>(prod);
                return pro;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // sql thuan
        public int CreateProductSql(CreateProductInput input)
        {
            try
            {
                var product = _objectMapper.Map<Product>(input);
                string sql = "insert into Product(ProductName,Quantity,Active,CategoryId) values(@name,@quantity,@active,@cateId)";
                var name = new SqlParameter("name", product.ProductName);
                var quantity = new SqlParameter("quantity", product.Quantity);
                var active = new SqlParameter("active", product.Active);
                var cateId = new SqlParameter("cateId", product.CategoryId);
                var p = _mySecondDbContext.GetDbContext().Database.ExecuteSqlCommand(sql, name, quantity, active, cateId);
                return p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateProductSql(UpdateProductInput input)
        {
            try
            {
                var sqlgetId = "select Id, ProductName,Quantity,CategoryId,Active from Product where Id=@id";
                var proId = new SqlParameter("id", input.Id);
                var pro = _mySecondDbContext.GetDbContext().Database.SqlQuery<Product>(sqlgetId, proId).FirstOrDefault();

                if (pro.Id > 0)
                {
                    var product = _objectMapper.Map<Product>(input);
                    string sql = "update Product set ProductName=@name,Quantity=quantity,Active=@active,CategoryId=@cateId where Id=@pId";
                    var pId = new SqlParameter("pId", pro.Id);
                    var name = new SqlParameter("name", product.ProductName);
                    var quantity = new SqlParameter("quantity", product.Quantity);
                    var active = new SqlParameter("active", product.Active);
                    var cateId = new SqlParameter("cateId", product.CategoryId);
                    var p = _mySecondDbContext.GetDbContext().Database.ExecuteSqlCommand(sql, name, quantity, active, cateId, pId);
                    return p;
                }
                else
                {
                    throw new UserFriendlyException("Update does not success");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteProductSql(int pId)
        {
            try
            {
                var sqlgetId = "select Id, ProductName,Quantity,CategoryId,Active from Product where Id=@id";
                var proId = new SqlParameter("id", pId);
                var pro = _mySecondDbContext.GetDbContext().Database.SqlQuery<Product>(sqlgetId, proId).FirstOrDefault();
                if (pro.Id > 0)
                {
                    string sql = "delete Product  where Id=@proId";
                    var id = new SqlParameter("proId", pId);
                    var delpro = _mySecondDbContext.GetDbContext().Database.ExecuteSqlCommand(sql, id);
                    return delpro;
                }
                else
                {
                    throw new UserFriendlyException("Update does not success");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetInerJoinProductInput> GetProductInnerJoinSql(GetCategoryInput input)
        {
            try
            {
                Product p = new Product();
                string sql = "select Product.Id, Product.ProductName,Product.CategoryId, Category.Name as CategoryName from Product inner join Category on Product.CategoryId = Category.Id";
                var cateId = new SqlParameter("id", input.CategoryId.HasValue);
                var pro = _mySecondDbContext.GetDbContext().Database.SqlQuery<GetInerJoinProductInput>(sql, cateId).ToList();

                var product = _objectMapper.Map<List<GetInerJoinProductInput>>(pro);
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetInerJoinProductInput> GetProductLeftJoinSql(GetCategoryInput input)
        {
            try
            {
                Product p = new Product();
                string sql = "select Product.Id, Product.ProductName,Product.CategoryId,Category.Name as CategoryName from Product left join Category on Product.CategoryId = Category.Id order by ProductName Desc";

                var pro = _mySecondDbContext.GetDbContext().Database.SqlQuery<GetInerJoinProductInput>(sql).ToList();

                var product = _objectMapper.Map<List<GetInerJoinProductInput>>(pro);
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // linq
        public List<GetInerJoinProductInput> GetInerJoinProdductLinq()
        {
            try
            {
                var product = (from c in _mySecondDbContext.GetDbContext().Categories
                               join p in _mySecondDbContext.GetDbContext().Products
                               on c.Id equals p.CategoryId
                               select p).ToList();
                var pro = _objectMapper.Map<List<GetInerJoinProductInput>>(product);
                return pro;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetInerJoinProductInput> GetLeftJoinProdductLinq()
        {
            try
            {
                var product = (from c in _mySecondDbContext.GetDbContext().Categories
                               join p in _mySecondDbContext.GetDbContext().Products
                               on c.Id equals p.CategoryId into ps
                               from p in ps.DefaultIfEmpty()
                               select p).ToList();
                var pro = _objectMapper.Map<List<GetInerJoinProductInput>>(product);
                return pro;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}