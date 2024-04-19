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
- Ethylene Oxide: 5 units
- Lauryl Alcohol: 1 unit
- Alcohol Ethoxylate: 1 unit
- Dioxane: 0.2 units

#### Suppliers:

- ABC Chemicals
	- Catalyst: $10 per unit
	- Ethylene Oxide: $20 per unit
	- Lauryl Alcohol: $15 per unit

- XYZ Corporation
	- Catalyst: $12 per unit
	- Ethylene Oxide: $18 per unit
	- Lauryl Alcohol: $17 per unit


### Implement REST API:

Define the necessary models (RawMaterial, Supplier, Recipe, Product) with appropriate properties and relationships.
Create RESTful endpoints for CRUD operations for each model.
Ensure that endpoints handle proper validation and error responses.

### Additional Endpoints:

1. OptimizeSuppliers Endpoint:
	- Description: This endpoint calculates the optimal combination of suppliers for a given recipe to minimize the total cost of ingredients.
	- Parameters:
		- recipeId: The ID of the recipe for which the optimal suppliers need to be determined.
	- Response: A list of suppliers along with their respective prices for each raw material required in the recipe, forming the optimal combination to minimize the total cost.

### Submission:

Provide the source code of the project along with any necessary instructions for setting up and running the APIs within a GitHub repository.