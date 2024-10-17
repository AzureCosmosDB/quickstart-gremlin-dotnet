namespace Cosmos.Samples.Gremlin.Quickstart.Web.Models;

public record Product
{
    public string id { get; set; } = $"{Guid.NewGuid()}";

    public string Category { get; set; } = String.Empty;

    public string Name { get; set; } = String.Empty;

    public int Quantity { get; set; } = 0;

    public decimal Price { get; set; } = 0.0m;

    public bool Clearance { get; set; } = false;
};