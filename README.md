# ManufacturingApp

Develop an API for a manufacturing-related application using .NET 8: using a RESTful approach. The application has the following models:

- RawMaterial: Name, Description
- Supplier: Name, Description, Price for each associated RawMaterial
- Recipe: Name, Description, Quantities for each RawMaterial and Product
- Product: Name, Description, Selling Price

### Relationships:

A Recipe has one or more products and requires specific raw materials supplied by one or more suppliers. Each supplier provides its own price for the raw materials it supplies. Each recipe defines the quantity it needs of the raw materials and the quantity it creates of each product.

Hint: Utilize Entity Framework Core to handle DB migrations, model relationships etc. Use LINQ to perform queries.

### Recipe example:

- Name: Alcohol Ethoxylate Production
- Description: This recipe outlines the production process for Alcohol Ethoxylate, a common form of nonionic surfactant.

#### Raw Materials:

- Catalyst
- Ethylene Oxide
- Lauryl Alcohol

#### Products:

- Alcohol Ethoxylate
- Dioxane

#### Quantities:

- Catalyst: 1 unit
- Ethylene Oxide: 5-10 units
- Lauryl Alcohol: 1 unit
- Alcohol Ethoxylate: 1 unit
- Dioxane: 0.2 units


### Implement REST API:

Define the necessary models (RawMaterial, Supplier, Recipe, Product) with appropriate properties and relationships.
Create RESTful endpoints for CRUD operations for each model.
Ensure that endpoints handle proper validation and error responses.

### Submission:

Provide the source code of the project along with any necessary instructions for setting up and running the APIs within a GitHub repository.