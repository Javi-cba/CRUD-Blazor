using BlazorCrud;
using BlazorCrud.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Registra el HttpClient con la URL base
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5145/") });

// Registra el servicio ArticuloService
builder.Services.AddScoped<ArticuloService>();
builder.Services.AddSweetAlert2();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
