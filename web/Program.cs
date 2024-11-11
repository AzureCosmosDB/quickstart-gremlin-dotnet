using Azure.Data.Tables;
using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Samples.Cosmos.Table.Quickstart.Services;
using Microsoft.Samples.Cosmos.Table.Quickstart.Services.Interfaces;
using Microsoft.Samples.Cosmos.Table.Quickstart.Web.Components;

using Settings = Microsoft.Samples.Cosmos.Table.Quickstart.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddOptions<Settings.Configuration>().Bind(builder.Configuration.GetSection(nameof(Settings.Configuration)));

builder.Services.AddSingleton<TableServiceClient>((serviceProvider) =>
{
    IOptions<Settings.Configuration> configurationOptions = serviceProvider.GetRequiredService<IOptions<Settings.Configuration>>();
    Settings.Configuration configuration = configurationOptions.Value;

    TableServiceClient serviceClient = new(
        connectionString: "<azure-cosmos-db-table-connection-string>"
    );

    TableClient client = serviceClient.GetTableClient(
        tableName: "<table-name>"
    );
    return client;
});

builder.Services.AddTransient<IDemoService, DemoService>();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
