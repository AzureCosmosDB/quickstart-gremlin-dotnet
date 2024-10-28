using Azure.Data.Tables;
using Azure.Identity;
using Microsoft.Samples.Cosmos.Table.Quickstart.Services;
using Microsoft.Samples.Cosmos.Table.Quickstart.Services.Interfaces;
using Microsoft.Samples.Cosmos.Table.Quickstart.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddSingleton<TableServiceClient>((_) =>
{
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
