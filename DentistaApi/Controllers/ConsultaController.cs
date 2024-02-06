using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
                                    .Include(p=> p.ConsultaEspecialidade)
                                    .OrderBy(p => p.DataConsulta)
                                    .ToList();


        return Ok(ajustaConsultas(consultas));
    }
    private IList<Consulta> ajustaConsultas(List<Consulta> consultas)
    {
        consultas.ToList().ForEach(p => p.Paciente.Consultas.Clear());
        consultas.ToList().ForEach(p => p.Dentista.Consultas.Clear());

        return consultas;
    }

    //private IList<ConsultaDTO> ajustaConsultas(List<Consulta> consultas)
    //{
    //    consultas.ToList().ForEach(p => p.Paciente.Consultas.Clear());
    //    consultas.ToList().ForEach(p => p.Dentista.Consultas.Clear());

    //    IList<ConsultaDTO> lista = new List<ConsultaDTO>();
    //    foreach (var consulta in consultas)
    //    {
    //        ConsultaDTO c = new ConsultaDTO();
    //        c.Id = consulta.Id;

    //        c.ProcedimentoConsulta = consulta.ProcedimentoConsulta;
    //        c.Dentista = consulta.Dentista;
    //        c.DataConsulta = consulta.DataConsulta;
    //        c.HoraConsulta = consulta.HoraConsulta;
    //        c.Paciente = consulta.Paciente;
    //        c.Pagamento = consulta.Pagamento;

    //        lista.Add(c);
    //    }

    //    return lista;
    //}


    //[HttpGet]
    //[Route("/v1/consultas")]
    //public ActionResult<IList<Consulta>> GetConsultasWeb([FromQuery] int page, [FromQuery] int size)
    //{

    //    int indiceInicial = (page - 1) * size;


    //    var consultasDaPagina = db.Consultas
    //        .Include(p => p.Dentista)
    //        .Include(p => p.Paciente)
    //        .Include(p => p.Pagamento)
    //        .Include(p => p.Dentista.Especialidade)
    //        .Skip(indiceInicial)
    //        .Take(size)
    //        .OrderBy(x => x.DataConsulta)
    //        .ToList();


    //    return consultasDaPagina == null ? NotFound() : Ok(ajustaConsultas(consultasDaPagina));

    //}
    //[HttpGet]
    //[Route("/v1/consultas/total")]
    //public ActionResult<int> getTotalConsultas()
    //{

    //    int total = db.Consultas.Count();

    //    return Ok(total);
    //}

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Consulta> GetById(int id)
    {

        var consulta = db.Consultas
            .Include(p => p.Dentista)
            .Include(p => p.Paciente)
            .Include(p => p.Pagamento)
            .Include(p => p.ConsultaEspecialidade)
            .FirstOrDefault(p => p.Id == id);

        consulta.Paciente.Consultas.Clear();
        consulta.Dentista.Consultas.Clear();

        return consulta == null ? NotFound() : Ok(consulta);
    }

    [HttpPost]
    public ActionResult<Consulta> Post(ConsultaDTO obj)
    {

        try
        {
            var dentista = db.Dentistas.First(x => x.Id == obj.Dentista.Id);
            var paciente = db.Pacientes.First(x => x.Id == obj.Paciente.Id);
            var consEspec = db.ConsultaEspecialidades.First(x => x.Id == obj.ConsultaEspecialidade.Id);

            Consulta nova = new Consulta();
            nova.Paciente = paciente;
            nova.Dentista = dentista;
            nova.ConsultaEspecialidade = consEspec;
            nova.Pagamento = new Pagamento();
            nova.Observacao = obj.Observacao;
            nova.Procedimentos = obj.Procedimentos;
            nova.DataConsulta = ajustaDataConsulta(obj.DataConsulta, obj.HoraConsulta);
            nova.TempoPrevisto = obj.TempoPrevisto;
            nova.setTempoPrevisto(obj.TempoPrevisto);            

            db.Consultas.Add(nova);
            db.SaveChanges();

            return Ok();

        }
        catch (Exception)
        {

            return BadRequest("Erro ao salvar ao consulta.");
        }

        

    }

    private DateTime ajustaDataConsulta(DateTime dataConsulta, string horaConsulta)
    {
        try
        {
            if (dataConsulta != null && horaConsulta != null)
            {
                string[] partes = horaConsulta.Split(':');

                return new DateTime(dataConsulta.Year, dataConsulta.Month, dataConsulta.Day, int.Parse(partes[0]), int.Parse(partes[1]), 0); ;
            }
            else
            {
                return new DateTime(0, 0, 0, 0, 0, 0);
            }
        }
        catch (Exception ex)
        {
            return new DateTime(0,0,0,0,0,0);
        }
    }

    [HttpGet]
    [Route("/v1/consultas/novaespec")]
    public ActionResult<ConsultaEspecialidade> GetEspecConsulta()
    {
        var lista = db.ConsultaEspecialidades.ToList();

        return lista == null ? NotFound() : Ok(lista);
    }

    [HttpPost]
    [Route("/v1/consultas/novaespec")]
    public ActionResult<ConsultaEspecialidade> PostEspecConsulta(ConsultaEspecialidade obj)
    {
        try
        {
            if(obj != null )
            {
                ConsultaEspecialidade novo = new ConsultaEspecialidade();
                novo.Tipo = obj.Tipo;
                novo.Descricao = obj.Descricao;  
                novo.ValorBase = obj.ValorBase;

                db.ConsultaEspecialidades.Add(novo);
                db.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound();
            }
            
        }
        catch (Exception e)
        {
            return NotFound();
        }
        
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

    [HttpGet]
    [Route("/v1/consulta/iniciar/{id}")]
    public ActionResult iniciarConsulta( int id)
    {
        if (id == 0 || id == null)
        {
            return BadRequest();
        }
        var consulta = db.Consultas.FirstOrDefault(x => x.Id.Equals(id));

        if (consulta == null)
        {
            return BadRequest();
        }

        consulta.setIniciarConsulta();
        db.SaveChanges();

        return Ok();
    }

    [HttpGet]
    [Route("/v1/consulta/finalizar/{id}")]
    public ActionResult finalizarConsulta(int id)
    {
        if (id == 0 || id == null)
        {
            return BadRequest();
        }
        var consulta = db.Consultas.FirstOrDefault(x => x.Id.Equals(id));

        if (consulta == null)
        {
            return BadRequest();
        }

        consulta.setFinalizarConsulta();
        db.SaveChanges();

        return Ok();
    }

    [HttpGet]
    [Route("/v1/consulta/ausentar/{id}")]
    public ActionResult ausentarPaciente(int id)
    {
        if (id == 0 || id == null)
        {
            return BadRequest();
        }
        var consulta = db.Consultas.FirstOrDefault(x => x.Id.Equals(id));

        if (consulta == null)
        {
            return BadRequest();
        }
        consulta.setAusentarPaciente();
        db.SaveChanges();

        return Ok();
    }

    [HttpGet]
    [Route("/v1/consulta/presenca/{id}")]
    public ActionResult presencaPaciente(int id)
    {
        if (id == 0 || id == null)
        {
            return BadRequest();
        }
        var consulta = db.Consultas.FirstOrDefault(x => x.Id.Equals(id));

        if (consulta == null)
        {
            return BadRequest();
        }
        consulta.setPresencaPaciente();
        db.SaveChanges();

        return Ok();
    }

    private readonly AppDbContext db = new();


}
