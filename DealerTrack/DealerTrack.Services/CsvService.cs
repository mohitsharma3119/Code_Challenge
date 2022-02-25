using DealerTrack.Helpers;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DealerTrack.Repositories.Interfaces;
using DealerTrack.Services.Interfaces;
using DealerTrack.Model.Models;
using Microsoft.AspNetCore.Http;

namespace DealerTrack.Services
{
    public class CsvService : IFileService
    {
        private readonly IDealershipRepository _dealershipRepository;
        public CsvService(IDealershipRepository dealershipRepository)
        {
            _dealershipRepository = dealershipRepository;
        }
        public List<Dealerships> ReadFileAsync(IFormFile file)
        {
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF7))
                using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
                {
                    csv.Context.RegisterClassMap<DealershipCsvMap>();
                    var records = csv.GetRecords<Dealerships>().ToList();
                    reader.Close();
                    return records;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ProcessFileAsync(IFormFile file)
        {            
            try
            {
                List<Dealerships> dealerships = new List<Dealerships>();
                dealerships = await Task.Run(() => ReadFileAsync(file));
                return await _dealershipRepository.BulkInsertDealershipAsync(dealerships);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void WriteFileAsync(string path, List<Dealerships> dealerships)
        {
            throw new NotImplementedException();
        }
    }
}
