using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.DTOs.Responses.Product;

public record GetProducts([Required] List<GetProduct> Products);