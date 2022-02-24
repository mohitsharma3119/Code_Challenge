using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DealerTrack.Models;
using DealerTrack.Interfaces;
using System.Linq;
using DealerTrack.Helpers;
using DealerTrack.Repositories.Interfaces;

namespace DealerTrack.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class DealershipsController : Controller
    {
        private readonly IFileService _csvService;
        private readonly IDealershipRepository _dealershipRepository;

        public DealershipsController(IFileService csvService, IDealershipRepository dealershipRepository)
        {
            _csvService = csvService;
            _dealershipRepository = dealershipRepository;
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
                List<Dealerships> dealershipList = new List<Dealerships>();
                dealershipList = await _dealershipRepository.GetAllAsync();
                return StatusCode(200, dealershipList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet]
        [Route("GetMostSoldVehicle")]
        public IActionResult GetMostSoldVehicle()
        {
            try
            {
                MostSoldVehicle mostSold = new MostSoldVehicle();
                mostSold = _dealershipRepository.GetByMostSoldVehicleAsync();
                return StatusCode(200, mostSold);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        // POST: Dealerships/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,DealNumber,CustomerName,DealershipName,Vehicle,Price,Date")] Dealerships dealerships)
        {
            try
            {
                await _dealershipRepository.CreateDealershipAsync(dealerships);
                return StatusCode(201, "Created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }

        [HttpPost]
        [Route("UploadCsv")]
        public async Task<IActionResult> UploadCsv()
        {
            var listError = new List<Error>()
                {
                    new Error()
                    {
                        ErrorMessage = NReasonForCode.Hashtbl[NReasonCode.ExceptionError].ToString(),
                        ErrorCode = NReasonCode.ExceptionError
                    }
                };
            try
            {
                var file = Request.Form.Files[0];
                if(await _csvService.ProcessFileAsync(file))
                    return Ok("Records Added");
                else
                    return StatusCode(500, listError);
            }
            catch (Exception ex)
            {                
                return StatusCode(500, listError);
            }
        }

        //// GET: Dealerships/Delete/5
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
                return StatusCode(500, ex.Message);
            }
        }
    }
}
