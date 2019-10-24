using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuardianAPI.BLL;
using GuardianAPI.Interfaces;
using GuardianAPI.Interfaces.ILoggerManager;
using GuardianAPI.Interfaces.PSIManager;
using GuardianAPI.LoggerService;
using GuardianAPI.Models;
using GuardianAPI.Repositories;
using GuardianAPI.Repositories.PSIManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GuardianAPI
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config, ILoggerFactory loggerFactory)
        {
            NLog.LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseMySql(_config.GetConnectionString("DCSConnectionStringDevelopment")));


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _config["Jwt:Issuer"],
                        ValidAudience = _config["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
                    };
                });
            

            services.AddMvc();

           
            //   .AddJsonOptions(
            //       options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //   ).AddXmlSerializerFormatters(); 

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });


            // Register Interfaces with their implementations
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IDocumentDataRepository, DocumentDataRepository>();  
            services.AddScoped<ILogEntryRepository, LogEntryRepository>();
            services.AddScoped<IPanelRepository, PanelRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<IParticipantPanelRepository, ParticipantPanelRepository>();
            services.AddScoped<IParticipantScheduleRepository, ParticipantScheduleRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IRequisitionRepository, RequisitionRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<IResultDetailRepository, ResultDetailRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<ITestPanelRepository, TestPanelRepository>();
            services.AddScoped<ITestScheduleRepository, TestScheduleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IPaternityRelationRepository, PaternityRelationRepository>();

            // PSI Manager implementations
            services.AddScoped<IClientRepository, ClientRepository>();

            // Register NLogger for DI
            services.AddScoped<ILoggerManager, LoggerManager>();

            services.AddScoped<IExternalCreateParticipant, ExternalCreateParticipant>();
            services.AddScoped<IResultGenerator, ResultGenerator>();
            services.AddScoped<IPDFCreatorRepository, PDFCreator>();

          

            // Test Services(for DI)
          //  services.AddScoped<IParticipantExtended, ParticipantExtended>();
          //  services.AddScoped<ITestLogic, TestLogic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enable Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "My API V1");
            });
            
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
