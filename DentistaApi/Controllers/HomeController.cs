using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DentistaApi.Models;
using DentistaApi.Data;
using DentistaApi.Services;

namespace AtletaBackend.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    public HomeController(IAuthService authService)
    {
        this.authService = authService;
    }

    public class LoginResponse
    {
        public object Logado { get; set; }
        public Organizacao Org { get; set; }
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserInfo userInfo)
    {
        var UserLogado = await authService.Login(userInfo);

        //var org = await buscaOrganizacao(UserLogado.Usuario.IdOrganizacao.Id);

        if (UserLogado.Status == EReturnStatus.Success)
        {
            //var response = new LoginResponse
            //{
            //    Logado = UserLogado,
            //    Org = (Organizacao) org
            //};
            return Ok(UserLogado);
        }
        else
        {
            return BadRequest(UserLogado);
        }
    }

    private async Task<IActionResult> buscaOrganizacao(int idOrganizacao)
    {
        var org = db.Organizacao.FirstOrDefault(x => x.Id == idOrganizacao);

        if(org == null)
        {
            return BadRequest();
        }

        return Ok(org);
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
        

        Paciente novo = new Paciente();

        novo.Nome = obj.Nome;
        novo.Email = obj.Email;
        novo.Login = obj.Login;
        novo.Senha = obj.Senha;
        novo.SetSenhaHash();
        novo.Telefone = obj.Telefone;
        novo.Cpf = obj.Cpf;
        novo.DataNascimento = obj.DataNascimento;
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

        var paciPaciente = db.Administrador.FirstOrDefault(x => x.Id == id);


        return paciPaciente == null ? NotFound() : Ok(paciPaciente);
    }





    [HttpPost("Organizacao")]
    public ActionResult PostOrganizacao([FromBody] Organizacao novo)
    {
        try
        {
            if (novo == null)
            {
                return BadRequest("Nem uma informacao foi passada");
            }

            db.Organizacao.Add(novo);
            db.SaveChanges();

            return Ok();

        }
        catch (Exception e)
        {
            return BadRequest("" + e.Message);
        }
    }


    private readonly IAuthService authService;
    private readonly AppDbContext db = new();
}