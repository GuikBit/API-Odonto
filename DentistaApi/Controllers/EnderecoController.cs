using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DentistaApi.Controllers;
[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class EnderecoController : ControllerBase
{
    [HttpGet]
    public ActionResult<IList<Endereco>> Get()
    {

        var enEnderecos = db.Enderecos.ToList();

        return Ok(enEnderecos);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Endereco> GetById(int id)
    {

        var enEndereco = db.Enderecos.FirstOrDefault(x => x.Id == id);

        return enEndereco == null ? NotFound() : Ok(enEndereco);
    }

    [HttpPost]
    public ActionResult<Endereco> Post(Endereco obj)
    {


        db.Enderecos.Add(obj);
        db.SaveChanges();


        return CreatedAtAction(nameof(GetById), new { id = obj.Id }, obj);

    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Endereco obj)
    {
        if (id != obj.Id)
            return BadRequest();

        db.Enderecos.Update(obj);
        db.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (db.Enderecos == null)
            return NotFound();

        var obj = db.Enderecos.FirstOrDefault(x => x.Id == id);

        if (obj == null)
            return NotFound();

        db.Enderecos.Remove(obj);
        db.SaveChanges();

        return NoContent();
    }

    private readonly AppDbContext db = new();
}
