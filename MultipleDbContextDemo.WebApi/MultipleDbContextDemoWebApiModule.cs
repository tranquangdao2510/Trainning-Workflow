using System.Linq;
using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Swashbuckle.Application;

namespace MultipleDbContextDemo
{
    [DependsOn(typeof(AbpWebApiModule), typeof(MultipleDbContextDemoApplicationModule))]
    public class MultipleDbContextDemoWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(MultipleDbContextDemoApplicationModule).Assembly, "app")
                .Build();
        }

        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "SwaggerIntegrationDemo.WebApi");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(Assembly.GetAssembly(typeof(MultipleDbContextDemoWebApiModule)), "AbpCompanyName.AbpProjectName.Api.Scripts.Swagger-Custom.js");
                });
        }
    }
}