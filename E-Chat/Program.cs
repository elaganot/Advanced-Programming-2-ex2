using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using E_Chat.Data;
using E_Chat.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<E_ChatContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("E_ChatContext") ?? throw new InvalidOperationException("Connection string 'E_ChatContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("Allow All",
//        builder =>
//        {
//            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
//        });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPermission", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:3000")
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("ClientPermission");

//app.UseCors("Allow All");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ratings}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/hubs/chat");
});

app.Run();
