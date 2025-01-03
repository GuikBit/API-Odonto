using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentistaApi.Controllers;

[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class PontoEletronicoController : Controller
{
    [HttpGet]
    public ActionResult<IList<RegistroPonto>> GetPontoEletronico()
    {
        var pontos = db.RegistroPonto
            .Select(p => new
            {
                p.Id,
                p.OrganizacaoId,
                Funcionario = new
                {
                    p.Funcionario.Nome,
                    Cargo = new
                    {
                        p.Funcionario.cargo.Nome,
                        p.Funcionario.cargo.Departamento
                    }
                },
                p.DataCriacao,
                p.DataRegistro,
                p.HoraRegistro,
                p.Status,
                p.Registro,
                p.Controle,
                p.Observacao,

            })
            .ToList();



        return Ok(pontos);

    }

    [HttpPost]
    public ActionResult<RegistroPonto> PostPontoEletronico(RegistroPonto novo)
    {
        try
        {
            // Valida organização e funcionário
            if (novo.OrganizacaoId <= 0 || novo.FuncionarioId <= 0)
            {
                return BadRequest("Organização ou Funcionário inválido.");
            }

            // Verifica se a organização e o funcionário existem
            var org = db.Organizacao.FirstOrDefault(x => x.Id == novo.OrganizacaoId);
            var func = db.Funcionarios.FirstOrDefault(x => x.Id == novo.FuncionarioId);
            org.Funcionarios = null;
            

            if (org == null)
            {
                return NotFound("Organização não encontrada.");
            }

            if (func == null)
            {
                return NotFound("Funcionário não encontrado.");
            }

            //// Valida se já existe um ponto para o funcionário no dia corrente
            //if (ValidaPontoCorrente(org.Id, func.Id))
            //{
            //    return BadRequest("Já existe um ponto registrado para o colaborador no dia corrente.");
            //}
           

            // Configura os dados do novo registro de ponto
            if(ValidaPontoCorrente(org.Id, func.Id))
            {
                novo.Organizacao = org;
                novo.Funcionario = func;
                novo.DataCriacao = DateTime.Now;
                novo.DataRegistro = DateOnly.FromDateTime(DateTime.Now);
                novo.Status = Status.Pendente;
                novo.Registro = TipoRegistro.InicioExpediente;
                
                db.RegistroPonto.Add(novo);
                db.SaveChanges();

                return Ok(novo);
            }
            else
            {
                return BadRequest("Não foi possivel salvar o ponto");
            }
            
        }
        catch (Exception ex)
        {
            // Log do erro (caso tenha um sistema de logs)
            Console.WriteLine($"Erro ao registrar ponto: {ex.Message}");

            // Retorna erro genérico para o cliente
            return StatusCode(500, "Ocorreu um erro ao registrar o ponto. Tente novamente mais tarde.");
        }
    }


    private bool ValidaPontoCorrente(int orgID, int funID)
    {
        if (orgID > 0 && funID > 0)
        {
            var dataAtual = DateOnly.FromDateTime(DateTime.Now);

            var ponto = db.RegistroPonto
                .FirstOrDefault(x => x.OrganizacaoId == orgID
                                  && x.FuncionarioId == funID
                                  && x.DataRegistro == dataAtual);

            return ponto != null? true : false;
        }

        return false;
    }

    private readonly AppDbContext db = new();
}
