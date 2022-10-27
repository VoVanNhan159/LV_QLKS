using Blazored.Modal;
using Blazored.SessionStorage;
using Blazored.Toast;
using LV_QLKS.Data;
using LV_QLKS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Radzen;
using Tewr.Blazor.FileReader;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddControllers().AddNewtonsoftJson();


builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "LV_QLKS",
        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

//Add Radzen
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

//Add Blazor Toast
builder.Services.AddBlazoredToast();

//Add Blazor Session
builder.Services.AddBlazoredSessionStorage();

//Add Http to upload Image
builder.Services.AddScoped<HttpClient>();
builder.Services.AddFileReaderService();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


builder.Services.AddHttpContextAccessor();

//builder.Services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseMvcWithDefaultRoute();

app.Run();
