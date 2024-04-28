using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Responses.Product;

public record GetProducts([Required] List<GetProduct> Products);