using FacturaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturaAPI.context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option):base(option){
            
        }

        public DbSet<tblCliente> tblCliente { get; set; }
        
        public DbSet<tblFactura> tblFactura { get; set; }

        public DbSet<tblProducto> tblProducto { get; set; }

        public DbSet<tblDetalleFactura> tblDetalleFactura { get; set; }
    }
}
