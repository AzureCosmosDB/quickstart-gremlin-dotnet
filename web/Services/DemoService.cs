using Cosmos.Samples.Gremlin.Quickstart.Web.Models;
using Gremlin.Net.Driver;

internal interface IDemoService
{
    Task RunAsync(Func<string, Task> writeOutputAync);

    string GetEndpoint();
}

internal sealed class DemoService(GremlinServer server, GremlinClient client) : IDemoService
{
    public string GetEndpoint() => $"{server.Uri.AbsoluteUri}";

    public async Task RunAsync(Func<string, Task> writeOutputAync)
    {
        {
            var gremlinQuery = $@"
                g.addV('product')
                 .property('id', '68719518391')
                 .property('Category', 'gear-surf-surfboards')
                 .property('Name', 'Yamba Surfboard')
                 .property('Quantity', 10)
                 .property('Price', 300)
                 .property('Clearance', true)
            ";
            await client.SubmitAsync(
                requestScript: gremlinQuery
            );

            await writeOutputAync($"Add entity:\t68719518391");
        }

        {
            string gremlinQuery = $@"
                g.addV('product')
                 .property('id', '68719518371')
                 .property('Category', 'gear-surf-surfboards')
                 .property('Name', 'Kiama Classic Surfboard')
                 .property('Quantity', 25)
                 .property('Price', 790.00)
                 .property('Clearance', false)
            ";
            await client.SubmitAsync(
                requestScript: gremlinQuery
            );

            await writeOutputAync($"Add entity:\t68719518371");
        }

        {
            string gremlinQuery = $@"
                g.V().has('product', 'id', '68719518391')
            ";
            ResultSet<Product> results = await client.SubmitAsync<Product>(gremlinQuery);
            Product product = results.First();

            await writeOutputAync($"Read entity:\t{product}");
        }

        {
            string category = "gear-surf-surfboards";
            string gremlinQuery = $@"
                g.V().has('product', 'category', '{category}')
            ";

            ResultSet<Product> results = await client.SubmitAsync<Product>(gremlinQuery);
            List<Product> entities = results.ToList();

            foreach (var entity in entities)
            {
                await writeOutputAync($"Found entity:\t{entity.id}\t[{entity.Category}]\t{entity.Name}");
            }
        }
    }
}
