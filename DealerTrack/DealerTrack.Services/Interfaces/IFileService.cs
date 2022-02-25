using DealerTrack.Model.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerTrack.Services.Interfaces
{
    public interface IFileService
    {
        List<Dealerships> ReadFileAsync(IFormFile file);
        Task<bool> ProcessFileAsync(IFormFile file);
        void WriteFileAsync(string path, List<Dealerships> dealerships);
    }
}
