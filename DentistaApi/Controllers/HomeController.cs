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


        if (UserLogado.Status == EReturnStatus.Success)
        {
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

    [HttpPost]
    [Route("InserirDadosTeste")]
    public IActionResult InserirDadosTeste()
    {
        try
        {
            //Administrador adm = new Administrador();

            //adm.Nome = "admin";
            //adm.Email = "admin.@gmail.com";
            //adm.Login = "admin";
            //adm.Senha = "123";
            //adm.SetSenhaHash();




            // Data atual
            DateTime dataAtual = DateTime.Now;

            // Gerar 50 registros de consultas
            for (int i = 0; i < 87 ; i++)
            {
                Random random = new Random();

                // Randomização de alguns campos
                int randDentista = random.Next(1, 4);  // ID de dentista aleatório
                int randPaciente = random.Next(1, 5);  // ID de paciente aleatório
                int randonDia = random.Next(1, 30); // Dia aleatório entre 1 e 28
                int randHora = random.Next(8, 18);   // Hora aleatória entre 8h e 18h
                int randMinuto = random.Next(0, 59); // Minuto aleatório
                int randEspec = random.Next(1, 5);
                // Cria uma nova consulta
                Consulta novaConsulta = new Consulta();

                // Buscar dentista e paciente aleatórios
                var dentista = db.Dentistas.First(x => x.Id == randDentista);
                var paciente = db.Pacientes.First(x => x.Id == randPaciente);
                var consEspec = db.ConsultaEspecialidades.First(x => x.Id == randEspec); // Procedimento aleatório

                novaConsulta.Paciente = paciente;
                novaConsulta.Dentista = dentista;
                novaConsulta.ConsultaEspecialidade = consEspec;

                // Data da consulta no mês atual com dia e hora aleatórios
                DateTime dataConsulta = new DateTime(dataAtual.Year, dataAtual.Month - 3, randonDia, randHora, randMinuto, 0);
                novaConsulta.DataConsulta = dataConsulta;

                // Definir tempo previsto de forma aleatória
                novaConsulta.TempoPrevisto = random.Next(1, 4); // Tempo previsto de 1 a 3 horas
                novaConsulta.setTempoPrevisto(novaConsulta.TempoPrevisto);
                novaConsulta.CorDentista = dentista.CorDentista;

                // Configuração de pagamento
                novaConsulta.Pagamento = new Pagamento();
                Parcela novaParcela = new Parcela
                {
                    ValorParcela = consEspec.ValorBase,  // Valor base do procedimento
                    DataVencimento = dataConsulta.AddDays(7)  // Vencimento após 7 dias da consulta
                };
                novaConsulta.Pagamento.Parcelas.Add(novaParcela);

                // Adicionar consulta ao banco de dados
                db.Consultas.Add(novaConsulta);
            }

            // Salvar todas as consultas de uma vez
            db.SaveChanges();

            return Ok("50 registros inseridos com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao inserir registros: {ex.Message}");
        }
    }

    //[HttpPost("Paciente")]
    //public ActionResult<Paciente> Post(Paciente obj)
    //{
    //    if (obj == null)
    //        return BadRequest();

    //    SalvaInfo(obj);

    //    return Ok();

    //}

    //private Paciente SalvaInfo(Paciente obj)
    //{


    //    Paciente novo = new Paciente();

    //    novo.Nome = obj.Nome;
    //    novo.Email = obj.Email;
    //    novo.Login = obj.Login;
    //    novo.Senha = obj.Senha;
    //    novo.SetSenhaHash();
    //    novo.Telefone = obj.Telefone;
    //    novo.Cpf = obj.Cpf;
    //    novo.DataNascimento = obj.DataNascimento;
    //    novo.SetRole(Role.PACIENTE);
    //    novo.Endereco = obj.Endereco;
    //    novo.Responsavel = obj.Responsavel;
    //    novo.Anamnese = obj.Anamnese;

    //    db.Pacientes.Add(novo);
    //    db.SaveChanges();

    //    return novo;


    //}

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