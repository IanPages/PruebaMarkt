using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaMarktAPI.Data;
using PruebaMarktAPI.Models;

namespace PruebaMarktAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ventasController : ControllerBase
    {
        private readonly pruebamarktContext _context;

        public ventasController(pruebamarktContext context)
        {
            _context = context;
        }

        // GET: api/ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<venta>>> Getventas()
        {
            return await _context.ventas.ToListAsync();
        }

        // GET: api/ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<venta>> Getventa(int id)
        {
            var venta = await _context.ventas.FindAsync(id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }

        // PUT: api/ventas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putventa(int id, venta venta)
        {
            if (id != venta.idventas)
            {
                return BadRequest();
            }

            _context.Entry(venta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ventaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ventas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<venta>> Postventa(venta venta)
        {
            _context.ventas.Add(venta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ventaExists(venta.idventas))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getventa", new { id = venta.idventas }, venta);
        }

        // DELETE: api/ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteventa(int id)
        {
            var venta = await _context.ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.ventas.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ventaExists(int id)
        {
            return _context.ventas.Any(e => e.idventas == id);
        }
    }
}
