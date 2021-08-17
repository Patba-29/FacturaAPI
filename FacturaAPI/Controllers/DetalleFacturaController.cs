using FacturaAPI.context;
using FacturaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleFacturaController : ControllerBase
    {
        private readonly ApplicationDbContext bd;
        public DetalleFacturaController(ApplicationDbContext context)
        {
            this.bd = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<tblDetalleFactura>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ObtenerDetallesFacturas()
        {
            var detallefacturas = await bd.tblDetalleFactura.OrderBy(c => c.codigotblDetalleFactura).Include(p => p.Producto)
                .Include(c=>c.Factura).ToListAsync();
            
                return Ok(detallefacturas);
        }

        [HttpGet("{id:int}", Name = "ObtenerDetalleFactura")]
        [ProducesResponseType(200, Type = typeof(List<tblDetalleFactura>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ObteneDetallerFactura(int id)
        {
            var detallefactura = await bd.tblDetalleFactura.Include(p => p.Producto).Include(c=>c.Factura).
                FirstOrDefaultAsync(c => c.codigotblDetalleFactura == id);
            if (detallefactura == null)
            {
                return NotFound();
            }
            return Ok(detallefactura);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CrearDetalleFactura([FromBody] tblDetalleFactura detallefactura)
        {
            if (detallefactura == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await bd.AddAsync(detallefactura);
            await bd.SaveChangesAsync();


            return CreatedAtRoute("ObtenerDetalleFactura", new { id = detallefactura.codigotblDetalleFactura }, detallefactura);
        }
    }
}
