using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)) ;
        }

        // GET: api/<CatalogController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }

        // GET api/<CatalogController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            Console.WriteLine(product);
            return Ok(product);
        }

        // POST api/<CatalogController>
        [ProducesResponseType(StatusCodes.Status201Created)]     // Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            await _repository.CreateProduct(product);
            //var actionName = nameof(GetProduct);
            //var routeValues = new { id = product.Id };
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        // PUT api/<CatalogController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] Product product)
        {
            return Ok(await _repository.UpdateProduct(product));
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await _repository.DeleteProduct(id));
        }
    }
}
