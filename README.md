# FakeStoreProxyApi

ASP.NET Core REST API that proxies product data from the external provider `https://fakestoreapi.com` and exposes two paginated endpoints:

- get products by category
- search products by name

There is available public [SwaggerUI](https://fakestoreproxyapi-600239760914.europe-central2.run.app/swagger/index.html) presentation.

## Endpoints

Base path: `/api/products`

### 1. Get products by category

`GET /api/products/by-category`

Query params:

- `category` (string, required)
- `page` (int, optional, default: `1`)
- `pageSize` (int, optional, default: `10`)

Example:

- `/api/products/by-category?category=jewelery&page=1&pageSize=10`

### 2. Get products by name

`GET /api/products/by-name`

Query params:

- `name` (string, required)
- `page` (int, optional, default: `1`)
- `pageSize` (int, optional, default: `10`)

Example:

- `/api/products/by-name?name=mens&page=2&pageSize=5`

## External provider endpoints used

- `GET https://fakestoreapi.com/products` (source for name search)
- `GET https://fakestoreapi.com/products/category/{category}` (source for category filtering)

## Running locally

```bash
dotnet restore
dotnet run
```
