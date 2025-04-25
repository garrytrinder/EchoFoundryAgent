using EchoAgent;
using Microsoft.Agents.Hosting.AspNetCore;
using Microsoft.Agents.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient("WebClient", client => client.Timeout = TimeSpan.FromSeconds(600));
builder.Services.AddHttpContextAccessor();
builder.Services.AddCloudAdapter<AdapterWithErrorHandler>();
builder.Logging.AddConsole();

// Add AspNet token validation
builder.Services.AddBotAspNetAuthentication(builder.Configuration);

// Add ApplicationOptions from config
builder.AddAgentApplicationOptions();

// Add the agent (which is transient)
builder.AddAgent<EchoAgent.Agent.EchoAgent>();
builder.Services.AddSingleton<IStorage, MemoryStorage>();

// Configure AzureAIConfiguration
builder.Services.AddSingleton(serviceProvider => 
    builder.Configuration.GetSection("AzureAIAgentConfiguration"));

// Add the Semantic Kernel services
builder.Services.AddKernel();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "TestTool")
{
    app.MapGet("/", () => "Echo Agent");
    app.UseDeveloperExceptionPage();
    app.MapControllers().AllowAnonymous();
}
else
{
    app.MapControllers();
}

app.Run();