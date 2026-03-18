using Microsoft.AspNetCore.Mvc;
using WarehouseStorage.Domain.Models;

namespace WarehouseStorage.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(ProductRepository productRepository) : ControllerBase
    {
        private readonly ProductRepository _productRepository = productRepository;

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null.");
            }
            try
            {
                await _productRepository.Add(product);
                return StatusCode(201, product);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(Guid id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }


        [HttpGet("filter")]
        public async Task<IActionResult> ReadAll(int skip = 0, int take = 100)
        {
            try
            {
                var products = await _productRepository.GetAll(skip, take);
                return Ok(products);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            if (product == null || product.Id == Guid.Empty)
            {
                return BadRequest("Product cannot be null and must have a valid ID.");
            }
            try
            {
                await _productRepository.Update(product);
                return StatusCode(200, product);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid product ID.");
            }
            try
            {
                await _productRepository.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
    }
}