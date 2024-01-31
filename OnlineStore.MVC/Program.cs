using Microsoft.AspNetCore.Authentication.Cookies;
using OnlineStore.MVC.Services;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Interfaces;
using Stripe;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    options.Secure = CookieSecurePolicy.Always;
});

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "OnlineStore-GB";
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
    opt.Cookie.Domain = "https://localhost:7019";
    opt.ExpireTimeSpan = TimeSpan.FromDays(14);

    opt.SlidingExpiration = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = new PathString("/account/login");
});

builder.Services.AddHttpClient<IClient, Client>(client => 
    client.BaseAddress = new Uri("https://localhost:7113"));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ISpecificationTypesService, SpecificationTypesService>();
builder.Services.AddScoped<ISpecificationsService, SpecificationsService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IReviewsService, ReviewsService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IWishlistsService, WishlistsService>();
builder.Services.AddScoped<IEventsService, EventsService>();
builder.Services.AddScoped<ICouponsService, CouponsService>();
builder.Services.AddScoped<ISubscribersService, SubscribersService>();
builder.Services.AddScoped<IContactRequestsService, ContactRequestsService>();
builder.Services.AddScoped<IMenuItemsService, MenuItemsService>();
builder.Services.AddScoped<IFilterGroupsService, FilterGroupsService>();

builder.Services.AddScoped<ICartStorage, CookieCartStorage>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/home/error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCookiePolicy();
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePagesWithRedirects("/error/{0}");

app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
