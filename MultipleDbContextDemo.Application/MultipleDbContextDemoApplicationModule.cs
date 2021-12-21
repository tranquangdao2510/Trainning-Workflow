using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using AutoMapper;
using MultipleDbContextDemo.Products;
using MultipleDbContextDemo.Products.Dtos;

namespace MultipleDbContextDemo
{
    [DependsOn(typeof(MultipleDbContextDemoCoreModule), typeof(AbpAutoMapperModule))]
    public class MultipleDbContextDemoApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<CreateProductInput, Product>();
                config.CreateMap<UpdateProductInput, Product>();
                config.CreateMap<Product, GetInerJoinProductInput>()
                    .ForMember(c => c.CategoryName, a => a.MapFrom(s => s.AssignCategory.Name))
                    .ForMember(c => c.CategoryId, x => x.MapFrom(a => a.AssignCategory.Id));
            });
        }
    }
}