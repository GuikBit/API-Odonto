//using DentistaApi.Data;
//using DentistaApi.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace DentistaApi.Controllers.Estoque
//{
//    [Authorize]
//    [ApiController]
//    [Route("v1/[controller]")]
//    public class FornecedorController : ControllerBase
//    {
//        private readonly AppDbContext _context = new();

//        public FornecedorController(AppDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedores()
//        {
//            return await _context.Fornecedores.ToListAsync();
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Fornecedor>> GetFornecedor(int id)
//        {
//            var fornecedor = await _context.Fornecedores.FindAsync(id);

//            if (fornecedor == null)
//            {
//                return NotFound();
//            }

//            return fornecedor;
//        }

//        [HttpPost]
//        public async Task<ActionResult<Fornecedor>> PostFornecedor(Fornecedor fornecedor)
//        {
//            _context.Fornecedores.Add(fornecedor);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetFornecedor), new { id = fornecedor.Id }, fornecedor);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutFornecedor(int id, Fornecedor fornecedor)
//        {
//            if (id != fornecedor.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(fornecedor).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!FornecedorExists(id))
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
//        public async Task<IActionResult> DeleteFornecedor(int id)
//        {
//            var fornecedor = await _context.Fornecedores.FindAsync(id);
//            if (fornecedor == null)
//            {
//                return NotFound();
//            }

//            _context.Fornecedores.Remove(fornecedor);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool FornecedorExists(int id)
//        {
//            return _context.Fornecedores.Any(e => e.Id == id);
//        }
//    }
//}