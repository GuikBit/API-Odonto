using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DentistaApi.Controllers;
[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class EspecialidadeController : ControllerBase
{
    [HttpGet]
    public ActionResult<IList<Especialidade>> Get()
    {

        var Especialidades = db.Especialidades.ToList();

        return Ok(Especialidades);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Especialidade> GetById(int id)
    {

        var espEspecialidade = db.Especialidades.FirstOrDefault(x => x.Id == id);

        return espEspecialidade == null ? NotFound() : Ok(espEspecialidade);
    }

    [HttpPost]
    public ActionResult<Especialidade> Post(Especialidade obj)
    {

        db.Especialidades.Add(obj);
        db.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = obj.Id }, obj);
    }

    // PUT: api/Atleta/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, Especialidade obj)
    {
        if (id != obj.Id)
            return BadRequest();

        db.Especialidades.Update(obj);
        db.SaveChanges();

        return NoContent();
    }

    // DELETE: api/Atleta/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (db.Especialidades == null)
            return NotFound();

        var obj = db.Especialidades.FirstOrDefault(x => x.Id == id);

        if (obj == null)
            return NotFound();

        db.Especialidades.Remove(obj);
        db.SaveChanges();

        return NoContent();
    }

    private readonly AppDbContext db = new();
}
