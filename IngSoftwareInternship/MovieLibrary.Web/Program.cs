using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Business.Mapping;
using MovieLibrary.Business.Services;
using MovieLibrary.Business.Services.ServiceInterfaces;
using MovieLibrary.Data.Models;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("MovieLibraryWebContextConnection") ?? throw new InvalidOperationException("Connection string 'MovieLibraryWebContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGenresService,GenreService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<UserService, UserService>();


builder.Services.AddDbContext<MoviesDataBaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MoviesDataBaseContext>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


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
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
