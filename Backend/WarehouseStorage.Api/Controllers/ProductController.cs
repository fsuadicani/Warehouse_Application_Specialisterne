using Microsoft.AspNetCore.Mvc;
using WarehouseStorage.Domain.Models;

namespace WarehouseStorage.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(ProductRepository productRepository) : ControllerBase
    {
        private const int MaxTake = 1000;
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
            catch (Exception)
            {
                // Log the exception here: _logger.LogError(e, "Failed to add product");
                return StatusCode(500, "Internal server error occurred while adding the product.");
            }        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(Guid id)
        {
            try
            {
                var product = await _productRepository.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error occurred while retrieving the product.");
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> ReadAll(int skip = 0, int take = 100)
        {
            if (take < 1 || take > MaxTake)
            {
                return BadRequest($"Parameter 'take' must be between 1 and {MaxTake}.");
            }

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
            if (product == null || !product.Id.HasValue || product.Id.Value == Guid.Empty)
            {
                return BadRequest("Product cannot be null and must have a valid ID.");
            }

            var productId = product.Id.Value;

            try
            {
                var existingProduct = await _productRepository.GetById(productId);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                await _productRepository.Update(product);
                return Ok(product);
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
                var product = await _productRepository.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }

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