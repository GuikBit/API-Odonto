//using DentistaApi.Data;
//using DentistaApi.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace DentistaApi.Controllers
//{
//    [Authorize]
//    [ApiController]
//    [Route("v1/[controller]")]
//    public class CategoriaController : ControllerBase
//    {
//        private readonly AppDbContext db = new();

//        public CategoriaController(AppDbContext context)
//        {
//            db = context;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
//        {
//            return await db.Categorias.ToListAsync();
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Categoria>> GetCategoria(int id)
//        {
//            var categoria = await db.Categorias.FindAsync(id);

//            if (categoria == null)
//            {
//                return NotFound();
//            }

//            return categoria;
//        }

//        [HttpPost]
//        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
//        {
//            db.Categorias.Add(categoria);
//            await db.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
//        {
//            if (id != categoria.Id)
//            {
//                return BadRequest();
//            }

//            db.Entry(categoria).State = EntityState.Modified;

//            try
//            {
//                await db.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!CategoriaExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteCategoria(int id)
//        {
//            var categoria = await db.Categorias.FindAsync(id);
//            if (categoria == null)
//            {
//                return NotFound();
//            }

//            db.Categorias.Remove(categoria);
//            await db.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool CategoriaExists(int id)
//        {
//            return db.Categorias.Any(e => e.Id == id);
//        }
//    }
//}
