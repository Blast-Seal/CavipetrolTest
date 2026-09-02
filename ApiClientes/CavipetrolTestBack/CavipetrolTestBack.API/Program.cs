#region Builder
using CavipetrolTestBack.API.Infrastructure;
using CavipetrolTestBack.API.Models;
using CavipetrolTestBack.Repositories.Configuration;
using CavipetrolTestBack.Repositories.Context;
using CavipetrolTestBack.Services.Interface;
using CavipetrolTestBack.Services.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultDBConnection");
var apiConfig = builder.Configuration.GetSection("ApiConfig").Get<ApiConfig>();
var key = Encoding.ASCII.GetBytes(apiConfig.Secret);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();

// DATABASE CONTEXT
builder.Services.AddDbContext<CavipetrolDBContext>(options =>
    options.UseSqlServer(connectionString));

// Enable Response Compression for better performance and avoid CRIME and BREACH attacks
builder.Services.AddResponseCompression();

// Dependency Injection for Application Services and Repositories
builder.Services.AddCustomServices(builder.Configuration);
builder.Services.AddScoped<IClienteService, ClienteService>();

// Add MVC with global authorization filter and JSON options
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new SetValidationDictionaryAttribute());    
})
.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


builder.Services.Configure<CookieTempDataProviderOptions>(options =>
{
    options.Cookie.IsEssential = true;
});

// Swagger Configuration
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Cavipetrol API", Version = "v1", Description = "Endpoints test Cavipetrol" });    
});


// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "https://localhost:4200"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddHttpContextAccessor();
#endregion

#region app
var app = builder.Build();

// Development middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "Cavipetrol API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Enable Response Compression
app.UseResponseCompression();

// Enable localization from client and set default culture
app.UseRequestLocalization();

// Https Routing
app.UseHttpsRedirection();

// Enable routing middleware
app.UseRouting();

// Enable CORS
app.UseCors("AllowSpecificOrigin");

//app.MapGet("/", () => "Welcome to Api from Cavipetrol");

// Map Controllers
app.MapControllers();

DBStartup.Initialize(app.Services, adminEmail: "admin@cavipetrol.com");

// Run the application
app.Run();
#endregion