using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ButiciBackend.Models
{
    [Table("Artikal")]
    public class Artikal
    {
        [Key]
        [Column("Sifra")]
        public int Sifra { get; set; }

        [Column("Brend")]
        [MaxLength(100)]
        public string Brend { get; set; }

        [Column("Model")]
        [MaxLength(150)]
        public string Model { get; set; }

        [Column("Cena")]
        [Required]
        public int Cena { get; set; }
        
        [JsonIgnore]
        public virtual List<SpojVelicina> Butici { get; set; }
    }
}
