using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DentistaApi.Controllers;
[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class AnamneseController : ControllerBase
{
    [HttpGet]
    public ActionResult<IList<Anamnese>> Get()
    {

        var anaAnamneses = db.Anamneses.ToList();

        return Ok(anaAnamneses);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Anamnese> GetById(int id)
    {

        var anaAnamnese = db.Anamneses.FirstOrDefault(x => x.Id == id);

        return anaAnamnese == null ? NotFound() : Ok(anaAnamnese);
    }

    [HttpPost]
    public ActionResult<Anamnese> Post(Anamnese obj)
    {
        

        db.Anamneses.Add(obj);
        db.SaveChanges();


        return CreatedAtAction(nameof(GetById), new { id = obj.Id }, obj);

    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Anamnese obj)
    {
        if (id != obj.Id)
            return BadRequest();

        db.Anamneses.Update(obj);
        db.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (db.Anamneses == null)
            return NotFound();

        var obj = db.Anamneses.FirstOrDefault(x => x.Id == id);

        if (obj == null)
            return NotFound();

        db.Anamneses.Remove(obj);
        db.SaveChanges();

        return NoContent();
    }

    private readonly AppDbContext db = new();
}
