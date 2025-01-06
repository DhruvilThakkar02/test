using FluentValidation.AspNetCore;
using HRMS.API.Endpoints.Tenant;
using HRMS.API.Endpoints.User;
using HRMS.API.Modules.User;
using HRMS.BusinessLayer.Interfaces;
using HRMS.BusinessLayer.Services;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.PersistenceLayer.Repositories;
using HRMS.Utility.AutoMapperProfiles.Tenant.OrganizationMapping;
using HRMS.Utility.AutoMapperProfiles.Tenant.SubdomainMapping;
using HRMS.Utility.AutoMapperProfiles.Tenant.TenancyRoleMapping;
using HRMS.Utility.AutoMapperProfiles.Tenant.TenantMapping;
using HRMS.Utility.AutoMapperProfiles.Tenant.TenantRegistrationMapping;
using HRMS.Utility.AutoMapperProfiles.User.UserMapping;
using HRMS.Utility.AutoMapperProfiles.User.UserRolesMapping;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using HRMS.Utility.Helpers.LogHelpers.Services;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Data;

namespace HRMS.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                //.WriteTo.ApplicationInsights("Your-Instrumentation-Key", TelemetryConverter.Traces)  // Optional: Application Insights
                .MinimumLevel.Information()
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IOrganizationLogger, OrganinizationLogger>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ITenantRepository, TenantRepository>();
            builder.Services.AddScoped<ITenantService, TenantService>();

            builder.Services.AddScoped<ISubdomainRepository, SubdomainRepository>();
            builder.Services.AddScoped<ISubdomainService, SubdomainService>();

            builder.Services.AddScoped<ITenancyRoleService, TenancyRoleService>();
            builder.Services.AddScoped<ITenancyRoleRepository, TenancyRoleRepository>();

            builder.Services.AddScoped<IUserRolesRepository, UserRolesRepository>();
            builder.Services.AddScoped<IUserRolesService, UserRolesService>();

            builder.Services.AddScoped<IUserRolesMappingRepository, UserRolesMappingRepository>();
            builder.Services.AddScoped<IUserRolesMappingService, UserRolesMappingService>();

            builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            builder.Services.AddScoped<IOrganizationService, OrganizationService>();

            builder.Services.AddScoped<ITenantRegistrationRepository, TenantRegistrationRepository>();
            builder.Services.AddScoped<ITenantRegistrationService, TenantRegistrationService>();

            builder.Services.AddSingleton<IDbConnection>(_ => new SqlConnection(builder.Configuration.GetConnectionString("HRMS_DB")));

            builder.Services.AddAutoMapper(typeof(UserMappingProfile),
                                           typeof(TenancyRoleMappingProfile),
                                           typeof(UserRolesMappingProfile),
                                           typeof(OrganizationMappingProfile),
                                           typeof(SubdomainMappingProfile),
                                           typeof(TenantRegistrationMappingProfile),
                                           typeof(SubdomainMappingProfile),
                                           typeof(OrganizationMappingProfile),
                                           typeof(TenantMappingProfile));

            builder.Services.AddAuthorization();
            builder.Services.AddCors();
            builder.Host.UseSerilog();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.EnableAnnotations();
            });

            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = null;
            });

            builder.Services.AddFluentValidationAutoValidation();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthorization();

            app.MapUserEndpoints();
            app.MapOrganizationEndpoints();
            app.MapTenancyRoleEndpoints();
            app.MapUserRolesEndpoints();
            app.MapSubdomainEndpoints();
            app.MapTenantEndpoints();
            app.MapTenantRegistrationEndpoints();
            app.MapUserRolesMappingEndpoints();

            app.Run();
        }
    }
}
