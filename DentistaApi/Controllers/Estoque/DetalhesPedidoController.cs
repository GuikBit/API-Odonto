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
//    public class DetalhePedidoController : ControllerBase
//    {
//        private readonly AppDbContext _context = new();

//        public DetalhePedidoController(AppDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<DetalhePedido>>> GetDetalhesPedido()
//        {
//            return await _context.DetalhePedidos.Include(d => d.Produto).Include(d => d.Pedido).ToListAsync();
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<DetalhePedido>> GetDetalhePedido(int id)
//        {
//            var detalhePedido = await _context.DetalhePedidos.Include(d => d.Produto).Include(d => d.Pedido).FirstOrDefaultAsync(d => d.PedidoId == id);

//            if (detalhePedido == null)
//            {
//                return NotFound();
//            }

//            return detalhePedido;
//        }

//        [HttpPost]
//        public async Task<ActionResult<DetalhePedido>> PostDetalhePedido(DetalhePedido detalhePedido)
//        {
//            _context.DetalhePedidos.Add(detalhePedido);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetDetalhePedido), new { id = detalhePedido.PedidoId }, detalhePedido);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutDetalhePedido(int id, DetalhePedido detalhePedido)
//        {
//            if (id != detalhePedido.PedidoId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(detalhePedido).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!DetalhePedidoExists(id))
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
//        public async Task<IActionResult> DeleteDetalhePedido(int id)
//        {
//            var detalhePedido = await _context.DetalhePedidos.FindAsync(id);
//            if (detalhePedido == null)
//            {
//                return NotFound();
//            }

//            _context.DetalhePedidos.Remove(detalhePedido);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool DetalhePedidoExists(int id)
//        {
//            return _context.DetalhePedidos.Any(e => e.PedidoId == id);
//        }
//    }
//}
