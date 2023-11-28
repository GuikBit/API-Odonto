using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace DentistaApi.Controllers;

[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class DashBordController : Controller
{
    [HttpGet]
    public IActionResult Dashbords()
    {
        try
        {
            var consultas = db.Consultas.ToList();
            var dentistas = db.Dentistas.ToList();
            var espec = db.Especialidades.ToList();

            Dashbords dashbords = new()
            {
                meses = RetornaMeses(),
                qtdPorMes = TotalConsultas(consultas),
                dentistas = Dentistas(dentistas),
                qtdPorDentista = TotalConsultasPorDentista(consultas, dentistas),
                espec = Especialidades(espec),
                qtdPorEspec = TotalPorEspec(consultas, espec)

            };


            return Ok(dashbords);
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }



    [HttpGet]
    [Route("{id}")]
    public IActionResult DashBordDentista(int id)
    {
        try
        {
            var consultas = db.Consultas.Where(c => c.Dentista.Id == id);
            var agora = DateTime.Now;

            var ultimos6Meses = Enumerable.Range(6, 12)
            .Select(offset => agora.AddMonths(+offset))
            .ToList();

            var totalConsultasPorMes = ultimos6Meses.Select(dataAtual => consultas.Count(c => c.DataConsulta.Month == dataAtual.Month && c.DataConsulta.Year == dataAtual.Year)).ToArray();

            Dashbords dashbord = new()
            {
                meses = RetornaMeses(),
                qtdPorMes = totalConsultasPorMes,
                dentistas = null,
                qtdPorDentista = null,
                espec = null,
                qtdPorEspec = null

            };

            return Ok(dashbord);
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }
    

    private readonly AppDbContext db = new();




    private int[] TotalConsultas(List<Consulta> consultas)
    {
        var agora = DateTime.Now;

        var ultimos6Meses = Enumerable.Range(0, 6)
        .Select(offset => agora.AddMonths(-offset))
        .ToList();

        var totalConsultasPorMes = ultimos6Meses
                 .Select(dataAtual => consultas.Count(c => c.DataConsulta.Month == dataAtual.Month && c.DataConsulta.Year == dataAtual.Year)).ToArray();

        return (int[])totalConsultasPorMes;

    }

    private static string[] RetornaMeses()
    {
        string[] nomesUltimos6Meses = new string[6];

        // Obtém o primeiro dia do mês atual
        DateTime primeiroDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        // Preenche o array com os nomes dos últimos 6 meses
        for (int i = 0; i < 6; i++)
        {
            DateTime mesAtual = primeiroDiaDoMes.AddMonths(-i);
            string nomeMes = mesAtual.ToString("MMMM");
            nomesUltimos6Meses[i] = nomeMes;
        }

        return nomesUltimos6Meses;
    

    //return meses;
}
    private string[] Dentistas(List<Dentista> dentistas)
    {
        ;
        var nomes = dentistas.Select(dentista => dentista.Nome).ToArray();

        return (string[])nomes;
    }
    private int[] TotalConsultasPorDentista(List<Consulta> consultas, List<Dentista> dentistas)
    {
        
         var total = dentistas.Select(dentista => consultas.Count(c => c.Dentista.Id == dentista.Id)).ToArray();

        return (int[])total;
    }
    private double[] TotalPorEspec(List<Consulta> consultas, List<Especialidade> especs)
    {

            var totalEspec = especs.Select(espec => consultas.Count(c => c.Dentista.Especialidade.Id == espec.Id)).ToArray();
            
            var totalConsultas = consultas.Count();

            if (totalConsultas > 0)
            {
                var total = totalEspec.Select(qtd => Math.Round((qtd / (double)totalConsultas) * 1, 2)).ToArray();
                return (double[])total;
            }
            else
            {
                var total = totalEspec.Select(qtd => 0.0).ToArray();
                return (double[])total;
            }

        }
 


    private string[] Especialidades(List<Especialidade> espec)    {
       
        var total = espec.Select(especialidade => especialidade.Tipo).ToArray();

        return (string[])total;
    }
}
