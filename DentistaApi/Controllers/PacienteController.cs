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
            //.Where(x => x.Ativo == true)
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
    [Route("{id}")]
    public ActionResult<Paciente> GetById(int id)
    {
        var pacienteCompleto = db.Pacientes
                   .Where(x => x.Id == id)
                   .Include(p => p.Endereco)
                   .Include(p => p.Anamnese)
                   .Include(p => p.Responsavel)
                   .FirstOrDefault();

        pacienteCompleto.Endereco.Paciente = null;
        pacienteCompleto.Anamnese.Paciente = null;
        pacienteCompleto.Responsavel.Paciente = null;

        var consultas = db.Consultas
            .Where(x => x.Paciente.Id == id)
            .Include(c => c.Dentista)
            .Include(c => c.Dentista.Especialidade)
            .Include(c => c.Pagamento)
            .ToList();

        consultas.ToList().ForEach(x => x.Dentista.Consultas = null);
        consultas.ToList().ForEach(x => x.Paciente = null);

        pacienteCompleto.Consultas = consultas;

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

            c.Observacao = cons.Observacao;
            c.Procedimentos = cons.Procedimentos;
            c.Dentista = cons.Dentista;
            c.DataConsulta = cons.DataConsulta;
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
    public IActionResult Put(int id, PacienteDTO obj)
    {
        try
        {
            var paciente = db.Pacientes.FirstOrDefault(x => x.Id == id);
            var pAnamnese = db.Anamneses.FirstOrDefault(x => x.PacienteId == id);
            var pEndereco = db.Enderecos.FirstOrDefault(x => x.PacienteId == id);
            var pResponsavel = db.Responsavel.FirstOrDefault(x => x.PacienteId == id);

            if (paciente != null && pAnamnese != null && pEndereco != null && pResponsavel != null && obj.Anamnese != null && obj.Responsavel != null && obj.Endereco != null)
            {
                paciente.Nome = obj.Nome;
                paciente.Email = obj.Email;
                paciente.Telefone = obj.Telefone;
                paciente.NumPasta = obj.NumPasta;

                pAnamnese.ProblemaSaude = obj.Anamnese.ProblemaSaude;
                pAnamnese.Tratamento = obj.Anamnese.Tratamento;
                pAnamnese.Remedio = obj.Anamnese.Remedio;
                pAnamnese.Alergia = obj.Anamnese.Alergia;
                pAnamnese.SangramentoExcessivo = obj.Anamnese.SangramentoExcessivo;
                pAnamnese.Hipertensao = obj.Anamnese.Hipertensao;
                pAnamnese.Gravida = obj.Anamnese.Gravida;
                pAnamnese.TraumatismoFace = obj.Anamnese.TraumatismoFace;

                pResponsavel.Nome = obj.Responsavel.Nome;
                pResponsavel.Cpf = obj.Responsavel.Cpf;
                pResponsavel.Telefone = obj.Responsavel.Telefone;

                pEndereco.Cep = obj.Endereco.Cep;
                pEndereco.Logradouro = obj.Endereco.Logradouro;
                pEndereco.Cidade = obj.Endereco.Cidade;
                pEndereco.Bairro = obj.Endereco.Bairro;
                pEndereco.Numero = obj.Endereco.Numero;
                pEndereco.Complemento = obj.Endereco.Complemento;
                pEndereco.Referencia = obj.Endereco.Referencia;


                db.Pacientes.Update(paciente);
                db.Anamneses.Update(pAnamnese);
                db.Enderecos.Update(pEndereco);
                db.Responsavel.Update(pResponsavel);

                db.SaveChanges();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {

            return BadRequest();
        }

    }


    [HttpDelete]
    [Route("/v1/paciente/inativar/{id}")]
    public IActionResult InativarPaciente(int id)
    {

        var obj = db.Pacientes.FirstOrDefault(x => x.Id == id);

        if (obj == null)
            return NotFound();

        obj.Ativo = false;
        db.Pacientes.Update(obj);
        //db.Pacientes.Remove(obj);
        db.SaveChanges();

        return NoContent();
    }
    [HttpDelete]
    [Route("/v1/paciente/reativar/{id}")]
    public IActionResult ReativarPaciente(int id)
    {

        var obj = db.Pacientes.FirstOrDefault(x => x.Id == id);

        if (obj == null)
            return NotFound();

        obj.Ativo = true;
        db.Pacientes.Update(obj);

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
        if (paciente != null)
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
