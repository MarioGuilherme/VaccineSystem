using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineSystem.Models {
    [NotMapped]
    public class Response {
        public string icon { get; set; }
        public string msg { get; set; }
    }
}