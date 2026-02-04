namespace FakeStoreProxy.Api.Models;

public sealed record Rating
{
    public decimal Rate { get; init; }
    public int Count { get; init; }
}

public sealed record Product
{
    public int Id { get; init; }
    public string Title { get; init; } = default!;
    public decimal Price { get; init; }
    public string Description { get; init; } = default!;
    public string Category { get; init; } = default!;
    public string Image { get; init; } = default!;
    public Rating Rating { get; init; } = default!;
}