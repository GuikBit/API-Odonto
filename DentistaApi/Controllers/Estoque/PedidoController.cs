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
//    public class PedidoController : ControllerBase
//    {
//        private readonly AppDbContext _context = new();

//        public PedidoController(AppDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
//        {
//            return await _context.Pedidos.Include(p => p.DetalhesPedido).ThenInclude(d => d.Produto).ToListAsync();
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Pedido>> GetPedido(int id)
//        {
//            var pedido = await _context.Pedidos.Include(p => p.DetalhesPedido).ThenInclude(d => d.Produto).FirstOrDefaultAsync(p => p.Id == id);

//            if (pedido == null)
//            {
//                return NotFound();
//            }

//            return pedido;
//        }

//        [HttpPost]
//        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
//        {
//            _context.Pedidos.Add(pedido);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
//        {
//            if (id != pedido.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(pedido).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!PedidoExists(id))
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
//        public async Task<IActionResult> DeletePedido(int id)
//        {
//            var pedido = await _context.Pedidos.FindAsync(id);
//            if (pedido == null)
//            {
//                return NotFound();
//            }

//            _context.Pedidos.Remove(pedido);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool PedidoExists(int id)
//        {
//            return _context.Pedidos.Any(e => e.Id == id);
//        }
//    }
//}
