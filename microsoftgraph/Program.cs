using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Graph;
using Microsoft.Identity.Web.UI;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();

// Azure AD Authentication Tokeni ön bellekte tutarak microsoft ile giriþ yapmýþ kullanýcý microsoft grap apýsýne istek atabilir hale geliyor.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi(new string[] { "User.Read" })
    .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
     .AddInMemoryTokenCaches();

// Burda graph apý ye istek tmak için  GraphServiceClient'a DI yapýyoruz.

builder.Services.AddScoped<GraphServiceClient>(sp =>
     {
         var authenticationProvider = new GraphAuthenticationProvider(sp.GetRequiredService<ITokenAcquisition>());
         return new GraphServiceClient(authenticationProvider);
     });



builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
