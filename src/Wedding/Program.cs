using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Wedding.Areas.Identity;
using Wedding.Data;
using Wedding.Services;
using Wedding.Controllers;
using Wedding.Models;
using Radzen;
using BlazorDownloadFile;
using FluentValidation;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// adding an appsettings that allows for local development, but will not be pushed to the repo.
builder.Configuration.AddJsonFile("appsettings.local.json", true, true);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(
    options => { 
        options.SignIn.RequireConfirmedAccount = true;
        })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddBlazorDownloadFile(ServiceLifetime.Scoped);
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddTransient<IValidator<RegisterModel>, RegisterModelValidator>();

builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IPartyService, PartyService>();
builder.Services.AddScoped<IPictureService, PictureService>();
builder.Services.AddScoped<IAccomodationService, AccomodationService>();
builder.Services.AddScoped<ImageController>();
builder.Services.AddScoped<IFileStorageService, AzureBlobService>();


builder.Services.AddScoped<DialogService>();

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<BlazorCookieLoginMiddleware>();

app.MapControllers();
app.MapControllerRoute(
    name: "image",
    pattern: "api/image/upload",
    defaults: new { controller = "Image", action = "Image" });
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
