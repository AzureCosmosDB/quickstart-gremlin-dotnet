using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<GremlinServer>((_) =>
    new GremlinServer(
        hostname: $"<azure-cosmos-db-gremlin-endpoint>",
        port: 443,
        username: "/dbs/cosmicworks/colls/products",
        password: $"<azure-cosmos-db-gremlin-key>",
        enableSsl: true
    )
);

builder.Services.AddSingleton<GremlinClient>((provider) =>
    new GremlinClient(
        gremlinServer: provider.GetRequiredService<GremlinServer>(),
        messageSerializer: new GraphSON2MessageSerializer()
    )
);

builder.Services.AddTransient<IDemoService, DemoService>();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
