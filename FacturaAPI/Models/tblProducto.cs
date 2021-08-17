using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacturaAPI.Models
{
    public class tblProducto
    {
        [Key]
        public int codigotblProducto{get;set;}
        
        [Required]
        public string nombre { get; set; }
        [Required]
        public decimal precio { get; set; }
    }
}
