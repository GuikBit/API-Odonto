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
    public ActionResult<IList<Paciente>> Get(int idOrg)
    {

        var paciPacientes = db.Pacientes
            //.Where(x => x.Ativo == true)
            //.Include(p=> p.Consultas)
            //.Include(p => p.Anamnese)
            //.Include(p => p.Endereco)
            //.Include(p => p.Responsavel)

            .Where(x=> x.OrganizacaoId == idOrg && x.Ativo)

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

        pacienteCompleto.Endereco = null;
        //pacienteCompleto.Anamnese.Paciente = null;
        //pacienteCompleto.Responsavel.Paciente = null;

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

            c.Observacao = cons.Observacao;
            c.Procedimentos = cons.Procedimentos;
            //c.Dentista = cons.Dentista;
            c.DataConsulta = cons.DataConsulta;
            //c.Paciente = cons.Paciente;
            //c.Pagamento = cons.Pagamento;

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
        //SalvarFotoPerfilPaciente(obj.Id, obj.fotoPerfil);
        novo.Anamnese = obj.Anamnese;
        novo.Endereco = obj.Endereco;
        novo.Responsavel = obj.Responsavel;
        novo.OrganizacaoId = obj.OrganizacaoId;

        db.Pacientes.Add(novo);
        db.SaveChanges();      


        return Ok();

    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, PacienteDTO obj)
    {
        try
        {
            var paciente = db.Pacientes.FirstOrDefault(x => x.Id == id);
            //var pAnamnese = db.Anamnese.FirstOrDefault(x => x. == id);
            var pEndereco = db.Endereco.FirstOrDefault(x => paciente.Id == id);
            //var pResponsavel = db.Responsavel.FirstOrDefault(x => x.PacienteId == id);

            if (paciente != null 
                //&& pAnamnese != null 
                && pEndereco != null 
                //&& pResponsavel != null 
                && obj.Anamnese != null 
                && obj.Responsavel != null 
                && obj.Endereco != null)
            {
                paciente.Nome = obj.Nome;
                paciente.Email = obj.Email;
                paciente.Telefone = obj.Telefone;
                paciente.NumPasta = obj.NumPasta;

                //pAnamnese.ProblemaSaude = obj.Anamnese.ProblemaSaude;
                //pAnamnese.Tratamento = obj.Anamnese.Tratamento;
                //pAnamnese.Remedio = obj.Anamnese.Remedio;
                //pAnamnese.Alergia = obj.Anamnese.Alergia;
                //pAnamnese.SangramentoExcessivo = obj.Anamnese.SangramentoExcessivo;
                //pAnamnese.Hipertensao = obj.Anamnese.Hipertensao;
                //pAnamnese.Gravida = obj.Anamnese.Gravida;
                //pAnamnese.TraumatismoFace = obj.Anamnese.TraumatismoFace;

                //pResponsavel.Nome = obj.Responsavel.Nome;
                //pResponsavel.Cpf = obj.Responsavel.Cpf;
                //pResponsavel.Telefone = obj.Responsavel.Telefone;

                pEndereco.Cep = obj.Endereco.Cep;
                pEndereco.Logradouro = obj.Endereco.Logradouro;
                pEndereco.Cidade = obj.Endereco.Cidade;
                pEndereco.Bairro = obj.Endereco.Bairro;
                pEndereco.Numero = obj.Endereco.Numero;
                pEndereco.Complemento = obj.Endereco.Complemento;
                pEndereco.Referencia = obj.Endereco.Referencia;


                db.Pacientes.Update(paciente);
               // db.Anamnese.Update(pAnamnese);
                db.Endereco.Update(pEndereco);
//                db.Responsavel.Update(pResponsavel);

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

    [HttpPost]
    [Route("/v1/paciente/alterarFoto")]
    public ActionResult AlterarFotoPerfil(int pacienteId, IFormFile novaFotoPerfil)
    {
        if(pacienteId != null && novaFotoPerfil != null) {
            if(SalvarFotoPerfilPaciente(pacienteId, novaFotoPerfil))
                return Ok();
            return BadRequest("Nao foi possivel salvar a foto de perfil");
            
        }
        return BadRequest("Houve um erro ao enviar a foto de perfil");
    }
    private Boolean SalvarFotoPerfilPaciente(int patientId, IFormFile file)
    {

        var paciente = db.Pacientes.FirstOrDefault(p => p.Id == patientId);
        if (paciente == null || file == null || file.Length == 0)
            return false;

        var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "Fotos_Perfil");
        if (!Directory.Exists(uploadDirectory))
        {
            Directory.CreateDirectory(uploadDirectory);
        }

        var fileName = $"{patientId}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadDirectory, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
             file.CopyToAsync(stream);
        }

        paciente.FotoPerfil = filePath;
        db.SaveChangesAsync();

        return true;
    }
    
    private readonly AppDbContext db = new();
}
