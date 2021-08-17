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
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext bd;

        public ClienteController(ApplicationDbContext context)
        {
           this.bd= context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type=typeof(List<tblCliente>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ObtenerClientes()
        {
            var lista = await bd.tblCliente.OrderBy(c => c.nombres).ToListAsync();
            if (lista == null)
            {
                return NotFound();
            }
            return Ok(lista);
        }
        
        [HttpGet("{id:int}",Name="ObtenerCliente")]
        [ProducesResponseType(200, Type = typeof(List<tblCliente>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ObtenerCliente(int id)
        {
            var cliente = await bd.tblCliente.FirstOrDefaultAsync(c => c.codigotblCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CrearCliente([FromBody] tblCliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await bd.AddAsync(cliente);
            await bd.SaveChangesAsync();

            return CreatedAtRoute("ObtenerCliente",new { id=cliente.codigotblCliente},cliente);
        }
    }
}
