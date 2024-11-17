using BaseballManagerApp.Components;
using BaseballManagerApp.Data;
using BaseballManagerApp.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();


//Authentication Services using Cookies and Microsoft Identity
builder.Services.AddDbContext<BaseballDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<BaseballDbContext>();

builder.Services.AddAuthentication("BaseballAuthScheme")
    .AddCookie("BaseballAuthScheme", o =>
    {
        o.Cookie.Name = "BaseballManager";
        o.LoginPath = "/login";
        o.Cookie.HttpOnly = true; //protecting access
        o.Cookie.SecurePolicy = CookieSecurePolicy.Always; //protection https
        o.Cookie.SameSite = SameSiteMode.Strict;
    });

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistAuthenticationStateProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BaseballManagerApp.Client._Imports).Assembly);

app.Run();
