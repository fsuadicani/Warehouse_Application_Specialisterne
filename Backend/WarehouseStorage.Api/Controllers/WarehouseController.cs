using Microsoft.AspNetCore.Mvc;
using WarehouseStorage.DTOs.DataTransferObjects;
using WarehouseStorage.Services.Factories;
using WarehouseStorage.Services.Repositories.Interfaces;

namespace WarehouseStorage.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController(IWarehouse warehouseRepository) : ControllerBase
    {
        private const int MaxTake = 1000;
        private readonly IWarehouse _warehouseRepository = warehouseRepository;

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] WarehouseDTO warehouse)
        {
            if (warehouse == null)
            {
                return BadRequest("Warehouse cannot be null.");
            }
            try
            {
                var createdWarehouse = ModelFactory.CreateWarehouse(warehouse);
                await _warehouseRepository.Add(createdWarehouse);
                WarehouseDTO warehouseDTO = ModelFactory.CreateWarehouseDTO(createdWarehouse);
                return StatusCode(201, warehouseDTO);
            }
            catch (Exception e)
            {
                // Log the exception here: _logger.LogError(e, "Failed to add warehouse");
                return StatusCode(500, $"Internal server error occurred while adding the warehouse. {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(Guid id)
        {
            try
            {
                var warehouse = await _warehouseRepository.GetById(id);
                if (warehouse == null)
                {
                    return NotFound();
                }
                WarehouseDTO warehouseDTO = ModelFactory.CreateWarehouseDTO(warehouse);
                if (warehouseDTO == null)
                {
                    return NotFound();
                }
                return Ok(warehouseDTO);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error occurred while retrieving the warehouse.");
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
                var warehouses = await _warehouseRepository.GetAll(skip, take);
                var warehouseDTOs = warehouses.Select(ModelFactory.CreateWarehouseDTO).ToArray();
                return Ok(warehouseDTOs);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error occurred while retrieving the warehouses.");
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] WarehouseDTO warehouse)
        {
             if (warehouse == null || !warehouse.Id.HasValue || warehouse.Id.Value == Guid.Empty)
            {
                return BadRequest("Warehouse cannot be null and must have a valid ID.");
            }

            var warehouseId = warehouse.Id.Value;

            try
            {
                var existingWarehouse = await _warehouseRepository.GetById(warehouseId);
                if (existingWarehouse == null)
                {
                    return NotFound();
                }

                var updatedWarehouse = ModelFactory.CreateWarehouse(warehouse);
                await _warehouseRepository.Update(updatedWarehouse);
                WarehouseDTO warehouseDTO = ModelFactory.CreateWarehouseDTO(updatedWarehouse);
                if (warehouseDTO == null)
                {
                    return NotFound();
                }
                return Ok(warehouseDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var existingWarehouse = await _warehouseRepository.GetById(id);
                if (existingWarehouse == null)
                {
                    return NotFound();
                }
                await _warehouseRepository.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error occurred while deleting the warehouse.");
            }
        }
    }
}