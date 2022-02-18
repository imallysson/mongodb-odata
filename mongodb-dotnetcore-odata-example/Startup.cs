using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using mongodb_dotnetcore_odata_example.Extensions;
using mongodb_dotnetcore_odata_example.Models;
using MongoDB.Bson.Serialization;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

namespace mongodb_dotnetcore_odata_example
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterMongoClient();
            services.RegisterMongoDbRepositories();
            services.AddControllers(opt => opt.EnableEndpointRouting = false)
                .AddOData(opt =>
                {
                    opt.Select().Filter().OrderBy().Count().SetMaxTop(1000);
                    opt.AddRouteComponents("odata", GetEdmModel());
                })
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseODataRouteDebug();

            BsonClassMap.RegisterClassMap<Customer>(cm =>
            {
                cm.AutoMap();
                cm.GetMemberMap(c => c.FirstName).SetElementName("firstname");
                cm.GetMemberMap(c => c.LastName).SetElementName("lastname");
                cm.GetMemberMap(c => c.EMail).SetElementName("email");
                cm.GetMemberMap(c => c.Active).SetElementName("active");
                cm.GetMemberMap(c => c.Gender).SetElementName("gender");
                cm.SetIgnoreExtraElements(true);
            });
        }

        private IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<Customer>("Customer");
            //add additional entities >>here<<
            return odataBuilder.GetEdmModel();
        }
    }
}
