using DealerTrack.Interfaces;
using DealerTrack.Helpers;
using DealerTrack.Models;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DealerTrack.Repositories.Interfaces;

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
                //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "TempFiles");
                //var filePath = "";

                //if (file.Length > 0)
                //{
                //    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                //    filePath = Path.Combine(pathToSave, fileName);
                //    using (var stream = new FileStream(filePath, FileMode.Create))
                //    {
                //         file.CopyTo(stream);
                //    }                   
                //}

                using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF7))
                using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
                {
                    csv.Context.RegisterClassMap<DealershipCsvMap>();
                    var records = csv.GetRecords<Dealerships>().ToList();
                    reader.Close();
                    return records;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void WriteFileAsync(string path, List<Dealerships> dealerships)
        {
            throw new NotImplementedException();
        }
    }
}
