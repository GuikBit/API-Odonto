using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DentistaApi.Controllers
{
    public class FuncionarioController : ControllerBase
    {
        //private readonly FinanceiroService _service;
        private readonly AppDbContext db = new();

        //public FinanceiroController(FinanceiroService service)
        //{
        //    _service = service;
        //}

        //[HttpGet]
        //[Route("contasReceber")]
        //public ActionResult<IList<Funcionario>> GetFuncionarios()
        //{
        //    List<Funcionario> lista = new List<Funcionario>();

        //    var consultas = db.Consultas
        //        .Include(p => p.Dentista)
        //        .Include(p => p.Paciente)
        //        .Include(p => p.Pagamento)
        //        .Include(p => p.Pagamento.Parcelas)
        //        .Include(p => p.ConsultaEspecialidade)
        //        .ToList();

        //    foreach (var item in consultas)
        //    {
        //        item.Paciente.Consultas = null;
        //        item.Dentista.Consultas = null;
        //    }

        //    foreach (var item in consultas)
        //    {
        //        if (!item.Pagamento.Pago)
        //        {
        //            foreach (var parcela in item.Pagamento.Parcelas)
        //            {
        //                if (parcela.DataVencimento < DateTime.Now)
        //                {
        //                    ContasReceber i = new ContasReceber();

        //                    i.Parcela = parcela;
        //                    i.IdConsulta = item.Id;
        //                    i.DataConsulta = item.DataConsulta;
        //                    i.NomeDentista = item.Dentista.Nome;
        //                    i.NomePaciente = item.Paciente.Nome;
        //                    i.contatoPaciente = item.Paciente.Telefone;

        //                    lista.Add(i);
        //                }
        //            }

        //        }
        //    }
        //    return Ok(lista);

        //}

        //[HttpGet]
        //[Route("contasPagar")]
        //public ActionResult<IList<ContasReceber>> GetContasPagar()
        //{

        //    return Ok();
        //}
    }
}
