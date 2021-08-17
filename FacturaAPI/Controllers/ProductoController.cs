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
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext bd;

        public ProductoController(ApplicationDbContext context)
        {
            this.bd = context;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<tblProducto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ObtenerProductos()
        {
            var lista = await bd.tblProducto.OrderBy(c => c.nombre).ToListAsync();
            if (lista == null)
            {
                return NotFound();
            }
            return Ok(lista);
        }

        [HttpGet("{id:int}", Name = "ObtenerProducto")]
        [ProducesResponseType(200, Type = typeof(List<tblProducto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ObtenerProducto(int id)
        {
            var producto = await bd.tblProducto.FirstOrDefaultAsync(c => c.codigotblProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CrearProducto(tblProducto producto)
        {
            if (producto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string insertarproducto = "exec insertarPoductos" +
                                    "@nombre='" + producto.nombre+"'," +
                                    "@precio=" + producto.precio + "";

            //
            //var result = await bd.tblProducto.FromSqlRaw("exec insertarProductos {1},{2}",producto.nombre, producto.precio).ToListAsync();
            await bd.AddAsync(producto);
            
            await bd.SaveChangesAsync();

            return CreatedAtRoute("ObtenerProducto", new { id = producto.codigotblProducto }, producto);
        }
    }
}
