using System.Data.Entity;
using Abp.EntityFramework;
using MultipleDbContextDemo.Categories;

namespace MultipleDbContextDemo.EntityFramework
{
    public class MySecondDbContext : AbpDbContext
    {
        public virtual IDbSet<Course> Courses { get; set; }
        public virtual IDbSet<Category> Categories { get; set; }

        public MySecondDbContext()
            : base("Second")
        {
        }
    }
}