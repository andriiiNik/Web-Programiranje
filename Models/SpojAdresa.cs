using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ButiciBackend.Models
{
    [Table("SpojAdresa")]
    public class SpojAdresa
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        
        [Column("Adresa")]
        [MaxLength(200)]
        public string Adresa { get; set; }

        public Butik Butik { get; set; }
        
        [JsonIgnore]
        public Grad Grad { get; set; }
    }
}
