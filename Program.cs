using Microsoft.EntityFrameworkCore;
using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Models.Operations;
using PARKIT_enterprise_final.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMapPointProvider, MapPointOperations>();
builder.Services.AddScoped<IListingsProvider, ListingOperations>();
builder.Services.AddScoped<IBookingProvider, BookingOperations>();
builder.Services.AddScoped<IUserProvider, UserOperations>();
builder.Services.AddScoped<IWalletProvider, WalletOperations>();
builder.Services.AddScoped<IAddressProvider, AddressOperations>();

builder.Services.AddHttpClient<GeocodeApi>();


builder.Services.AddDbContext<PARKITDBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("PARKITDB")));

// using session service
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
