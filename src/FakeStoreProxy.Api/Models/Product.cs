namespace FakeStoreProxyApi.Models;

public record Rating(decimal Rate, int Count);

public record Product
(
    int Id,
    string Title,
    decimal Price,
    string Description,
    string Category,
    string Image,
    Rating Rating
);