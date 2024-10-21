using Azure.Data.Tables;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<TableClient>((_) =>
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

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
