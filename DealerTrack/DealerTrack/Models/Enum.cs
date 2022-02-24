using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace DealerTrack.Models
{
    [NotMapped]
    public static class NReasonCode
    {
        public const string ExceptionError = "500";
    }

    [NotMapped]
    public static class NReasonForCode
    {
        public static Hashtable Hashtbl = new Hashtable
        {
            {"500", "Internal Server Error"}
        };
    }
}
