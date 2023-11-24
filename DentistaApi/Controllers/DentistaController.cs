using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using DentistaApi.Services;
using Microsoft.EntityFrameworkCore;

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
    [Route("{id}")]
    public ActionResult<Dentista> GetById(int id)
    {

        var dentDentista = db.Dentistas.FirstOrDefault(x => x.Id == id);

        return dentDentista == null ? NotFound() : Ok(dentDentista);
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
            dataNasc = obj.dataNasc,
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

    private readonly AppDbContext db = new();

    // private readonly UserManager<IdentityUser> userManager;
    // private readonly RoleManager<IdentityRole> roleManager;
    // private readonly SignInManager<IdentityUser> signInManager;
    // private readonly IAuthService authService;
}
