# FakeStoreProxy

Lightweight ASP.NET Core proxy around `https://fakestoreapi.com` that adds paging, name search, and Swagger docs.

Public [SwaggerUI](fakestoreproxy-600239760914.europe-central2.run.app/swagger/) available.

## Quick start

- Requirements: .NET SDK 10.0 (preview) or newer.
- Restore & run: `dotnet restore && dotnet run --project src/FakeStoreProxy.Api` (launch profile serves Swagger).

## Configuration

- `FakeStore:BaseUrl` (default `https://fakestoreapi.com/`)
- `FakeStore:TimeoutSeconds` (default `10`)
  Override via `appsettings.*.json`.

## Endpoints

- `GET /api/products?name={text}&page=1&pageSize=10` - title search.
- `GET /api/categories/{category}/products?page=1&pageSize=10` - products by category.

## Tests

`dotnet test`
