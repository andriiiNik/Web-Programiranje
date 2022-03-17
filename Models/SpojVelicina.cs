using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ButiciBackend.Models
{
    [Table("SpojVelicina")]
    public class SpojVelicina
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        
        [Column("Velicina")]
        [MaxLength(50)]
        public string Velicina { get; set; }

        public Butik Butik { get; set; }
        
        public Artikal Artikal { get; set; }
    }
}
