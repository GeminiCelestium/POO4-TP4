using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModernRecrut.MVC.Areas.Identity.Data;
using ModernRecrut.MVC.Data;
using ModernRecrut.MVC.Interfaces;
using ModernRecrut.MVC.Services;

var builder = WebApplication.CreateBuilder(args);
// Test commit NJ // Test Jay // Test Nathan 2 // Jay test 2
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews().AddControllersAsServices();

builder.Services.AddHttpClient<IGestionPostulationsService, GestionPostulationsServiceProxy>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UrlPostulationsAPI")));
builder.Services.AddHttpClient<IGestionFavorisService, GestionFavorisServiceProxy>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UrlFavorisAPI")));
builder.Services.AddHttpClient<IGestionEmploisService, GestionEmploisServiceProxy>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UrlEmploisAPI")));
builder.Services.AddHttpClient<IGestionDocumentsService, GestionDocumentsServiceProxy>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UrlDocumentsAPI")));

var connectionString = builder.Configuration.GetConnectionString("ModernRecrutMVCContextConnection");

builder.Services.AddDbContext<ModernRecrutMVCContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<ModernRecrutMVCUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ModernRecrutMVCContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/CodeStatus?code={0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePagesWithRedirects("/Home/CodeStatus?code={0}");
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
