using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces;
using PMS.Application.Services;
using PMS.Domain.Interfaces;
using PMS.Infrastructure.Data;
using PMS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<PmsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
//Precompile Mappings: Register AutoMapper in Program.cs (as done) to initialize at startup.
builder.Services.AddAutoMapper(typeof(PMS.Application.Profiles.MappingProfile).Assembly);
//builder.Services.AddAutoMapper(new[] { typeof(PMS.Application.Profiles.MappingProfile) });

// Add UnitOfWork and Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IHoldingService, HoldingService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Add MVC
builder.Services.AddControllers();
//builder.Services.AddControllersWithViews();

// Add Static Files - only require if we want to use SPA feature
//builder.Services.AddSpaStaticFiles(configuration =>
//{
//    configuration.RootPath = "wwwroot";
//});

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PmsDbContext>();
    DbInitializer.Seed(context);
}

// Configure middleware
app.UseStaticFiles();

//app.MapGet("/", "index.html");
// Configure middleware
app.MapControllers();
//app.MapDefaultControllerRoute();

app.Run();

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
