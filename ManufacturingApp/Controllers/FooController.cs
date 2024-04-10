using Microsoft.AspNetCore.Mvc;

namespace ManufacturingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FooController : ControllerBase
    {
        [HttpGet(Name = "Foo")]
        public FooResrouce Foo()
        {
            return new FooResrouce() { Foo = "bar" };
        }
    }

    public class FooResrouce
    {
        public string Foo { get; set; } = string.Empty;
    }
}
