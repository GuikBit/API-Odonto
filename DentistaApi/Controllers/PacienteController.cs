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
            .Include(p=> p.Consultas)
            .Include(p => p.Anamnese)
            .Include(p => p.Endereco)
            .Include(p => p.Responsavel)
            .ToList();

        paciPacientes.ToList().ForEach(p => p.Consultas.Clear());
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
    public ActionResult<IList<ConsultaDTO>> GetByConsultasId(int id)
    {

        var consulta = db.Consultas
               .Include(p => p.Paciente)
               .Include(p => p.Dentista)
               .Include(p => p.Dentista.Especialidade)
               .Where(p => p.Paciente.Id == id)
               .OrderBy(p=>p.DataConsulta)
               .ToList();

        consulta.ToList().ForEach(p => p.Paciente.Consultas.Clear());
        consulta.ToList().ForEach(p => p.Dentista.Consultas.Clear());

        IList<ConsultaDTO> lista = new List<ConsultaDTO>();
        foreach (var cons in consulta)
        {
            ConsultaDTO c = new ConsultaDTO();
            c.Id = cons.Id;
            c.TempoPrevisto = cons.TempoPrevisto;
            c.ProcedimentoConsulta = cons.ProcedimentoConsulta;
            c.Dentista = cons.Dentista;
            c.DataConsulta = cons.DataConsulta.ToString();
            c.HoraConsulta = cons.HoraConsulta;
            c.Paciente = cons.Paciente;
            c.Pagamento = cons.Pagamento;

            lista.Add(c);
        }

        //return lista;

        return lista == null ? NotFound() : Ok(lista);
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
