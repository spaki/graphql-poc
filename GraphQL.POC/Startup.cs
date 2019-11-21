using GraphQL.POC.Infra;
using GraphQL.POC.Repositories;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
            services.AddScoped<IDependencyResolver>(c => new FuncDependencyResolver(c.GetRequiredService));
            services.AddScoped<ISchema, ProductSchema>();
            services.AddGraphQL(c => { c.ExposeExceptions = true; }).AddGraphTypes(ServiceLifetime.Scoped);

            // -> workaround for graphql serialization in .net 3.0
            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });
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

            // -> Swagger ui
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraphQL POC"));

            // -> GraphQL route (post) and ui/playgorund
            app.UseGraphQL<ISchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
