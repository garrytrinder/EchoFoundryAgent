using EchoAgent;
using EchoAgent.Agent;
using Microsoft.Agents.Builder;
using Microsoft.Agents.Hosting.AspNetCore;
using Microsoft.Agents.Storage;
using Microsoft.Agents.Storage.Blobs;

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
builder.AddAgent<Echo>();

// Configure the storage for the agent
builder.Services.AddSingleton<IStorage>((sp) => new BlobsStorage(
    builder.Configuration["BlobsStorageOptions:ConnectionString"],
    builder.Configuration["BlobsStorageOptions:ContainerName"]));

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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Map the /api/messages endpoint to the AgentApplication
app.MapPost("/api/messages", async (HttpRequest request, HttpResponse response, IAgentHttpAdapter adapter, IAgent agent, CancellationToken cancellationToken) =>
{
    await adapter.ProcessAsync(request, response, agent, cancellationToken);
});

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Playground")
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