using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentistaApi.Controllers;

[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class PacienteController : ControllerBase
{
    [HttpGet]
    public ActionResult<IList<Paciente>> Get()
    {

        var paciPacientes = db.Pacientes
            .Where(x => x.Ativo == true)
            .Include(p => p.Anamnese)
            .Include(p => p.Endereco)
            .Include(p => p.Responsavel)
            .ToList();


        return Ok(paciPacientes);
    }
    [HttpGet]
    [Route("{id}")]
    public ActionResult<Paciente> GetById(int id)
    {
        var pacienteCompleto = db.Pacientes
                   .Where(x => x.Ativo == true)
                   .Include(p => p.Endereco)
                   .Include(p => p.Anamnese)
                   .Include(p => p.Responsavel)
                   .FirstOrDefault(p => p.Id == id);


        return pacienteCompleto == null ? NotFound() : Ok(pacienteCompleto);
    }

    [HttpGet]
    [Route("/v1/paciente/consultas/{id}")]
    public ActionResult<Paciente> GetByConsultasId(int id)
    {
        var pacienteConsultas = db.Pacientes
            .Where(x => x.Ativo == true)
            .Include(p => p.Consultas)
            .FirstOrDefault(p => p.Id == id);

        return pacienteConsultas == null ? NotFound() : Ok(pacienteConsultas);
    }

    [HttpPost]
    public ActionResult<Paciente> Post(Paciente obj)
    {
        if (obj == null)
            return BadRequest();

        obj.SetSenhaHash();
        obj.SetRole();

        db.Pacientes.Add(obj);
        db.SaveChanges();


        return Ok();

    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Paciente obj)
    {
        if (id != obj.Id)
            return BadRequest();

        db.Pacientes.Update(obj);
        db.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (db.Pacientes == null)
            return NotFound();

        var obj = db.Pacientes.FirstOrDefault(x => x.Id == id);

        if (obj == null)
            return NotFound();

        //obj.Ativo = false;
        //db.Pacientes.Update(obj);
        db.Pacientes.Remove(obj);
        db.SaveChanges();

        return NoContent();
    }



    private readonly AppDbContext db = new();
}
