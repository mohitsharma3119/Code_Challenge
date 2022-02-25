using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DealerTrack.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Serilog;
using DealerTrack.Model.Models;
using DealerTrack.Model.Enums;
using DealerTrack.Services.Interfaces;

namespace DealerTrack.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class DealershipsController : Controller
    {
        private readonly IFileService _csvService;
        private readonly IDealershipRepository _dealershipRepository;
        private readonly ILogger<DealershipsController> _logger;

        public DealershipsController(IFileService csvService, IDealershipRepository dealershipRepository, ILogger<DealershipsController> logger)
        {
            _csvService = csvService;
            _dealershipRepository = dealershipRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Dealerships> GetDealershipDetailsByDealNumber(int dealNumber)
        {
            var result = await _dealershipRepository.GetDealershipDetails(dealNumber);
            return result;
        }

        // GET: Dealerships
        [HttpGet]
        [Route("GetDealerships")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                Log.Information($"GET GetDealerships called at {DateTime.Now}");

                return Ok(await _dealershipRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error while fetching records GetAll controller at {DateTime.Now}");
                var listError = new List<Error>()
                {
                    new Error()
                    {
                        ErrorMessage = NReasonForCode.Hashtbl[NReasonCode.ExceptionError].ToString(),
                        ErrorCode = NReasonCode.ExceptionError
                    }
                };
                return StatusCode(500, listError);
            }
            
        }

        [HttpGet]
        [Route("GetMostSoldVehicle")]
        public IActionResult GetMostSoldVehicle()
        {
            try
            {
                Log.Information($"GET GetMostSoldVehicle called at {DateTime.Now}");

                return Ok(_dealershipRepository.GetByMostSoldVehicleAsync());
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error while fetching data GetMostSoldVehicle controller at {DateTime.Now}");
                var listError = new List<Error>()
                {
                    new Error()
                    {
                        ErrorMessage = NReasonForCode.Hashtbl[NReasonCode.ExceptionError].ToString(),
                        ErrorCode = NReasonCode.ExceptionError
                    }
                };
                return StatusCode(500, listError);
            }

        }

        // POST: Dealerships/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,DealNumber,CustomerName,DealershipName,Vehicle,Price,Date")] Dealerships dealerships)
        {
            try
            {
                Log.Information($"POST Create controller called at {DateTime.Now}");
                
                return Ok(await _dealershipRepository.CreateDealershipAsync(dealerships));
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error while creating dealership at {DateTime.Now}");
                var listError = new List<Error>()
                {
                    new Error()
                    {
                        ErrorMessage = NReasonForCode.Hashtbl[NReasonCode.ExceptionError].ToString(),
                        ErrorCode = NReasonCode.ExceptionError
                    }
                };
                return StatusCode(500, listError);
            }
           
        }

        [HttpPost]
        [Route("UploadCsv")]
        public async Task<IActionResult> UploadCsv()
        {
            Log.Information($"POST UploadCsv controller called at {DateTime.Now}");            
            try
            {
                var file = Request.Form.Files[0];
                return Ok(await _csvService.ProcessFileAsync(file));  
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error while adding dealerships in UploadCSV controller at {DateTime.Now}");
                var listError = new List<Error>()
                {
                    new Error()
                    {
                        ErrorMessage = NReasonForCode.Hashtbl[NReasonCode.ExceptionError].ToString(),
                        ErrorCode = NReasonCode.ExceptionError
                    }
                };
                return StatusCode(500, listError);
            }
        }

        //// GET: Dealerships/Delete/5 Placeholder for delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                    //call repository to delete
                    return StatusCode(202, "Deleted");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error while adding dealerships in UploadCSV controller at {DateTime.Now}");
                var listError = new List<Error>()
                {
                    new Error()
                    {
                        ErrorMessage = NReasonForCode.Hashtbl[NReasonCode.ExceptionError].ToString(),
                        ErrorCode = NReasonCode.ExceptionError
                    }
                };
                return StatusCode(500, listError);
            }
        }
    }
}
