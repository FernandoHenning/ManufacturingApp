# ManufacturingApp API

ManufacturingApp is an API developed using .NET 8 with a RESTful approach for a manufacturing-related application. It manages models like RawMaterial, Supplier, Recipe, and Product, along with their relationships.

## Installation
1. Clone the repository to your local machine using `git clone`.
2. Open the project in your preferred IDE (e.g., Visual Studio, Visual Studio Code).
3. Ensure you have .NET 8 installed on your machine.
4. Restore the required NuGet packages by running `dotnet restore` in the project root directory.

## Configuration
Before running the application, you need to configure the database connection string in the `appsettings.json` file:
```
{
  "ConnectionStrings": 
    "ManufacturingDatabase": "Host=<host>; Database=Manufacturing; Username=<user>; Password=<password>"
  }
}
```
Replace `<host>`, `<user>` and `<password>` with your PostgreSQL server details.

## Usage
1. Build the soluction using you IDE or run `dotnet build` in the project root directory.
2. Run the migrations to create the database schema using Entity Framework Core: `dotnet ef database update`.
3. Start the API by running `dotnet run` in the project root directory.
4. The API will be accesible at `http://localhost:5138` or `https://localhost:7082` if running with HTTPS.

## Endpoints
The API provides the following endpoints:

- GET /api/resource: Retrieves all resources.
- GET /api/resource/{id}: Retrieves a specific resource by ID.
- POST /api/resource: Creates a new resource.
- PUT /api/resource/{id}: Updates an existing resource.
- DELETE /api/resource/{id}: Deletes a resource by ID.

Replace resource with the name of the resource entity.

## Additional Endpoints
- GET /Recipes/OptimizeSuppliers/{recipeId}: Calculates the optimal combination of suppliers for a given recipe to minimize the total cost of ingredients.
- POST /Suppliers/AddRawMaterial: Adds a new raw material to the supplier inventory.
