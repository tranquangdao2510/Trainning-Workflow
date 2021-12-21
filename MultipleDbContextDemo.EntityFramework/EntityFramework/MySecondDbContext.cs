using System.Data.Entity;
using Abp.EntityFramework;
using MultipleDbContextDemo.Categories;
using MultipleDbContextDemo.Products;

namespace MultipleDbContextDemo.EntityFramework
{
    public class MySecondDbContext : AbpDbContext
    {
        public virtual IDbSet<Course> Courses { get; set; }
        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Product> Products { get; set; }

        public MySecondDbContext()
            : base("Second")
        {
        }
    }
}