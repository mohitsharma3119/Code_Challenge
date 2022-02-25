using DealerTrack.Repositories.Interfaces;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using DealerTrack.Model.Models;
using DealerTrack.Model;

namespace DealerTrack.Repositories
{
    public class DealershipRepository : IDealershipRepository
    {
        private readonly DealershipContext _context;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public DealershipRepository(DealershipContext context, IMemoryCache cache, IConfiguration configuration)
        {
            _context = context;
            _cache = cache;
            _configuration = configuration;
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
            List<Dealerships> dealerships = new List<Dealerships>();
            if (_cache.TryGetValue(_configuration["CacheKey:DealershipsData"], out dealerships))
            {
                return dealerships;
            }
            else
                return await GetAllDealershipsData();
        }

        private async Task<List<Dealerships>> GetAllDealershipsData()
        {
            List<Dealerships> dealerships = new List<Dealerships>();
            dealerships = await _context.Dealerships.ToListAsync();
            _cache.Set(_configuration["CacheKey:DealershipsData"], dealerships, TimeSpan.FromSeconds(5));
            return dealerships;
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
