using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using VikasFashionsAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.AddFile("Logs/VikasFashionLog-{Date}.txt");

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:JWTKey").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Add DB connection/context
builder.Services.AddDbContext<DataContextVikasFashion>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"))
);

builder.Services.AddScoped<VikasFashionsAPI.APIServices.CountryService.ICountryService, VikasFashionsAPI.APIServices.CountryService.CountryService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.UnitsOfMeasureService.IUnitsOfMeasureService, VikasFashionsAPI.APIServices.UnitsOfMeasureService.UnitsOfMeasureService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.CatalogMasterService.ICatalogMasterService, VikasFashionsAPI.APIServices.CatalogMasterService.CatalogMasterService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.HSNMasterService.IHSNMasterService, VikasFashionsAPI.APIServices.HSNMasterService.HSNMasterService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.MaterialGroupService.IMaterialGroupService, VikasFashionsAPI.APIServices.MaterialGroupService.MaterialGroupService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.MaterialTypeService.IMaterialTypeService, VikasFashionsAPI.APIServices.MaterialTypeService.MaterialTypeService>();

builder.Services.AddScoped<VikasFashionsAPI.APIServices.StateService.IStateService, VikasFashionsAPI.APIServices.StateService.StateService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.CityService.ICityService, VikasFashionsAPI.APIServices.CityService.CityService>();

builder.Services.AddScoped<VikasFashionsAPI.APIServices.ChartService.IChartService, VikasFashionsAPI.APIServices.ChartService.ChartService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.ColorService.IColorService, VikasFashionsAPI.APIServices.ColorService.ColorService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.DesignService.IDesignService, VikasFashionsAPI.APIServices.DesignService.DesignService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.MaterialService.IMaterialService, VikasFashionsAPI.APIServices.MaterialService.MaterialService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.ShadeService.IShadeService, VikasFashionsAPI.APIServices.ShadeService.ShadeService>();

builder.Services.AddScoped<VikasFashionsAPI.APIServices.CompanyService.ICompanyService, VikasFashionsAPI.APIServices.CompanyService.CompanyService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.PlantBranchService.IPlantBranchService, VikasFashionsAPI.APIServices.PlantBranchService.PlantBranchService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.WarehouseService.IWarehouseService, VikasFashionsAPI.APIServices.WarehouseService.WarehouseService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.BinLocationService.IBinLocationService, VikasFashionsAPI.APIServices.BinLocationService.BinLocationService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.BusinessPartnerAddressService.IBusinessPartnerAddressService, VikasFashionsAPI.APIServices.BusinessPartnerAddressService.BusinessPartnerAddressService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.BusinessPartnersBankDetailService.IBusinessPartnersBankDetailService, VikasFashionsAPI.APIServices.BusinessPartnersBankDetailService.BusinessPartnersBankDetailService>();

builder.Services.AddScoped<VikasFashionsAPI.APIServices.WithHoldingTaxService.IWithHoldingTaxService, VikasFashionsAPI.APIServices.WithHoldingTaxService.WithHoldingTaxService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.PaymentTermService.IPaymentTermService, VikasFashionsAPI.APIServices.PaymentTermService.PaymentTermService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.CompanyGroupService.ICompanyGroupService, VikasFashionsAPI.APIServices.CompanyGroupService.CompanyGroupService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.AreaService.IAreaService, VikasFashionsAPI.APIServices.AreaService.AreaService>();

builder.Services.AddScoped<VikasFashionsAPI.APIServices.UserService.IUserService, VikasFashionsAPI.APIServices.UserService.UserService>();

builder.Services.AddScoped<VikasFashionsAPI.APIServices.BusinessPartnerService.IBusinessPartnerService, VikasFashionsAPI.APIServices.BusinessPartnerService.BusinessPartnerService>();
builder.Services.AddScoped<VikasFashionsAPI.APIServices.BusinessPartnerTypeService.IBusinessPartnerTypeService, VikasFashionsAPI.APIServices.BusinessPartnerTypeService.BusinessPartnerTypeService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{

    // Do not add exception handler for dev environment. In dev,
    // we get the developer exception page with detailed error info.

    app.UseExceptionHandler(errorApp =>
    {
        // Logs unhandled exceptions. For more information about all the
        // different possibilities for how to handle errors see
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-5.0
        errorApp.Run(async context =>
        {
            // Return machine-readable problem details. See RFC 7807 for details.
            // https://datatracker.ietf.org/doc/html/rfc7807#page-6
            var pd = new ProblemDetails
            {
                Type = "https://google.com/",
                Title = "An unrecoverable error occurred",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "This is a crash, but dont worry we are working on this.",
            };
            pd.Extensions.Add("RequestId", context.TraceIdentifier);
            await context.Response.WriteAsJsonAsync(pd, pd.GetType(), null, contentType: "application/problem+json");
        });
    });
}


app.UseCors(builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
