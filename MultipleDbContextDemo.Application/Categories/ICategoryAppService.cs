using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MultipleDbContextDemo.Categories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleDbContextDemo.Categories
{
    public interface ICategoryAppService : IApplicationService
    {
        // entity frameword
        List<Category> GetCategories();

        int Create(CreateCategory input);

        int Edit(UpdateCategory input);

        bool Delete(int id);

        CategoryDto Detail(int id);

        // linq

        List<Category> GetCategoriesLinq();

        void CreateLinq(CreateCategory input);

        void EditLinq(UpdateCategory input);

        bool DeleteLinq(int id);

        CategoryDto DetailLinq(int id);

        // sql thuan
        List<Category> GetCategorySql();

        int CreateSql(CreateCategory input);

        int EditSql(UpdateCategory input);

        bool DeleteSql(int id);

        CategoryDto DetailSql(int id);
    }
}