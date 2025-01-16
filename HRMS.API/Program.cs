using FluentValidation.AspNetCore;
using HRMS.API.Endpoints.CompanyBranch;
using HRMS.API.Endpoints.Address;
using HRMS.API.Endpoints.Tenant;
using HRMS.API.Endpoints.User;
using HRMS.API.Modules.User;
using HRMS.BusinessLayer.Interfaces;
using HRMS.BusinessLayer.Services;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.PersistenceLayer.Repositories;
using HRMS.Utility.AutoMapperProfiles.Tenant.CompanyMapping;
using HRMS.Utility.AutoMapperProfiles.Address.AddressMapping;
using HRMS.Utility.AutoMapperProfiles.Address.City;
using HRMS.Utility.AutoMapperProfiles.Address.Country;
using HRMS.Utility.AutoMapperProfiles.Address.State;
using HRMS.Utility.AutoMapperProfiles.Tenant.OrganizationMapping;
using HRMS.Utility.AutoMapperProfiles.Tenant.SubdomainMapping;
using HRMS.Utility.AutoMapperProfiles.Tenant.TenancyRoleMapping;
using HRMS.Utility.AutoMapperProfiles.Tenant.TenantMapping;
using HRMS.Utility.AutoMapperProfiles.Tenant.TenantRegistrationMapping;
using HRMS.Utility.AutoMapperProfiles.User.UserMapping;
using HRMS.Utility.AutoMapperProfiles.User.UserRoleMapping;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using HRMS.Utility.Helpers.LogHelpers.Services;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Data;
using System.Diagnostics.Metrics;
using HRMS.Utility.AutoMapperProfiles.Address.AddressTypeMapping;

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
            //builder.Services.AddScoped<>
            builder.Services.AddScoped<IAddressTypeLogger, AddressTypeLogger>();
            builder.Services.AddScoped<IAddressLogger, AddressLogger>();
            builder.Services.AddScoped<IOrganizationLogger, OrganinizationLogger>();
            builder.Services.AddScoped<ICompanyLogger, CompanyLogger>();
            builder.Services.AddScoped<ICompanyBranchLogger, CompanyBranchLogger>();
            builder.Services.AddScoped<ITenancyRoleLogger, TenancyRoleLogger>();

            builder.Services.AddScoped<ICityLogger, CityLogger>();
            builder.Services.AddScoped<ICountryLogger, CountryLogger>();
            builder.Services.AddScoped<IStateLogger, StateLogger>();

            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<ICityService, CityService>();

            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<ICountryService, CountryService>();

            builder.Services.AddScoped<IStateRepository, StateRepository>();
            builder.Services.AddScoped<IStateService, StateService>();

            builder.Services.AddScoped<ITenantRegistrationLogger, TenantRegistrationLogger>();
            builder.Services.AddScoped<IUserLogger, UserLogger>();
            builder.Services.AddScoped<IUserRoleLogger, UserRoleLogger>();
            builder.Services.AddScoped<IUserRoleMappingLogger, UserRoleMappingLogger>();
            builder.Services.AddScoped<ISubdomainLogger, SubdomainLogger>();
            builder.Services.AddScoped<ITenantLogger, TenantLogger>();


            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
         
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IAddressService, AddressService>();

            builder.Services.AddScoped<ITenantRepository, TenantRepository>();
            builder.Services.AddScoped<ITenantService, TenantService>();

            builder.Services.AddScoped<ISubdomainRepository, SubdomainRepository>();
            builder.Services.AddScoped<ISubdomainService, SubdomainService>();

            builder.Services.AddScoped<ITenancyRoleService, TenancyRoleService>();
            builder.Services.AddScoped<ITenancyRoleRepository, TenancyRoleRepository>();

            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddScoped<IUserRoleService, UserRoleService>();

            builder.Services.AddScoped<IUserRoleMappingRepository, UserRoleMappingRepository>();
            builder.Services.AddScoped<IUserRoleMappingService, UserRoleMappingService>();

            builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            builder.Services.AddScoped<IOrganizationService, OrganizationService>();

            builder.Services.AddScoped<ITenantRegistrationRepository, TenantRegistrationRepository>();
            builder.Services.AddScoped<ITenantRegistrationService, TenantRegistrationService>();

            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();

            builder.Services.AddScoped<ICompanyBranchRepository, CompanyBranchRepository>();
            builder.Services.AddScoped<ICompanyBranchService, CompanyBranchService>();

            builder.Services.AddScoped<IAddressTypeRepository, AddressTypeRepository>();
            builder.Services.AddScoped<IAddressTypeService, AddressTypeService>();

            builder.Services.AddSingleton<IDbConnection>(_ => new SqlConnection(builder.Configuration.GetConnectionString("HRMS_DB")));

            builder.Services.AddAutoMapper(typeof(UserMappingProfile),
                                           typeof(TenancyRoleMappingProfile),
                                           typeof(UserRoleMappingProfile),
                                           typeof(OrganizationMappingProfile),
                                           typeof(SubdomainMappingProfile),
                                           typeof(TenantRegistrationMappingProfile),
                                           typeof(SubdomainMappingProfile),
                                           typeof(OrganizationMappingProfile),
                                           typeof(CompanyMappingProfile),
                                           typeof(TenantMappingProfile),
                                           typeof(CompanyMappingProfile),
                                           typeof(TenantMappingProfile),
                                           typeof(AddressMappingProfile),
                                           typeof(AddressTypeMappingProfile),
                                           typeof(AddressMappingProfile),
                                           typeof(StateMappingProfile),
                                           typeof(CityMappingProfile),
                                           typeof(CountryMappingProfile));

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
            app.MapUserRoleEndpoints();
            app.MapSubdomainEndpoints();
            app.MapTenantEndpoints();
            app.MapTenantRegistrationEndpoints();
            app.MapUserRoleMappingEndpoints();
            app.MapCompanyEndpoints();
            app.MapCompanyBranchEndpoints();
            app.MapAddressEndpoints();
            app.MapAddressTypeEndpoints();

            app.MapCityEndpoints();
            app.MapCountryEndpoints();
            app.MapStateEndpoints();
            app.Run();
        }
    }
}
