using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentistaApi.Controllers;

[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class CargoController : ControllerBase
{

    [HttpGet]
    public ActionResult<IList<Cargo>> Get(int idOrg)
    {

        var cargos = db.Cargos
            .Where(x => x.OrganizacaoId == idOrg)
            .OrderBy(p => p.Titulo)
            .ToList();

        return Ok(cargos);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Cargo> GetById(int id)
    {

        var cargo = db.Cargos.FirstOrDefault(x => x.Id == id);

        return cargo == null ? NotFound() : Ok(cargo);
    }

    [HttpPost]
    public ActionResult<Cargo> Post(Cargo obj)
    {

        db.Cargos.Add(obj);
        db.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = obj.Id }, obj);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Cargo obj)
    {
        if (id != obj.Id)
            return BadRequest();

        db.Cargos.Update(obj);
        db.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (db.Cargos == null)
            return NotFound();

        var obj = db.Cargos.FirstOrDefault(x => x.Id == id);

        if (obj == null)
            return NotFound();

        db.Cargos.Remove(obj);
        db.SaveChanges();

        return NoContent();
    }

    private readonly AppDbContext db = new();

}
