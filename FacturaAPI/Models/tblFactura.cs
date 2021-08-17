using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FacturaAPI.Models
{
    public class tblFactura
    {
        [Key]
        public int codigotblFactura { get; set; }
        [Required]
        public DateTime fechaFactura { get; set; }
        
        [Required]
        public int tblClienteId { get; set; }
        
        [ForeignKey("tblClienteId")]
        public tblCliente cliente { get; set; } 
        [Required]
        public decimal Total { get; set; }
    }
}
