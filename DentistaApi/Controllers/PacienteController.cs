using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
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
            //.Include(p=> p.Consultas)
            //.Include(p => p.Anamnese)
            //.Include(p => p.Endereco)
            //.Include(p => p.Responsavel)
            .OrderBy(p => p.Nome)
            .ToList();

        paciPacientes.ToList().ForEach(p => p.Consultas.Clear());
        //paciPacientes.ToList().ForEach(p => p.Endereco.Clear());
        return Ok(paciPacientes);
    }

    [HttpGet]
    [Route("/v1/pacientes")]
    public ActionResult<IList<Paciente>> GetPacientesWeb([FromQuery] int page, [FromQuery] int size)
    {

        int indiceInicial = (page - 1) * size;


        IList<Paciente> pacientesDaPagina = db.Pacientes
            .Where(x => x.Ativo == true)
            .Skip(indiceInicial)
            .Take(size)
            .OrderBy(p => p.Nome)
            .ToList();


        return pacientesDaPagina == null ? NotFound() : Ok(pacientesDaPagina);

    }

    [HttpGet]
    [Route("/v1/paciente/total")]
    public ActionResult<int> getTotalPacientes() {

        int totalPacientes = db.Pacientes.Count();

        return Ok(totalPacientes);
    }
    [HttpGet]
    [Route("{id}")]
    public ActionResult<Paciente> GetById(int id)
    {
        var pacienteCompleto = db.Pacientes
                   .Where(x => x.Id == id)
                   .Include(p => p.Endereco)
                   .Include(p => p.Anamnese)
                   .Include(p => p.Responsavel)
                   .FirstOrDefault();

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
               .OrderBy(p => p.DataConsulta)
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
    public ActionResult<Paciente> Post(PacienteDTO obj)
    {
        if (obj == null)
            return BadRequest();

        Paciente novo = new Paciente();
        novo.Nome = obj.Nome;
        novo.Email = obj.Email;
        novo.Login = obj.Login;
        novo.Senha = obj.Senha;
        novo.Telefone = obj.Telefone;
        novo.Cpf = obj.Cpf;
        novo.DataNascimento = obj.DataNascimento;
        novo.NumPasta = obj.NumPasta;
        novo.SetSenhaHash();
        novo.SetRole();

        db.Pacientes.Add(novo);
        db.SaveChanges();

        Paciente p = db.Pacientes.OrderByDescending(x => x.Id).FirstOrDefault();

        novo.Id = p.Id;

        Anamnese anamnese = new Anamnese();

        anamnese.Paciente = p;
        anamnese.PacienteId = (int)p.Id;

        anamnese.ProblemaSaude = obj.Anamnese.ProblemaSaude;
        anamnese.Tratamento = obj.Anamnese.Tratamento;
        anamnese.Remedio = obj.Anamnese.Remedio;
        anamnese.Alergia = obj.Anamnese.Alergia;
        anamnese.SangramentoExcessivo = obj.Anamnese.SangramentoExcessivo;
        anamnese.Hipertensao = obj.Anamnese.Hipertensao;
        anamnese.Gravida = obj.Anamnese.Gravida;
        anamnese.TraumatismoFace = obj.Anamnese.TraumatismoFace;

        Responsavel resp = new Responsavel();
        resp.Paciente = p;
        resp.PacienteId = (int)p.Id;

        resp.Nome = obj.Responsavel.Nome;
        resp.Cpf = obj.Responsavel.Cpf;
        resp.Telefone = obj.Responsavel.Telefone;

        Endereco end = new Endereco();
        end.Paciente = p;
        end.PacienteId = (int)p.Id;

        end.Cep = obj.Endereco.Cep;
        end.Logradouro = obj.Endereco.Logradouro;
        end.Cidade = obj.Endereco.Cidade;
        end.Bairro = obj.Endereco.Bairro;
        end.Numero = obj.Endereco.Numero;
        end.Complemento = obj.Endereco.Complemento;
        end.Referencia = obj.Endereco.Referencia;

        db.Enderecos.Add(end);
        db.Responsavel.Add(resp);
        db.Anamneses.Add(anamnese);
        db.Pacientes.Update(novo);

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

    [HttpGet]
    [Route("/v1/paciente/validaLogin")]
    public bool validaLoginPaciente(string login)
    {
        Paciente paciente = db.Pacientes.FirstOrDefault(p => p.Login == login);
        if (paciente == null)
        {
            return true;
        }
        else
        {
            return false;
        }

      
    }
    [HttpGet]
    [Route("/v1/paciente/validaCPF")]
    public bool validaCpfPaciente(string cpf)
    {
        Paciente paciente = db.Pacientes.FirstOrDefault(p => p.Cpf == cpf);
        if (paciente == null)
        {
            return false;
        }
        else
        {
            return true;
        }


    }

    [HttpGet]
    [Route("/v1/paciente/buscarPaciente")]
    public ActionResult<IList<Paciente>> buscarPaciente([FromQuery] string search, [FromQuery] int page, [FromQuery] int size)
    {
        try
        {
            int indiceInicial = (page - 1) * size;

            var pacientes = db.Pacientes.Where(p => p.Nome.ToUpper().Contains(search.ToUpper()))
                .OrderBy(p => p.Nome)
                .Skip(indiceInicial)
                .Take(size)
                .ToList();

            return Ok(pacientes); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocorreu um erro interno" });
        }


    }



    private readonly AppDbContext db = new();
}
