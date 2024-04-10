# ManufacturingApp

Develop an API for a manufacturing-related application using .NET 8: using a RESTful approach. The application has the following models:

- RawMaterial: Name, Description
- Supplier: Name, Description, Price for each associated RawMaterial
- Recipe: Name, Description
- Product: Name, Description, Selling Price

### Relationships:

A Recipe has one or more products and requires specific raw materials supplied by one or more suppliers.
Each supplier provides its own price for the raw materials it supplies.

Hint: you should use Entity Framework Core to handle DB migrations, model relationships, queries, etc.


### Implement REST API:

Define the necessary models (RawMaterial, Supplier, Recipe, Product) with appropriate properties and relationships.
Create RESTful endpoints for CRUD operations for each model.
Ensure that endpoints handle proper validation and error responses.

### Submission:

Provide the source code of the project along with any necessary instructions for setting up and running the APIs within a GitHub repository.