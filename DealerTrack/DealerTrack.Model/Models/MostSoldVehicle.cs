using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DealerTrack.Model.Models
{
    [NotMapped]
    public class MostSoldVehicle
    {
        public string VehicleName { get; set; }
        public int SoldCount { get; set; }
    }
}
