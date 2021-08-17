using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacturaAPI.Models
{
    
    public class tblCliente
    {
        [Key]
        public int codigotblCliente { get; set; }
        [Required]
        public string NitCliente { get; set; }
        [Required]
        public string nombres { get; set; }
        [Required]
        public string apellidos { get; set; }
        [Required]
        public string direccion { get; set; }
        [Required]
        public string telefono { get; set; }

    }
}
