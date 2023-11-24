using System.Text.Json;
using System.Text.Json.Serialization;
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
public class ConsultaController : ControllerBase
{
    [HttpGet]
    public ActionResult<IList<Consulta>> Get()
    {
        var consultas = db.Consultas.Include(p => p.Dentista)
                                    .Include(p => p.Paciente)
                                    .Include(p => p.Pagamento)
                                    .Include(p => p.Dentista.Especialidade)
                                    .ToList();

        consultas.ToList().ForEach(p => p.Paciente.Consultas.Clear());
        consultas.ToList().ForEach(p => p.Dentista.Consultas.Clear());

        return Ok(consultas);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Consulta> GetById(int id)
    {

        var consulta = db.Consultas
            .Include(p => p.Dentista)
            .Include(p => p.Paciente)
            .Include(p => p.Pagamento)
            .FirstOrDefault(p => p.Id == id);

        consulta.Paciente.Consultas.Clear();
        consulta.Dentista.Consultas.Clear();

        return consulta == null ? NotFound() : Ok(consulta);
    }

    [HttpPost]
    public ActionResult<Consulta> Post(Consulta obj)
    {

        var dentista = db.Dentistas.First(x => x.Id == obj.Dentista.Id);
        var paciente = db.Pacientes.First(x => x.Id == obj.Paciente.Id);
        //var pagamento = db.Pagamentos.FirstOrDefault(x => x.Id == obj.Pagamento.Id);

        Consulta nova = new Consulta
        {
            Pagamento = new Pagamento(),
            Dentista = dentista,
            Paciente = paciente,
            ProcedimentoConsulta = obj.ProcedimentoConsulta,
            DataConsulta = obj.DataConsulta,
            HoraConsulta = obj.HoraConsulta,
            TempoPrevisto = obj.TempoPrevisto
        };

        db.Consultas.Add(nova);
        db.SaveChanges();


        return Ok();

    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Consulta obj)
    {
        if (id != obj.Id)
            return BadRequest();

        db.Consultas.Update(obj);
        db.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (db.Consultas == null)
            return NotFound();

        var obj = db.Consultas.FirstOrDefault(x => x.Id == id);

        if (obj == null)
            return NotFound();

        db.Consultas.Remove(obj);
        db.SaveChanges();

        return NoContent();
    }

    private readonly AppDbContext db = new();


}
