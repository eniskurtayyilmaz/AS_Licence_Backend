using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Data.Repository.Host.EntityFramework;
using AS_Licence.Data.Repository.UnitOfWork.EntityFramework;
using AS_Licence.Service.Host.Customer;
using AS_Licence.Service.Host.CustomerComputerInfo;
using AS_Licence.Service.Host.RegisterComputer;
using AS_Licence.Service.Host.Software;
using AS_Licence.Service.Host.Subscription;
using AS_Licence.Service.Interface.Customer;
using AS_Licence.Service.Interface.RegisterComputer;
using AS_Licence.Service.Interface.Software;
using AS_Licence.Service.Interface.Subscription;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace AS_Licence.WebUI.CoreAPI
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "AS_Licence API", Version = "v1" });
      });


      services.AddDbContext<EfAsLicenceContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("AS_Licence.WebUI.CoreAPI")));
      services.AddTransient<IUnitOfWork, EfUnitOfWork>();
      /*
    private readonly ICustomerManager _customerManager;
    */

      services.AddTransient<ICustomerManager, CustomerService>();
      services.AddTransient<ICustomerComputerInfoManager, CustomerComputerInfoService>();
      services.AddTransient<ISubscriptionManager, SubscriptionService>();
      services.AddTransient<ISoftwareManager, SoftwareService>();
      services.AddTransient<IRegisterComputerManager, RegisterComputerService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseMvcWithDefaultRoute();

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AS_Licence API");
      });
    }
  }
}
