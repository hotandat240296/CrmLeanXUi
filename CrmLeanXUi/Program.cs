using CrmLeanXUi.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Razor Components + Interactive Server
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

// MudBlazor
builder.Services.AddMudServices();

// Authorization (optional)
builder.Services.AddAuthorizationCore();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Map root component
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
