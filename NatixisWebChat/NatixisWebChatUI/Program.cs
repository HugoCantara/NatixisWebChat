using Microsoft.AspNetCore.Authentication.Cookies;
using NatixisWebChatInfrastructure.Context;
using NatixisWebChatInfrastructure.Hubs;
using NatixisWebChatInfrastructure.Repositories;
using NatixisWebChatInfrastructure.Repositories.Interfaces;
using NatixisWebChatInfrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IGroupsRepository, GroupsRepository>();
builder.Services.AddScoped<PasswordServices>();
builder.Services.AddScoped<AuthenticationServices>();

builder.Services.AddDbContext<NatixisDbContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Index";
        options.Cookie.Name = "LoginCookie";
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
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

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<WebChatHub>("/chatHub");

app.Run();
