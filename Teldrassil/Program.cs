using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Teldrassil.Data;
using Microsoft.AspNetCore.Server.Kestrel.Https;

var builder = WebApplication.CreateBuilder(args);

// Configure HTTPS with the specified certificate
var certificatePath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "cert", "dotnet5.net.pfx");
var passwordPath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "cert", "keystorePass.txt"); ;
//2023.06.17之后尝试方法如下
//https://www.cnblogs.com/jackyfei/p/16416868.html
// Configure HTTP
if (File.Exists(certificatePath) && File.Exists(passwordPath))
{
    var certificatePassword = File.ReadAllText(passwordPath).Trim();
    builder.WebHost.UseKestrel((host,options) =>
    {
        //var filename = host.Configuration.GetValue("AppSettings:certfilename", "");
        //var password = host.Configuration.GetValue("AppSettings:certpassword", "");
        options.Listen(IPAddress.Any, 80);
        options.Listen(IPAddress.Any, 443, listenOptions =>
        {
            listenOptions.UseHttps(certificatePath, certificatePassword);
        });
    });
}

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure HTTPS redirection
app.UseHttpsRedirection(); // 重定向 HTTP 请求到 HTTPS

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
//ICollection<String> urls = new String[2] {"http://[::]:80","http://[::]:443"};
//2023.06.17之前方法
//app.Urls.Add("http://*:80");
//app.Urls.Add("https://*:443");


app.Run();

