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
    public class FacturaController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public FacturaController(ApplicationDbContext context)
        {
            this.db = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<tblFactura>))]
        [ProducesResponseType(400)]
        public  async Task<IActionResult> ObtenerFacturas()
        {
            var facturas = await db.tblFactura.OrderBy(c => c.codigotblFactura).Include(p => p.cliente).ToListAsync();
            
                return Ok(facturas);
            
        }

        
        [HttpGet("{id:int}", Name = "ObtenerFactura")]
        [ProducesResponseType(200, Type = typeof(List<tblFactura>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ObtenerFactura(int id)
        {
            var factura = await db.tblFactura.Include(p => p.cliente).FirstOrDefaultAsync(c => c.codigotblFactura == id);
            if (factura == null)
            {
                return NotFound();
            }
            return Ok(factura);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CrearFactura([FromBody]tblFactura factura)
        {
            if (factura == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await db.AddAsync(factura);
            await db.SaveChangesAsync();

            return Ok();
            //return CreatedAtRoute("ObtenerFactura", new { id = factura.codigotblCliente }, factura);
        }
    }
}
