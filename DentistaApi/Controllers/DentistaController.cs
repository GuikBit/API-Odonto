using DentistaApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DentistaApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DentistaApi.Controllers;
[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class DentistaController : ControllerBase
{ 
    [HttpGet]
    public ActionResult<IList<Dentista>> Get()
    {

        var dentDentistas = db.Dentistas.Include(d => d.Especialidade).ToList();

        return Ok(dentDentistas);
    }
    [HttpGet]
    [Route("/v1/dentistas/total")]
    public ActionResult<int> getTotalConsultas()
    {

        int total = db.Dentistas.Count();

        return Ok(total);
    }

    //[HttpGet]
    //[Route("{id}")]
    //public ActionResult<Dentista> GetById(int id)
    //{

    //    var dentDentista = db.Dentistas
    // .Where(d => d.Id == id)
    // .Include(d => d.Especialidade)
    // .Include(d => d.Consultas.OrderBy(c => c.DataConsulta))
    // .FirstOrDefault(); // Use FirstOrDefault() para obter apenas um dentista

    //    if (dentDentista == null)
    //    {
    //        return NotFound();
    //    }

    //    var jsonSerializerOptions = new JsonSerializerOptions
    //    {
    //        ReferenceHandler = ReferenceHandler.Preserve,
    //        MaxDepth = 32 // Defina o máximo permitido se necessário
    //    };

    //    var jsonDentista = JsonSerializer.Serialize(dentDentista, jsonSerializerOptions);

    //    return Ok(jsonDentista);
    //}
    [HttpGet]
    [Route("{id}")]
    public ActionResult<Dentista> GetById(int id)
    {
        try{
            var dentDentista = db.Dentistas
            .Where(d => d.Id == id)
            .Include(d => d.Especialidade)
            .FirstOrDefault();

            var consultasDent = db.Consultas
                .Where(d => d.Dentista.Id == id)
                .Include(p => p.Paciente)
                .Include(p => p.Pagamento)
                .OrderBy(c => c.DataConsulta).ToList();

            if(consultasDent != null)
            {
                foreach (var consulta in consultasDent)
                {
                    consulta.CorDentista = consulta.Dentista.CorDentista;
                    consulta.Dentista = null;
                    consulta.Paciente.Consultas = null;
                }
                dentDentista.Consultas = consultasDent;
            }
            return dentDentista == null ? NotFound() : Ok(dentDentista);
        }
        catch (Exception e) {
            return NotFound();
        }  
    }

    [HttpPost]
    public ActionResult<Dentista> Post(Dentista obj)
    {
        var espec = db.Especialidades.FirstOrDefault(x => x.Id == obj.Especialidade.Id);
        
        Dentista novo = new Dentista()
        {
            Nome = obj.Nome,
            Email = obj.Email,
            Login = obj.Login,
            Senha   = obj.Senha,
            Telefone = obj.Telefone,
            Cpf = obj.Cpf,
            DataNascimento = obj.DataNascimento,
            Especialidade = espec,

        };
        novo.SetSenhaHash();
        novo.SetRole();

        db.Dentistas.Add(novo);
        db.SaveChanges();


        return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);

    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Dentista obj)
    {
        if (id != obj.Id)
            return BadRequest();

        db.Dentistas.Update(obj);
        db.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (db.Dentistas == null)
            return NotFound();

        var obj = db.Dentistas.FirstOrDefault(x => x.Id == id);

        if (obj == null)
            return NotFound();

        db.Dentistas.Remove(obj);
        db.SaveChanges();

        return NoContent();
    }

    [HttpGet]
    [Route("/v1/dentista/validaLogin")]
    public bool validaLoginDentista(string login)
    {
        Dentista dentista = db.Dentistas.FirstOrDefault(p => p.Login == login);
        if (dentista == null)
        {
            return true;
        }
        else
        {
            return false;
        }


    }
    [HttpGet]
    [Route("/v1/dentista/validaCPF")]
    public bool validaCpfDentista(string cpf)
    {
        Dentista dentista = db.Dentistas.FirstOrDefault(p => p.Cpf == cpf);
        if (dentista != null)
        {
            return false;
        }
        else
        {
            return true;
        }


    }

    private readonly AppDbContext db = new();

    // private readonly UserManager<IdentityUser> userManager;
    // private readonly RoleManager<IdentityRole> roleManager;
    // private readonly SignInManager<IdentityUser> signInManager;
    // private readonly IAuthService authService;
}
