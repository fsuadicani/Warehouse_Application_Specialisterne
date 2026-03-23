using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.DTOs.DataTransferObjects;
using WarehouseStorage.Services.Factories;
using WarehouseStorage.Services.Interfaces;
using WarehouseStorage.Services.Repositories.Interfaces;

namespace WarehouseStorage.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransitController : ControllerBase
    {
        public ITransitService _transitService { get; set; }
        public TransitController(ITransitService transitService)
        {
            _transitService = transitService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> StartTransit([FromBody] TransitDTO request)
        {
            try
            {
                Transit transit = ModelFactory.CreateTransit(request);
                foreach (StockDTO stockDTO in request.Stocks)
                {
                    Stock stock = ModelFactory.CreateStock(stockDTO);
                    transit.Location.Stocks.Add(stock);
                }
                await _transitService.StartTransitToDestination(transit);
                
                return Accepted();
            }
            catch (System.Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> TransitArrived([FromQuery] Guid id)
        {
            try
            {
                await _transitService.TransitHasArrived(id);
                
                return Accepted();
            }
            catch (System.Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}