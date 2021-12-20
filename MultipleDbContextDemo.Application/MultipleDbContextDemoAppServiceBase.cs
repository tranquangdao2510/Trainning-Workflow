using Abp.Application.Services;
using Abp.Dependency;
using MultipleDbContextDemo.EntityFramework;

namespace MultipleDbContextDemo
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class MultipleDbContextDemoAppServiceBase : ApplicationService
    {
        protected MultipleDbContextDemoAppServiceBase()
        {
            LocalizationSourceName = MultipleDbContextDemoConsts.LocalizationSourceName;
        }
    }

    //public interface ISqlExecuter
    //{
    //    int Execute(string sql, params object[] parameters);
    //}

    //public class SqlExecuter : ISqlExecuter, ITransientDependency
    //{
    //    private readonly IDbContextProvider<MySecondDbContext> _dbContextProvider;

    //    public SqlExecuter(IDbContextProvider<MySecondDbContext> dbContextProvider)
    //    {
    //        _dbContextProvider = dbContextProvider;
    //    }

    //    public int Execute(string sql, params object[] parameters)
    //    {
    //        //_dbContextProvider.GetDbContext().Database//<= Here I dont see any function to execute SQL query
    //        //return 0;
    //        return _dbContextProvider.GetDbContext().Database.ExecuteSqlCommand(sql, parameters);
    //    }
    //}
}