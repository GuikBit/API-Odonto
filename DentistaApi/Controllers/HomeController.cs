using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DentistaApi.Services;
using DentistaApi.Models;
using DentistaApi.Data;

namespace AtletaBackend.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    public HomeController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserInfo userInfo)
    {
        var UserLogado = await authService.Login(userInfo);

        if (UserLogado.Status == EReturnStatus.Success)
            return Ok(UserLogado);
        else
            return BadRequest(UserLogado);
    }

    [HttpPost("Admin")]
    public ActionResult<Administrador> Post(Administrador obj)
    {
        if (obj == null)
            return BadRequest();

        obj.SetSenhaHash();
        obj.SetRole();

        db.Administrador.Add(obj);
        db.SaveChanges();


        return CreatedAtAction(nameof(GetById), new { id = obj.Id }, obj);

    }

    [HttpPost("Paciente")]
    public ActionResult<Paciente> Post(Paciente obj)
    {
        if (obj == null)
            return BadRequest();

        SalvaInfo(obj);

        return Ok();

    }

    private Paciente SalvaInfo(Paciente obj)
    {
        // Endereco novoEndereco = new Endereco
        // {
        //     Cep = obj.Endereco.Cep,
        //     Rua = obj.Endereco.Rua,
        //     Complemento = obj.Endereco.Complemento,
        //     Bairro = obj.Endereco.Bairro,
        //     Numero = obj.Endereco.Numero,
        //     Cidade = obj.Endereco.Cidade
        // };



        // Responsavel novoResponsavel = new Responsavel
        // {
        //     Cpf = obj.Responsavel.Cpf,
        //     Nome = obj.Responsavel.Nome,
        //     Telefone = obj.Responsavel.Telefone
        // };

        // Anamnese novaAnamnese = new Anamnese
        // {
        //     Tratamento = obj.Anamnese.Tratamento,
        //     Alergia = obj.Anamnese.Alergia,
        //     Gravida = obj.Anamnese.Gravida,
        //     SangramentoExcessivo = obj.Anamnese.SangramentoExcessivo,
        //     Hipertensao = obj.Anamnese.Hipertensao,
        //     TraumatismoFace = obj.Anamnese.TraumatismoFace,
        //     Remedio = obj.Anamnese.Remedio
        // };

        // Paciente novo = new Paciente();

        // novo.Nome = obj.Nome;
        // novo.Email = obj.Email;
        // novo.Login = obj.Login;
        // novo.Senha = obj.Senha;
        // novo.SetSenhaHash();
        // novo.Telefone = obj.Telefone;
        // novo.Cpf = obj.Cpf;
        // novo.dataNasc = obj.dataNasc;
        // novo.SetRole();
        // novo.Endereco = novoEndereco;
        // novo.Responsavel = novoResponsavel;
        // novo.Anamnese = novaAnamnese;

        Paciente novo = new Paciente();

        novo.Nome = obj.Nome;
        novo.Email = obj.Email;
        novo.Login = obj.Login;
        novo.Senha = obj.Senha;
        novo.SetSenhaHash();
        novo.Telefone = obj.Telefone;
        novo.Cpf = obj.Cpf;
        novo.dataNasc = obj.dataNasc;
        novo.SetRole();
        novo.Endereco = obj.Endereco;
        novo.Responsavel = obj.Responsavel;
        novo.Anamnese = obj.Anamnese;

        db.Pacientes.Add(novo);
        db.SaveChanges();

        return novo;


    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Paciente> GetById(int id)
    {

        var paciPaciente = db.Pacientes.FirstOrDefault(x => x.Id == id);

        return paciPaciente == null ? NotFound() : Ok(paciPaciente);
    }


    private readonly IAuthService authService;
    private readonly AppDbContext db = new();
}