using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ButiciBackend.Models
{
    [Table("Grad")]
    public class Grad
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Naziv")]
        [MaxLength(100)]
        public string NazivGrada { get; set; }

        public virtual List<SpojAdresa> GradAdrese { get; set; }
    }
}
