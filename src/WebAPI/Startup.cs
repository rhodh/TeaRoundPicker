using Application.Calculators;
using Application.Services;
using Domain.Mappers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.DrinkRunRepo;
using Persistence.Mappers;
using Persistence.UserRepo;
using WebAPI.Filters;

namespace WebAPI
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
            services
                .AddControllers(op => op.Filters.Add<HttpResponseExceptionsFilter>())
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            services.AddApiVersioning();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TeaRoundPickerAPI", Version = "v1" });
            });

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserWriter, UserRepository>();
            services.AddTransient<IUserReader, UserRepository>();
            services.AddTransient<IDrinkRunService, DrinkRunService>();
            services.AddTransient<IDrinkPicker, FairDrinkPicker>();
            services.AddTransient<IDrinkRunWriter, DrinkRunRepository>();
            services.AddAutoMapper(typeof(UserProfile), typeof(UserDomainProfile));
            services.AddDbContext<TeaRoundPickerContext>( 
                options => options.UseLazyLoadingProxies().UseNpgsql(Configuration.GetConnectionString("TeaRoundPickerDb")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeaRoundPickerAPI v1"));
            }

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
