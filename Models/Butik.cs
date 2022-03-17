using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ButiciBackend.Models
{
    [Table("Butik")]
    public class Butik
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Naziv")]
        [MaxLength(150)]
        public string NazivButika { get; set; }

        [Column("Kontakt telefon")]
        [MaxLength(50)]
        public int Telefon { get; set; }

        [JsonIgnore]
        public virtual  List<SpojVelicina> Artikli { get; set; }
        
        [JsonIgnore]
        public virtual List<SpojAdresa> ButikAdresa { get; set; }

    }
}
