
namespace Bank
{
  using Microsoft.EntityFrameworkCore;
  using Bank.Integration.Repositories;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using System;
  using Bank.Business.Services.Info;
  using Bank.Business.Services;
  using System.Reflection;
  using System.IO;

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
      services.AddDbContext<BankAppDbContext>(opt => opt.UseInMemoryDatabase("BankDB"));
      services.AddScoped<IAccountRepo, AccountRepo>();
      services.AddScoped<ICustomerRepo, CustomerRepo>();
      services.AddScoped<ITransferHistoryRepo, TransferHistoryRepo>();

      services.AddScoped<IAccountDetailsInfoService, AccountDetailsInfoService>();
      services.AddScoped<ITransferAmountService, TransferAmountService>();
      services.AddScoped<ITransferHistoryDetailsInfoService, TransferHistoryDetailsInfoService>();
      services.AddScoped<IAddAccountService, AddAccountService>();
      services.AddScoped<IAddAccountToCustomerService, AddAccountToCustomerService>();
      services.AddScoped<IAccountsDetailsInfoService, AccountsDetailsInfoService>();

      services.AddControllers();
      services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();
      app.UseSwaggerUI(c =>
        {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank Api V1");
          c.RoutePrefix = string.Empty;
        });
      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}

