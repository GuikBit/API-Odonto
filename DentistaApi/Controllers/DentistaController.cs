using DentistaApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DentistaApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.IdentityModel.Logging;

namespace DentistaApi.Controllers;
[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class DentistaController : ControllerBase
{ 
    [HttpGet]
    public ActionResult<IList<Dentista>> Get(int idOrg)
    {

        var dentDentistas = db.Dentistas.Where(x => x.OrganizacaoId == idOrg).Include(x=> x.Especialidade).ToList();

        return Ok(dentDentistas);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Dentista> GetById(int id, int idOrg)
    {
        try{
            var dentDentista = db.Dentistas
            .Where(d => d.Id == id && d.OrganizacaoId == idOrg)
            .Include(x => x.Especialidade)
            .Include (x => x.IdOrganizacao)
            .FirstOrDefault();
            if (dentDentista == null)
            {
                return NotFound();
            }

            var consultas = db.Consultas.Where(x=> x.DentistaId == dentDentista.Id).Include(x=> x.Paciente).Include(x=> x.Pagamento).ToList();

            if( consultas.Count() > 0 )
            {
                foreach (var item in consultas)
                {
                    item.Paciente.Consultas = null;
                    item.Paciente.IdOrganizacao = null;
                    item.Dentista = null;

                }
            }
            
            dentDentista.IdOrganizacao.Dentistas = null;
            dentDentista.Consultas = consultas;


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
            CorDentista = obj.CorDentista,
            EspecialidadeId = obj.EspecialidadeId,
            OrganizacaoId = obj.OrganizacaoId,
            //IdOrganizacao = obj.IdOrganizacao,

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
    [HttpPost]
    [Route("/v1/dentista/especialidade")]
    public IActionResult PostEspecialidade(Especialidade espec)
    {
        try
        {
            if (espec != null)
            {
                db.Especialidades.Add(espec);
                db.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }catch(Exception ex)
        {
            return BadRequest();
        }


    }
    [HttpGet]
    [Route("/v1/dentista/especialidade")]
    public IActionResult GetEspecialidade(int idOrg)
    {
        try
        {
            var list = db.Especialidades.ToList();
            if (list == null)
            {                
                return NotFound();
            }
            if(list.Count == 0)
            {
                return Ok(list);
            }

            return Ok(list);
            
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("/v1/dentista/especialidade/{id}")]
    public IActionResult DeleteEspecialidade(int id)
    {
        try
        {
            var espec = db.Especialidades.FirstOrDefault(x => x.Id == id);
            if (espec == null)
            {
                return NotFound();
            }

            db.Especialidades.Remove(espec);
            db.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
          
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut]
    [Route("/v1/dentista/especialidade")]
    public IActionResult putEspecialidade(int id, Especialidade espec)
    {
        try
        {
            var especialidade = db.Especialidades.FirstOrDefault(x=> x.Id == id);

            if (especialidade == null)
            {
                return NotFound();
            }
            especialidade.Tipo = espec.Tipo;
            especialidade.Descricao = espec.Descricao;

            db.Especialidades.Update(especialidade);
            db.SaveChanges();

            return Ok();

        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    private readonly AppDbContext db = new();

    // private readonly UserManager<IdentityUser> userManager;
    // private readonly RoleManager<IdentityRole> roleManager;
    // private readonly SignInManager<IdentityUser> signInManager;
    // private readonly IAuthService authService;
}
