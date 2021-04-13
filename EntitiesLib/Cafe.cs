using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared {
    [Table("Cafe")]
    public class Cafe {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string BuildingNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}