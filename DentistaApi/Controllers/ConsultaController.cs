using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
                                    .OrderBy(p => p.DataConsulta)
                                    .ToList();


        return Ok(ajustaConsultas(consultas));
    }

    private IList<ConsultaDTO> ajustaConsultas(List<Consulta> consultas)
    {
        consultas.ToList().ForEach(p => p.Paciente.Consultas.Clear());
        consultas.ToList().ForEach(p => p.Dentista.Consultas.Clear());

        IList<ConsultaDTO> lista = new List<ConsultaDTO>();
        foreach (var consulta in consultas)
        {
            ConsultaDTO c = new ConsultaDTO();
            c.Id = consulta.Id;
            c.TempoPrevisto = consulta.TempoPrevisto;
            c.ProcedimentoConsulta = consulta.ProcedimentoConsulta;
            c.Dentista = consulta.Dentista;
            c.DataConsulta = consulta.DataConsulta.ToString();
            c.HoraConsulta = consulta.HoraConsulta;
            c.Paciente = consulta.Paciente;
            c.Pagamento = consulta.Pagamento;

            lista.Add(c);
        }

        return lista;
    }

    [HttpGet]
    [Route("/v1/consultas")]
    public ActionResult<IList<Consulta>> GetConsultasWeb([FromQuery] int page, [FromQuery] int size)
    {

        int indiceInicial = (page - 1) * size;


        var consultasDaPagina = db.Consultas
            .Include(p => p.Dentista)
            .Include(p => p.Paciente)
            .Include(p => p.Pagamento)
            .Include(p => p.Dentista.Especialidade)
            .Skip(indiceInicial)
            .Take(size)
            .OrderBy(x => x.DataConsulta)
            .ToList();


        return consultasDaPagina == null ? NotFound() : Ok(ajustaConsultas(consultasDaPagina));

    }
    [HttpGet]
    [Route("/v1/consultas/total")]
    public ActionResult<int> getTotalConsultas()
    {

        int total = db.Consultas.Count();

        return Ok(total);
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
    public ActionResult<Consulta> Post(ConsultaDTO obj)
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
            DataConsulta = stringDateFormatada(obj.DataConsulta),
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

    private DateOnly stringDateFormatada(string dataConsulta)
    {
        string formato = "dd/MM/yyyy";

        try
        {
            DateTime dateTime = DateTime.ParseExact(dataConsulta, formato, null);

            DateOnly dataOnly = DateOnly.FromDateTime(dateTime);
            return dataOnly;
        }
        catch (FormatException)
        {
            DateTime dateTime = DateTime.ParseExact("00/00/0000", formato, null);
            DateOnly dataOnly = DateOnly.FromDateTime(dateTime);
            return dataOnly;
        }
    }

    private DateOnly dateStringFormadata(string dataConsulta)
    {
        string formato = "dd/MM/yyyy";

        try
        {
            DateTime dateTime = DateTime.ParseExact(dataConsulta, formato, null);

            DateOnly dataOnly = DateOnly.FromDateTime(dateTime);
            return dataOnly;
        }
        catch (FormatException)
        {
            DateTime dateTime = DateTime.ParseExact("00/00/0000", formato, null);
            DateOnly dataOnly = DateOnly.FromDateTime(dateTime);
            return dataOnly;
        }
    }
}
