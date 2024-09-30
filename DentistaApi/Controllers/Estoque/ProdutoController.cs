//using DentistaApi.Data;
//using DentistaApi.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace DentistaApi.Controllers.Estoque
//{
//    [Authorize]
//    [ApiController]
//    [Route("v1/[controller]")]
//    public class ProdutoController : ControllerBase
//    {

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
//        {
//            return await db.Produtos.ToListAsync();
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Produto>> GetProduto(int id)
//        {
//            var produto = await db.Produtos.FirstOrDefaultAsync(p => p.Id == id);

//            if (produto == null)
//            {
//                return NotFound();
//            }

//            return produto;
//        }

//        [HttpPost]
//        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
//        {
//            db.Produtos.Add(produto);
//            await db.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutProduto(int id, Produto produto)
//        {
//            if (id != produto.Id)
//            {
//                return BadRequest();
//            }

//            db.Entry(produto).State = EntityState.Modified;

//            try
//            {
//                await db.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ProdutoExists(id))
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
//        public async Task<IActionResult> DeleteProduto(int id)
//        {
//            var produto = await db.Produtos.FindAsync(id);
//            if (produto == null)
//            {
//                return NotFound();
//            }

//            db.Produtos.Remove(produto);
//            await db.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ProdutoExists(int id)
//        {
//            return db.Produtos.Any(e => e.Id == id);
//        }

//        private readonly AppDbContext db = new();
//    }
//}
