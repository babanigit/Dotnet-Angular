using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_Angular_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var products = new[] {
                new { Id = 1, Name = "Pen" },
                new { Id = 2, Name = "Book" }
            };
            return Ok(products);
        }
    }

}