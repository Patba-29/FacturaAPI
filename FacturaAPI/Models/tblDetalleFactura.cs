using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FacturaAPI.Models
{
    public class tblDetalleFactura
    {
        [Key]
        public int codigotblDetalleFactura{get;set;}

        [Required]
        public int tblFacturaId { get; set; }
        
        [ForeignKey("tblFacturaId")]
        public tblFactura Factura { get; set; }

        [Required]
        public int tblProductoId { get; set; }

        [ForeignKey("tblProductoId")]
        public tblProducto Producto { get; set; }

        [Required]
        public int cantidad { get; set; }
    }
}
