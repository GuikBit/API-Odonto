using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DentistaApi.Controllers;
[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class PagamentoController : ControllerBase
{
    [HttpGet]
    public ActionResult<IList<Pagamento>> Get()
    {

        var pagaPagamentos = db.Pagamentos.ToList();

        return Ok(pagaPagamentos);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Pagamento> GetById(int id)
    {

        var pagaPagamento = db.Pagamentos.FirstOrDefault(x => x.Id == id);

        return pagaPagamento == null ? NotFound() : Ok(pagaPagamento);
    }

    [HttpPost]
    public ActionResult<Pagamento> Post(Pagamento obj)
    {
        if (obj == null)
            return BadRequest();

        db.Pagamentos.Add(obj);
        db.SaveChanges();


        return Ok();

    }

    // PUT: api/Atleta/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, Pagamento obj)
    {
        if (id != obj.Id)
            return BadRequest();

        db.Pagamentos.Update(obj);
        db.SaveChanges();

        return NoContent();
    }

    // DELETE: api/Atleta/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (db.Pagamentos == null)
            return NotFound();

        var obj = db.Pagamentos.FirstOrDefault(x => x.Id == id);

        if (obj == null)
            return NotFound();

        db.Pagamentos.Remove(obj);
        db.SaveChanges();

        return NoContent();
    }

    private readonly AppDbContext db = new();
}

