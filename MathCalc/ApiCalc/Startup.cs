using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathCalc.Calc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MathCalc.ApiCalc
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddControllers();

            services.AddScoped<ICalcPrimeNumber, CalcPrimeNumber>();

            services
                .AddMvc(options =>
                {

                    var authPolicy = new AuthorizationPolicyBuilder()
                         .RequireAuthenticatedUser()
                         .Build();

                    options.Filters.Add(new AuthorizeFilter(authPolicy));
                    options.Filters.Add(new UnhandledExceptionLoggerFilter());
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                ;

            services.AddMemoryCache();


            services.AddResponseCompression();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // The signing key must match!
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(new JwtOptions().GetSecretKeyBytes()),

                        // Validate the JWT Issuer (iss) claim
                        ValidateIssuer = true,
                        ValidIssuer = "ralfesapi",

                        // Validate the JWT Audience (aud) claim
                        ValidateAudience = true,
                        ValidAudience = "ralfesweb",

                        // Validate the token expiry
                        ValidateLifetime = true
                    };

                    options.IncludeErrorDetails = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Daniel Ralfes - Api de Cálculo V1",
                    Description = "Api de Cálculo Divisores/Números Primos",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Daniel Ralfes",
                        Email = "danielralfes@gmail.com"
                    }
                });

                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title       = "Daniel Ralfes - Api de Cálculo V2",
                    Description = "Api de Cálculo Divisores/Números Primos",
                    Version     = "v2",
                    Contact = new OpenApiContact
                    {
                        Name = "Daniel Ralfes",
                        Email = "danielralfes@gmail.com"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Caso não é para expor fora de testes
            //deixar dentro do if (env.IsDevelopment())

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "[Ralfes v1] Api de Cálculo Divisores/Números Primos");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "[Ralfes v2] Api de Cálculo Divisores/Números Primos");
            });

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(_ => true) // allow any origin
                 //.AllowCredentials()
                ); // allow credentials

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseMvc();
        }
    }
}
