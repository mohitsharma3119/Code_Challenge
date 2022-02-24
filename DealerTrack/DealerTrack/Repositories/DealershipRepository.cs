using DealerTrack.Repositories.Interfaces;
using DealerTrack.Helpers;
using DealerTrack.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerTrack.Repositories
{
    public class DealershipRepository : IDealershipRepository
    {
        private readonly DealershipContext _context;

        public DealershipRepository(DealershipContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateDealershipAsync(Dealerships newDealership)
        {
            _context.Add(newDealership);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task<bool> BulkInsertDealershipAsync(List<Dealerships> newDealership)
        {           
            try
            {
                await _context.BulkInsertAsync(newDealership);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteDealershipAsync(int dealNumber)
        {
            try
            {
                var toDelete = _context.Dealerships.Find(dealNumber);
                _context.Dealerships.Remove(toDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }   

        public async Task<List<Dealerships>> GetAllAsync()
        {
            return await _context.Dealerships.ToListAsync();
        }

        public async Task<Dealerships> GetDealershipDetails(int dealNumber)
        {
            var dealership = await _context.Dealerships.FirstOrDefaultAsync(c => c.Id == dealNumber);
            return dealership;
        }

        public  MostSoldVehicle GetByMostSoldVehicleAsync()
        {
            try
            {
                var mostSoldVehicle = (from dlr in _context.Dealerships
                                  group dlr by dlr.Vehicle into g
                                  orderby g.Count() descending
                                  select new MostSoldVehicle()
                                  {
                                      VehicleName = g.Key,
                                      SoldCount = g.Count()
                                  });
                return mostSoldVehicle.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
