using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using DealerTrack.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DealerTrack.Helpers
{
    public sealed class DealershipCsvMap : ClassMap<Dealerships>
    {        
        public DealershipCsvMap()
        {
            Map(row => row.DealNumber).Name("DealNumber");
            Map(row => row.CustomerName).Name("CustomerName");
            Map(row => row.DealershipName).Name("DealershipName");
            Map(row => row.Vehicle).Name("Vehicle");
            Map(row => row.Price).Name("Price").TypeConverter<CustomDatatypeConverter>();
            Map(row => row.Date).Name("Date").TypeConverter<CustomDatatypeConverter>(); ;
        }
    }
}
