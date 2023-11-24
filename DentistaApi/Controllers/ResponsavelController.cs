using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DentistaApi.Controllers;
[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class ResponsavelController : ControllerBase
{

    //[HttpGet]
    //public ActionResult<IList<Responsavel>> Get()
    //{

       

    //    return Ok();
    //}

    //[HttpGet]
    //[Route("{id}")]
    //public ActionResult<Responsavel> GetById(int id)
    //{

    //    var enEndereco = db.Enderecos.FirstOrDefault(x => x.Id == id);

    //    return enEndereco == null ? NotFound() : Ok(enEndereco);
    //}

    //[HttpPost]
    //public ActionResult<Responsavel> Post(Responsavel obj)
    //{




    //    return CreatedAtAction(nameof(GetById), new { id = obj.Id }, obj);

    //}

    //[HttpPut("{id}")]
    //public IActionResult Put(int id, Responsavel obj)
    //{
    //    if (id != obj.Id)
    //        return BadRequest();

    //    db.Enderecos.Update(obj);
    //    db.SaveChanges();

    //    return NoContent();
    //}

    //[HttpDelete("{id}")]
    //public IActionResult Delete(int id)
    //{
    //    if (db.Enderecos == null)
    //        return NotFound();

    //    var obj = db.Enderecos.FirstOrDefault(x => x.Id == id);

    //    if (obj == null)
    //        return NotFound();

    //    db.Enderecos.Remove(obj);
    //    db.SaveChanges();

    //    return NoContent();
    //}

    //private readonly AppDbContext db = new();
}
