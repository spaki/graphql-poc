using GraphiQl;
using GraphQL.POC.Infra;
using GraphQL.POC.Repositories;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace GraphQL.POC
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphQL POC", Version = "v1" }));

            services.AddTransient<IRepository, FakeRepository>();

            // -> GraphQL
            services.AddSingleton<IDependencyResolver>(c => new FuncDependencyResolver(c.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<ProductQuery>();
            services.AddSingleton<ProductType>();
            services.AddSingleton<CategoryType>();
            services.AddSingleton<ISchema, ProductSchema>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // -> just to have some data
            FakeRepository.CreateFakeData();



            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraphQL POC"));
            app.UseGraphiQl("/graphql");

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
