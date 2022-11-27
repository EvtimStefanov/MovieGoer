using System;
using System.Data;
using System.IO;
using Application.Behaviors;
using Domain.Abstractions;
using FluentValidation;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Web.Middleware;


namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;

            services.AddControllers()
                .AddApplicationPart(presentationAssembly)
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.PropertyNamingPolicy = null
                );

            var applicationAssembly = typeof(Application.AssemblyReference).Assembly;

            services.AddMediatR(applicationAssembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(applicationAssembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });
            });

            var connString = Configuration.GetConnectionString("SqlConnectionString");

            services.AddDbContext<ApplicationDbContext>(builder =>
                builder.UseSqlServer(connString));

            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddScoped<IUnitOfWork>(
                factory => factory.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IDbConnection>(
                factory => factory.GetRequiredService<ApplicationDbContext>().Database.GetDbConnection());

            services.AddTransient<ExceptionHandlingMiddleware>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
