using DealerTrack.Controllers;
using DealerTrack.Model.Models;
using DealerTrack.Repositories.Interfaces;
using DealerTrack.Services;
using DealerTrack.Services.Interfaces;
using Moq;
using Serilog;
using System;
using Xunit;

namespace DealerTrack.Test
{
    public class DealershipTest
    {
        #region Property  
        public Mock<IDealershipRepository> mockDealership = new Mock<IDealershipRepository>();
        public Mock<IFileService> mockCsvService = new Mock<IFileService>();
        public Mock<ILogger> mockLogger = new Mock<ILogger>();
        #endregion       

        [Fact]
        public async void GetDealershipDetailsTest()
        {
            var dealerships = new Dealerships()
            {
                DealNumber = 4589,
                CustomerName = "MS",
                DealershipName = "Test Dealership",
                Vehicle = "2018 Ford Mustang",
                Price = 350987,
                Date = DateTime.Now.Date
            };
            mockDealership.Setup(p => p.GetDealershipDetails(4589)).ReturnsAsync(dealerships);
            DealershipsController dealership = new DealershipsController(mockCsvService.Object, mockDealership.Object);
            var result = await dealership.GetDealershipDetailsByDealNumber(4589);
            Assert.True(dealerships.Equals(result));
        }
    }
}
