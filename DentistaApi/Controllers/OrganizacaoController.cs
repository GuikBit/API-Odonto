﻿using DentistaApi.Data;
using DentistaApi.Models;
using Google.Protobuf.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentistaApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizacaoController : ControllerBase
    {
        
        [HttpGet]
        public ActionResult<List<Organizacao>> GetOrganizacoes(int id)
        {
            try
            {
                var organizacao = db.Organizacao.Include(x => x.Endereco).ToList();
                   

                if(organizacao == null)
                {
                    BadRequest("Nem uma organizacao salva no banco");
                }

                return Ok(organizacao);

            }catch (Exception ex)
            {
                return BadRequest("Organizacao nao encontrada: "+ ex.Message);
            }
        }

        
        [HttpGet("{id}")]
        public ActionResult<Organizacao> GetOrganizacao(int id)
        {
            var organizacao = db.Organizacao
                .Include(x => x.Funcionarios)
                .Include(x => x.Cargos)
                .Where(x => x.Id == id);

            if (organizacao == null)
            {
                BadRequest("Organizacao nao encontrada");
            }

            return Ok(organizacao);
        }

        
        [HttpPost]
        public ActionResult PostOrganizacao([FromBody] Organizacao novo)
        {
            try
            {
                if(novo == null)
                {
                    return BadRequest("Nem uma informacao foi passada");
                }

                db.Organizacao.Add(novo);
                db.SaveChanges();

                return Ok();

            }catch(Exception e)
            {
                return BadRequest(""+e.Message);
            }
        }

        
        [HttpPut("{id}")]
        public ActionResult PutOrganizacao(int id, [FromBody] Organizacao obj)
        {
            try
            {
                if(id == null && obj == null)
                {
                    BadRequest("Nem uma informacao foi passada");
                }
                var organizacao = db.Organizacao.FirstOrDefault(x => x.Id == id);
                
                if(organizacao != null && obj != null)
                {
                    organizacao.Telefone1 = obj.Telefone1;
                    organizacao.Telefone2 = obj.Telefone2;
                    organizacao.Whastapp = obj.Whastapp;
                    organizacao.Email = obj.Email;
                    organizacao.Endereco = obj.Endereco;
                }
                else
                {
                    BadRequest("Nao foi encontrado o registro dessa organizacao");
                }

                


                return Ok();
            }catch(Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("funcionarios")]
        public ActionResult<List<Funcionario>> GetFuncionarios( int idOrg)
        {
            try
            {
                var listFunc = db.Funcionarios.Where( f => f.OrganizacaoId == idOrg).Include(x => x.cargo).ToList();

                if( listFunc == null)
                {
                    return NotFound();
                }

                return Ok(listFunc);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [HttpGet("funcionario/{id}")]
        public ActionResult<Funcionario> GetByIdFuncionario([FromBody] int idOrg)
        {
            try
            {
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("funcionario")]
        public ActionResult<Funcionario> PostFuncionario([FromBody] Funcionario func)
        {
            try
            {
                Funcionario novo = new Funcionario
                {
                    Nome = func.Nome,
                    Email = func.Email,
                    Login = func.Login,
                    Senha = func.Senha,
                    Telefone = func.Telefone,
                    Cpf = func.Cpf,
                    DataNascimento = func.DataNascimento,
                    DataCadastro = DateTime.Now,
                    Role = func.Role,
                    NivelAcesso = func.NivelAcesso,

                    RG = func.RG,
                    RgUF = func.RgUF,
                    OrgEmissor = func.OrgEmissor,
                    PisPasep = func.PisPasep,
                    CTPSN = func.CTPSN,
                    CTPSSerie = func.CTPSSerie,
                    CTPSUF = func.CTPSUF,
                    DataAdmissao = func.DataAdmissao,
                    RegistroN = func.RegistroN
                };
                novo.SetSenhaHash();

                Endereco endNovo = func.Endereco;
                db.Endereco.Add(endNovo);
                db.SaveChanges();

                novo.Endereco = endNovo;
                novo.IdEndereco = endNovo.Id;

                var cargoExistente = db.Cargos.Find(func.cargo.Id);
                if (cargoExistente == null)
                {
                    return BadRequest("Cargo não encontrado.");
                }

                novo.cargo = cargoExistente;
                novo.IdCargo = cargoExistente.Id;

                var orgExiste = db.Organizacao.FirstOrDefault(x => x.Id == func.OrganizacaoId);
                if(orgExiste == null)
                {
                    return BadRequest("Empresa não encontrada.");
                }
                orgExiste.Funcionarios = null;

                novo.OrganizacaoId = orgExiste.Id;
                novo.IdOrganizacao = orgExiste;

                db.Funcionarios.Add(novo);
                db.SaveChanges();

                return Ok(novo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("funcionario/login")]
        public ActionResult GetLoginFuncionario(string login)
        {
            try
            {
                var isLogin = db.Funcionarios.FirstOrDefault(x => x.Login == login);

                if (isLogin != null)
                {
                    return BadRequest(false);
                }
                else
                {
                    return Ok(true);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro no servidor", details = ex.Message });
            }
        }



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var org = db.Organizacao.FirstOrDefault(x => x.Id == id);

                    if (org != null)
                    {
                        org.Ativo = false;
                        db.Organizacao.Update(org);
                        db.SaveChanges();

                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Organizacao nao foi encontrada");
                    }
                    
                    
                }
                else
                {
                    return BadRequest("Organizacao nao foi encontrada");
                }   
            }catch(Exception ex)
            {
                return BadRequest("Organizacao nao foi encontrada");
            }
            
        }

        private readonly AppDbContext db = new();
    }
}
