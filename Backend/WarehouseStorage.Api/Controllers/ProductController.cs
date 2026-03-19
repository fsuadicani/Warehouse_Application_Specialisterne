using Microsoft.AspNetCore.Mvc;
using WarehouseStorage.DTOs.DataTransferObjects;
using WarehouseStorage.Services.Factories;
using WarehouseStorage.Services.Repositories.Interfaces;

namespace WarehouseStorage.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        private const int MaxTake = 1000;
        private readonly IProductRepository _productRepository = (IProductRepository)productRepository;
        

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null.");
            }
            try
            {
                var createdProduct = ModelFactory.CreateProduct(product);
                await _productRepository.Add(createdProduct);
                ProductDTO productDTO = ModelFactory.CreateProductDTO(createdProduct);
                return StatusCode(201, productDTO);
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
                ProductDTO productDTO = ModelFactory.CreateProductDTO(product);
                if (productDTO == null)
                {
                    return NotFound();
                }
                return Ok(productDTO);
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
                var productDTOs = products.Select(product => ModelFactory.CreateProductDTO(product)).ToList();
                return Ok(productDTOs);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ProductDTO product)
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

                var updatedProduct = ModelFactory.CreateProduct(product);
                await _productRepository.Update(updatedProduct);
                ProductDTO productDTO = ModelFactory.CreateProductDTO(updatedProduct);
                if (productDTO == null)
                {
                    return NotFound();
                }
                return Ok(productDTO);
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