using System.ComponentModel.DataAnnotations.Schema;

namespace DealerTrack.Model.Models
{
    [NotMapped]
    public class Error
    {
        /// <summary>
        /// Name of the property that contains the error
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Error code
        /// </summary>
        public string ErrorCode { get; set; }
    }
}
