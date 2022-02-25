using DealerTrack.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerTrack.Repositories.Interfaces
{
    public interface IDealershipRepository
    {
        Task<List<Dealerships>> GetAllAsync();
        Task<Dealerships> GetDealershipDetails(int dealNumber);
        MostSoldVehicle GetByMostSoldVehicleAsync();
        Task<bool> CreateDealershipAsync(Dealerships dealership);
        Task<bool> BulkInsertDealershipAsync(List<Dealerships> dealership);
    }
}
