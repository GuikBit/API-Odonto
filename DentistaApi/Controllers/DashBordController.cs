using DentistaApi.Data;
using DentistaApi.Models;
using DentistaApi.Models.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static DentistaApi.Models.Dashbords;

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
            var dentistas = db.Dentistas.Where(d => d.Ativo).Include(d => d.Especialidade).Include(d => d.Consultas).ToList();
            dentistas.ForEach(d => { d.Consultas = null; });

            var procedimentos = db.ConsultaEspecialidades.ToList();
            
            var especialidades = db.Especialidades.ToList();
            var consultas = db.Consultas.Include(c => c.Pagamento).Include(c => c.ConsultaEspecialidade).ToList();
            var espec = db.Especialidades.ToList();

            var atual = DateTime.Now;
            //Dashbords dashbords = new()
            //{
            //    dentistasList = dentistas.ToList(),
            //    procedimentos = procedimentos.ToList(),
            //    especialidades = especialidades.ToList(),
            //    pacientes = RetornaPacientes(),
            //    meses = RetornaMeses(),
            //    qtdPorMes = TotalConsultas(consultas),
            //    dentistas = Dentistas(dentistas),
            //    qtdPorDentista = TotalConsultasPorDentista(consultas, dentistas),
            //    espec = Especialidades(espec),
            //    qtdPorEspec = TotalPorEspec(consultas, espec)

            //};

            Dashbords dashbords = new Dashbords();
            dashbords.Meses = RetornaMeses();
            dashbords.QtdMes = TotalConsultas(consultas, atual);
            dashbords.QtdPacienteMes = TotalPacientesMes(atual);
            dashbords.QtdAtrasoMes = TotalAtrazoMes(consultas, atual);
            dashbords.QtdFaturamentoMes = TotalFaturadoMes(consultas, atual);
            dashbords.QtdProcedimentos = TotalProcedimentosMes(consultas, atual);
            dashbords.QtdConsultasDentistaMes = TotalConsultasPorDentista(consultas, dentistas);
            dashbords.QtdEspecialidade = TotalPorEspec(consultas, espec);

            dashbords.DentistasList = dentistas;
            dashbords.Procedimentos = procedimentos;
            dashbords.Especialidades = espec;

         


            return Ok(dashbords);
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    //private List<int> TotalProcedimentosMes(List<Consulta> consultas, DateTime atual)
    //{
    //    var espec = db.ConsultaEspecialidades.ToList();

    //    List<int> consultasPorEspecialidade = new List<int>();

    //    foreach (var especialidade in espec)
    //    {

    //        string primeiroTermo = especialidade.Tipo.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[0];

    //        int quantidadeConsultas = consultas.Count(c =>
    //            c.ConsultaEspecialidade.Tipo.StartsWith(primeiroTermo)
    //        );

    //        consultasPorEspecialidade.Add(quantidadeConsultas);
    //    }

    //    return consultasPorEspecialidade;
    //}

    private List<EspecialidadeConsultaDTO> TotalProcedimentosMes(List<Consulta> consultas, DateTime atual)
    {
        var espec = db.ConsultaEspecialidades.ToList();
        List<EspecialidadeConsultaDTO> consultasPorEspecialidade = new List<EspecialidadeConsultaDTO>();

        foreach (var especialidade in espec)
        {
            int quantidadeConsultas = consultas.Count(c =>
                c.ConsultaEspecialidade.Id == especialidade.Id ) ;

            consultasPorEspecialidade.Add(new EspecialidadeConsultaDTO
            {
                Nome = especialidade.Tipo,
                QuantidadeConsultas = quantidadeConsultas
            });
        }

        return consultasPorEspecialidade;
    }






    private List<double> TotalFaturadoMes(List<Consulta> consultas, DateTime atual)
    {
        List<double> faturadoMes = new List<double>();

        var ultimos6Meses = Enumerable.Range(0, 6)
            .Select(offset => atual.AddMonths(-offset))
            .ToList();

        foreach (var mes in ultimos6Meses)
        {
            double totalMes =  consultas
                .Where(consulta =>
                    consulta.DataHoraInicioAtendimento?.Month == mes.Month &&
                    consulta.DataHoraInicioAtendimento?.Year == mes.Year &&
                    consulta.Pagamento != null &&
                    consulta.Pagamento.Pago &&
                    consulta.Pagamento.ValorTotal != 0
                    )
                .Sum(consulta =>
                    consulta.Pagamento.ValorTotal - consulta.Pagamento.Desconto + consulta.Pagamento.Acrecimo);

            faturadoMes.Add(totalMes);
        }

        return faturadoMes;
    }



    private List<double> TotalAtrazoMes(List<Consulta> consultas, DateTime atual)
    {
        try
        {
            List<double> totalAtrazoMes = new List<double>();

            var ultimos6Meses = Enumerable.Range(0, 6)
                .Select(offset => atual.AddMonths(-offset))
                .ToList();
 
            foreach (var mes in ultimos6Meses)
            {
                TimeSpan atraso = new TimeSpan();
                int cont = 0;

                foreach (var consulta in consultas)
                {
                    if ((consulta.DataConsulta.Month == mes.Month && consulta.DataConsulta.Year == mes.Year) && consulta.DataHoraInicioAtendimento != null)
                    {
                        if (consulta.DataConsulta < consulta.DataHoraInicioAtendimento)
                        {
                            atraso += (TimeSpan)(consulta.DataHoraInicioAtendimento - consulta.DataConsulta);
                            cont++;
                        }

                    }
                }
                if (atraso != null && cont > 0)
                {
                    totalAtrazoMes.Add(atraso.TotalMinutes / cont);
                }
                else
                {
                    totalAtrazoMes.Add(0);
                }
                
            }

            return totalAtrazoMes;
        }
        catch (Exception ex)
        {
            return new List<double>();
        }

    }

    private List<int> TotalPacientesMes(DateTime atual)
    {
        try
        {
            List<int> totalPacientesMes = new List<int>();
            var pacientes = db.Pacientes.ToList();

            var ultimos6Meses = Enumerable.Range(0, 6)
                .Select(offset => atual.AddMonths(-offset))
                .ToList();

            var totalPacientesPorMes = ultimos6Meses
                .Take(6)
                .Select(dataAtual => pacientes.Count(p => p.DataCadastro.Month == dataAtual.Month && p.DataCadastro.Year == dataAtual.Year))
                .ToList();

            foreach(var item in totalPacientesPorMes)
            {
                totalPacientesMes.Add(item);
            }


            return totalPacientesMes;

        }
        catch (Exception ex) {

            return new List<int>();
        }
    }

    private List<int> TotalConsultas(List<Consulta> consultas, DateTime atual)
    {

        var ultimos5MesesEMesSeguinte = Enumerable.Range(0, 7) // 5 meses anteriores + 1 mês seguinte
            .Select(offset => atual.AddMonths(-offset + 1))
            .ToList();

        var totalConsultasPorMes = ultimos5MesesEMesSeguinte
            .Take(7) // Considera apenas os últimos 5 meses
            .Select(dataAtual => consultas.Count(c => c.DataConsulta.Month == dataAtual.Month && c.DataConsulta.Year == dataAtual.Year))
            .ToList();

        return totalConsultasPorMes;
    }



    private static List<string> RetornaMeses()
    {
        List<string> meses = new List<string>();
        DateTime primeiroDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
        for (int i = 0; i < 6; i++)
        {
            DateTime mesAtual = primeiroDiaDoMes.AddMonths(-i);
            string nomeMes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mesAtual.ToString("MMMM"));
            meses.Add(nomeMes);
        }

        return meses;
    }

    private string[] Dentistas(List<Dentista> dentistas)
    {
        ;
        var nomes = dentistas.Select(dentista => dentista.Nome).ToArray();

        return (string[])nomes;
    }

    //private int[] TotalConsultasPorDentista(List<Consulta> consultas, List<Dentista> dentistas)
    //{

    //     var total = dentistas.Select(dentista => consultas.Count(c => c.Dentista.Id == dentista.Id)).ToArray();

    //    return (int[])total;
    //}
    //private int[] TotalConsultasPorDentista(List<Consulta> consultas, List<Dentista> dentistas)
    //{
    //    // Obtém a data atual
    //    DateTime dataAtual = DateTime.Now;

    //    // Inicializa o vetor bidimensional para armazenar os totais mensais
    //    int[] resultados = new int[6];

    //    // Itera sobre os últimos 6 meses
    //    for (int i = 0; i < 6; i++)
    //    {
    //        DateTime dataReferencia = dataAtual.AddMonths(-i);

    //        // Filtra as consultas para o mês atual
    //        var consultasMensais = consultas
    //            .Where(c => c.DataConsulta.Month == dataReferencia.Month && c.DataConsulta.Year == dataReferencia.Year)
    //            .ToList();

    //        // Para cada dentista, calcula o total de consultas no mês
    //        for (int j = 0; j < dentistas.Count; j++)
    //        {
    //            Dentista dentista = dentistas[j];
    //            resultados[i] = consultasMensais.Count(c => c.Dentista.Id == dentista.Id);
    //        }
    //    }

    //    return resultados;
    //}
    private List<List<int>> TotalConsultasPorDentista(List<Consulta> consultas, List<Dentista> dentistas)
    {
        // Obtém a data atual
        DateTime dataAtual = DateTime.Now;

        // Inicializa a lista de listas para armazenar os totais mensais por dentista
        List<List<int>> resultados = new List<List<int>>();

        // Itera sobre os últimos 6 meses
        for (int i = 0; i < 6; i++)
        {
            DateTime dataReferencia = dataAtual.AddMonths(-i + 1);

            // Filtra as consultas para o mês atual
            var consultasMensais = consultas
                .Where(c => c.DataConsulta.Month == dataReferencia.Month && c.DataConsulta.Year == dataReferencia.Year)
                .ToList();

            // Inicializa a lista de resultados para o mês atual
            List<int> resultadosDoMes = new List<int>();

            // Para cada dentista, calcula o total de consultas no mês
            foreach (var dentista in dentistas)
            {
                int totalConsultas = consultasMensais.Count(c => c.Dentista.Id == dentista.Id);
                resultadosDoMes.Add(totalConsultas);
            }

            // Adiciona a lista de resultados do mês atual à lista de listas de resultados
            resultados.Add(resultadosDoMes);
        }

        return resultados;
    }



    private List<int> TotalPorEspec(List<Consulta> consultas, List<Especialidade> especs)
    {
        List<int> totalPorEspec = new List<int>();

        // Itera sobre todas as especialidades
        foreach (var espec in especs)
        {
            // Calcula o total de consultas para a especialidade atual
            int totalConsultasEspec = consultas.Count(c => c.Dentista.Especialidade.Id == espec.Id);

            // Adiciona o total de consultas ao resultado
            totalPorEspec.Add(totalConsultasEspec);
        }

        return totalPorEspec;
    }

    private string[] Especialidades(List<Especialidade> espec)    {
       
        var total = espec.Select(especialidade => especialidade.Tipo).ToArray();

        return (string[])total;
    }

    private readonly AppDbContext db = new();
}
