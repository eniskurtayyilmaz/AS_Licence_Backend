using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Data.Repository.Host.EntityFramework;
using AS_Licence.Data.Repository.UnitOfWork.EntityFramework;
using AS_Licence.Service.Host.Customer;
using AS_Licence.Service.Host.CustomerComputerInfo;
using AS_Licence.Service.Host.RegisterComputer;
using AS_Licence.Service.Host.Software;
using AS_Licence.Service.Host.Subscription;
using AS_Licence.Service.Host.User;
using AS_Licence.Service.Interface.Customer;
using AS_Licence.Service.Interface.RegisterComputer;
using AS_Licence.Service.Interface.Software;
using AS_Licence.Service.Interface.Subscription;
using AS_Licence.Service.Interface.User;
using AS_Licence.WebUI.CoreAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

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
      services.AddTransient<ICustomerManager, CustomerService>();
      services.AddTransient<ICustomerComputerInfoManager, CustomerComputerInfoService>();
      services.AddTransient<ISubscriptionManager, SubscriptionService>();
      services.AddTransient<ISoftwareManager, SoftwareService>();
      services.AddTransient<IRegisterComputerManager, RegisterComputerService>();
      services.AddScoped<IUserManager, UserService>();


      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
        options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
            ValidateAudience = false,
            ValidateIssuer = false
          };
        }
      );
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      //Error handling
      app.UseExceptionHandler(builder =>
      {
        builder.Run(async context =>
        {
          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

          var error = context.Features.Get<IExceptionHandlerFeature>();

          string errorMessage = "";
          if (error != null)
          {
            //Error Content from Exception.Message
            errorMessage = error.Error.Message;
          }
          else
          {
            errorMessage = "Something wrong that we do not know";
          }
          context.Response.AddApplicationError(errorMessage);
          await context.Response.WriteAsync(errorMessage);
        });
      });

      app.UseCors(x => x.AllowAnyOrigin().AllowAnyOrigin().AllowAnyHeader());

      app.UseAuthentication();
      app.UseMvcWithDefaultRoute();


      app.UseSwagger(c =>
      {
        c.PreSerializeFilters.Add((doc, requset) =>
        {
          var root = $"{requset.Host.Value}";
          doc.Host = root;
        });
      });
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AS_Licence API");

      });
    }
  }
}
