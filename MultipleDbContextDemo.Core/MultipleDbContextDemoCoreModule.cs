using Abp.AutoMapper;
using Abp.Modules;
using System.Reflection;

namespace MultipleDbContextDemo
{
    public class MultipleDbContextDemoCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}