using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extentions
{
    public static class ApplicationServiceExtentions
    {
        // notice this method extend the IServiceCollection and hence only need one param when used outside
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<DataContext>(options => 
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            // cors is required for frontend browser to request data from api 
            services.AddCors(opt => 
            {
                opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
                });
            });

            // add automapper and find the correct assembly
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);


            // we need to tell this guy where the handler for the requests are
            // when the request is sent from the API controllers, mediator with direct them to the handlers in the Application layer
            // finds the assembly of Application > List > Handler
            services.AddMediatR(typeof(List.Handler));

            return services;
        }
    }
}