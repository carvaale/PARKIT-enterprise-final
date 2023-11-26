using Microsoft.EntityFrameworkCore;
using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Models.Operations;
using PARKIT_enterprise_final.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IListingsProvider, ListingOperations>();
builder.Services.AddScoped<IBookingProvider, BookingOperations>();
builder.Services.AddScoped<IUserProvider, UserOperations>();

builder.Services.AddHttpClient<GeocodeApi>();


builder.Services.AddDbContext<PARKITDBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("PARKITDB")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
