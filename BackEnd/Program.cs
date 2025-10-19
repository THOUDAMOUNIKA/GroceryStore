using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GroceryStoreAPI.Data;
using GroceryStoreAPI.Services;
using GroceryStoreAPI.Models;
using Serilog;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/grocerystore-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token.",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Database
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<GroceryStoreContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else
{
    builder.Services.AddDbContext<GroceryStoreContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
}

// In-Memory Caching (for free deployment)
builder.Services.AddMemoryCache();

// Authentication
builder.Services.AddScoped<IAuthService, AuthService>();

var jwtKey = builder.Configuration["Jwt:Key"] ?? "your-secret-key-here-make-it-long-enough";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Initialize database and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GroceryStoreContext>();
    context.Database.EnsureCreated();
    
    // Seed data if no items exist
    if (!context.GroceryItems.Any())
    {
        context.GroceryItems.AddRange(
            // Fruits
            new GroceryItem { Name = "Apples", Description = "Fresh red apples", Price = 2.99m, Quantity = 100, Category = "Fruits", ImageUrl = "https://images.unsplash.com/photo-1560806887-1e4cd0b6cbd6?w=300", IsAvailable = true },
            new GroceryItem { Name = "Bananas", Description = "Ripe yellow bananas", Price = 1.99m, Quantity = 150, Category = "Fruits", ImageUrl = "https://images.unsplash.com/photo-1571771894821-ce9b6c11b08e?w=300", IsAvailable = true },
            new GroceryItem { Name = "Oranges", Description = "Juicy navel oranges", Price = 3.49m, Quantity = 80, Category = "Fruits", ImageUrl = "https://images.unsplash.com/photo-1547036967-23d11aacaee0?w=300", IsAvailable = true },
            
            // Dairy
            new GroceryItem { Name = "Milk", Description = "Fresh whole milk", Price = 3.49m, Quantity = 50, Category = "Dairy", ImageUrl = "https://images.unsplash.com/photo-1550583724-b2692b85b150?w=300", IsAvailable = true },
            new GroceryItem { Name = "Eggs", Description = "Farm fresh eggs", Price = 4.99m, Quantity = 80, Category = "Dairy", ImageUrl = "https://images.unsplash.com/photo-1582722872445-44dc5f7e3c8f?w=300", IsAvailable = true },
            new GroceryItem { Name = "Cheese", Description = "Cheddar cheese block", Price = 6.99m, Quantity = 45, Category = "Dairy", ImageUrl = "https://images.unsplash.com/photo-1486297678162-eb2a19b0a32d?w=300", IsAvailable = true },
            
            // Bakery
            new GroceryItem { Name = "Bread", Description = "Whole wheat bread", Price = 2.49m, Quantity = 75, Category = "Bakery", ImageUrl = "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=300", IsAvailable = true },
            new GroceryItem { Name = "Croissants", Description = "Buttery croissants", Price = 3.99m, Quantity = 25, Category = "Bakery", ImageUrl = "https://images.unsplash.com/photo-1555507036-ab794f4afe5e?w=300", IsAvailable = true },
            
            // Meat
            new GroceryItem { Name = "Chicken Breast", Description = "Boneless chicken breast", Price = 8.99m, Quantity = 30, Category = "Meat", ImageUrl = "https://images.unsplash.com/photo-1604503468506-a8da13d82791?w=300", IsAvailable = true },
            new GroceryItem { Name = "Ground Beef", Description = "Lean ground beef", Price = 7.99m, Quantity = 25, Category = "Meat", ImageUrl = "https://images.unsplash.com/photo-1588347818481-c7c1b8b8e8b8?w=300", IsAvailable = true },
            
            // Vegetables
            new GroceryItem { Name = "Tomatoes", Description = "Fresh tomatoes", Price = 3.99m, Quantity = 90, Category = "Vegetables", ImageUrl = "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=300", IsAvailable = true },
            new GroceryItem { Name = "Carrots", Description = "Fresh carrots", Price = 2.49m, Quantity = 75, Category = "Vegetables", ImageUrl = "https://images.unsplash.com/photo-1598170845058-32b9d6a5da37?w=300", IsAvailable = true }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Serve Angular app
app.MapFallbackToFile("index.html");



app.Run();