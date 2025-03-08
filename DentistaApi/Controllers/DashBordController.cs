using DentistaApi.Data;
using DentistaApi.Models;
using DentistaApi.Models.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Globalization;
using static DentistaApi.Models.Dashbords;

namespace DentistaApi.Controllers;

public class IFiltro
{
    public DateTime? DtInicio { get; set; }
    public DateTime? DtFim { get; set; }
    public int? DentistaId { get; set; }
    public int? ProcedimentosId { get; set; }
    public int? EspecialidadeId { get; set; }

}


[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class DashBordController : Controller
{
    [HttpPost]
    public IActionResult Dashbords([FromBody] IFiltro filtros)
    {
        try
        {
            var atual = DateTime.Now;

            // Subtrai 3 meses da data atual e define como o primeiro dia do mês
            var dtInicio = filtros.DtInicio ?? new DateTime(atual.AddMonths(-3).Year, atual.AddMonths(-3).Month, 1);

            // Define a data final como o último dia do mês atual
            var dtFim = filtros.DtFim ?? new DateTime(atual.Year, atual.Month, 1).AddMonths(1).AddSeconds(-1);


            if ( dtFim > atual || filtros.DtFim != null ) 
            {
                var consultas = db.Consultas
               .Include(c => c.Pagamento)
               .Include(c => c.ConsultaEspecialidade)
               .Where(c => c.DataConsulta >= dtInicio && c.DataConsulta <= dtFim)
               .AsQueryable();

                 var dentistas = db.Dentistas.Where(d => d.Ativo).Include(d => d.Consultas).AsQueryable();
                

                if (filtros.EspecialidadeId.HasValue)
                {
                    //consultas = consultas.Where(c => c.ConsultaEspecialidadeId == filtros.EspecialidadeId);                    
                    consultas = consultas.GroupJoin(
                                    dentistas,                             // Coleção de dentistas
                                    consulta => consulta.DentistaId,       // Chave na tabela consultas (DentistaId)
                                    dentista => dentista.Id,               // Chave na tabela dentistas (Id)
                                    (consulta, dentistaGroup) => new { consulta, dentistaGroup }) // Resultado do join
                                .SelectMany(
                                    temp => temp.dentistaGroup.DefaultIfEmpty(),  // Garantindo o "left join"
                                    (temp, dentista) => new
                                    {
                                        Consulta = temp.consulta,
                                        Dentista = dentista
                                    }) // Projeção do resultado
                                .Where(result =>
                                    result.Dentista == null ||  // Inclui as consultas sem dentista associado
                                    result.Dentista.EspecialidadeId == filtros.EspecialidadeId) // Filtro por especialidade
                                .Select(result => result.Consulta);

                    dentistas = dentistas.Where(d => d.EspecialidadeId == filtros.EspecialidadeId);
                }

                if (filtros.DentistaId.HasValue)
                {
                    consultas = consultas.Where(c => c.DentistaId == filtros.DentistaId);
                    dentistas = dentistas.Where(d => d.Id == filtros.DentistaId);
                }

                if (filtros.ProcedimentosId.HasValue )
                {
                    consultas = consultas.Where(c => c.ConsultaEspecialidadeId == filtros.ProcedimentosId);
                    
                }

                var ConsuRes = consultas.ToList();
                var DentRes = dentistas.ToList();

                DentRes.ForEach(d => { d.Consultas = null; });

                var procedimentos = db.ConsultaEspecialidades.ToList();
                var especialidades = db.Especialidades.ToList();

                var listDentista = db.Dentistas.Where(d => d.Ativo).Include(d => d.Consultas).ToList();
                listDentista.ForEach(d => { d.Consultas = null; });

                Dashbords dashbords = new Dashbords
                {
                    Meses = RetornaMeses(dtInicio, dtFim),
                    QtdMes = TotalConsultas(ConsuRes, dtInicio, dtFim),
                    QtdPacienteMes = TotalPacientesMes(dtInicio, dtFim),
                    QtdAtrasoMes = TotalAtrazoMes(ConsuRes, dtInicio, dtFim),
                    QtdFaturamentoMes = TotalFaturadoMes(ConsuRes, atual),
                    QtdProcedimentos = TotalProcedimentosMes(ConsuRes, atual),
                    QtdConsultasDentistaMes = TotalConsultasPorDentista(ConsuRes, DentRes, dtInicio, dtFim),
                    QtdEspecialidade = TotalPorEspec(ConsuRes, especialidades),

                    DentistasFiltrados = DentRes,
                    DentistasList = listDentista,
                    Procedimentos = procedimentos,
                    Especialidades = especialidades
                };
                return Ok(dashbords);
            }
            else
            {
                return BadRequest("Verifique as datas filtradas!");
            }

           


            
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
        //try
        //{
        //    var consultasQuery = db.Consultas
        //        .Include(c => c.Pagamento)
        //        .Include(c => c.ConsultaEspecialidade)
        //        .AsQueryable();

        //    // Aplicação dos filtros
        //    if (filtros.Dentista != null)
        //    {
        //        consultasQuery = consultasQuery.Where(c => c.Dentista.Id == filtros.Dentista.Id);
        //    }


        //    if (filtros.Procedimentos != null)
        //    {
        //        consultasQuery = consultasQuery.Where(c => c.ConsultaEspecialidade.Id == filtros.Procedimentos.Id);
        //    }

        //    if (filtros.Especialidade != null)
        //    {
        //        consultasQuery = consultasQuery.Where(c => c.Dentista.Especialidade.Id == filtros.Especialidade.Id);
        //    }

        //    if (filtros.DtInicio != DateTime.MinValue && filtros.DtFim != DateTime.MinValue)
        //    {
        //        consultasQuery = consultasQuery.Where(c => c.DataConsulta >= filtros.DtInicio && c.DataConsulta <= filtros.DtFim);
        //    }
        //    else
        //    {

        //    }

        //    var consultas = consultasQuery.ToList();

        //    var dentistas = db.Dentistas.Where(d => d.Ativo).Include(d => d.Especialidade).Include(d => d.Consultas).ToList();
        //    dentistas.ForEach(d => { d.Consultas = null; });

        //    var procedimentos = db.ConsultaEspecialidades.ToList();
        //    var especialidades = db.Especialidades.ToList();

        //    var atual = DateTime.Now;

        //    Dashbords dashbords = new Dashbords
        //    {
        //        Meses = RetornaMeses(),
        //        QtdMes = TotalConsultas(consultas, atual),
        //        QtdPacienteMes = TotalPacientesMes(atual),
        //        QtdAtrasoMes = TotalAtrazoMes(consultas, atual),
        //        QtdFaturamentoMes = TotalFaturadoMes(consultas, atual),
        //        QtdProcedimentos = TotalProcedimentosMes(consultas, atual),
        //        QtdConsultasDentistaMes = TotalConsultasPorDentista(consultas, dentistas),
        //        QtdEspecialidade = TotalPorEspec(consultas, especialidades),
        //        DentistasList = dentistas,
        //        Procedimentos = procedimentos,
        //        Especialidades = especialidades
        //    };

        //    return Ok(dashbords);
        //}
        //catch (Exception ex)
        //{
        //    return StatusCode(500, $"Erro interno: {ex.Message}");
        //}

    }

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



    private List<double> TotalAtrazoMes(List<Consulta> consultas, DateTime dtInicio, DateTime dtFim)
    {
        try
        {
            List<double> totalAtrazoMes = new List<double>();

            int totalMeses = ((dtFim.Year - dtInicio.Year) * 12) + dtFim.Month - dtInicio.Month + 1;

            var mesesNoIntervalo = Enumerable.Range(0, totalMeses)
                .Select(offset => dtInicio.AddMonths(offset))
                .ToList();

            foreach (var mes in mesesNoIntervalo)
            {
                TimeSpan atrasoTotal = new TimeSpan();
                int cont = 0;

                foreach (var consulta in consultas)
                {
                    if (consulta.DataConsulta.Month == mes.Month && consulta.DataConsulta.Year == mes.Year &&
                        consulta.DataHoraInicioAtendimento != null)
                    {
                        if (consulta.DataConsulta < consulta.DataHoraInicioAtendimento)
                        {
                            atrasoTotal += (TimeSpan)(consulta.DataHoraInicioAtendimento - consulta.DataConsulta);
                            cont++;
                        }
                    }
                }

                if (cont > 0)
                {
                    totalAtrazoMes.Add(atrasoTotal.TotalMinutes / cont);
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


    //private List<int> TotalPacientesMes(DateTime dtInicio, DateTime dtFim)
    //{
    //    try
    //    {
    //        List<int> totalPacientesMes = new List<int>();
    //        var pacientes = db.Pacientes.ToList();

    //        var ultimos6Meses = Enumerable.Range(0, 6)
    //            .Select(offset => atual.AddMonths(-offset))
    //            .ToList();

    //        var totalPacientesPorMes = ultimos6Meses
    //            .Take(6)
    //            .Select(dataAtual => pacientes.Count(p => p.DataCadastro.Month == dataAtual.Month && p.DataCadastro.Year == dataAtual.Year))
    //            .ToList();

    //        foreach(var item in totalPacientesPorMes)
    //        {
    //            totalPacientesMes.Add(item);
    //        }


    //        return totalPacientesMes;

    //    }
    //    catch (Exception ex) {

    //        return new List<int>();
    //    }
    //}

    private List<int> TotalPacientesMes(DateTime dtInicio, DateTime dtFim)
    {
        try
        {
            List<int> totalPacientesMes = new List<int>();
            var pacientes = db.Pacientes.ToList();

            int totalMeses = ((dtFim.Year - dtInicio.Year) * 12) + dtFim.Month - dtInicio.Month + 1;

            var mesesNoIntervalo = Enumerable.Range(0, totalMeses)
                .Select(offset => dtInicio.AddMonths(offset))
                .ToList();

            var totalPacientesPorMes = mesesNoIntervalo
                .Select(dataAtual => pacientes.Count(p => p.DataCadastro.Month == dataAtual.Month && p.DataCadastro.Year == dataAtual.Year))
                .ToList();

            totalPacientesMes.AddRange(totalPacientesPorMes);

            return totalPacientesMes;
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao calcular o total de pacientes por mês.", ex);
        }
    }


    private List<int> TotalConsultas(List<Consulta> consultas, DateTime dtInicio, DateTime dtFim)
    {
        if (dtInicio.Month == dtFim.Month && dtInicio.Year == dtFim.Year)
        {
            dtInicio = dtInicio.AddMonths(-1);
            dtFim = dtFim.AddMonths(1);
        }

        var mesesIntervalo = Enumerable.Range(0, ((dtFim.Year - dtInicio.Year) * 12) + dtFim.Month - dtInicio.Month + 1)
               .Select(offset => new DateTime(dtInicio.Year, dtInicio.Month, 1).AddMonths(offset))
               .ToList();

        var totalConsultasPorMes = mesesIntervalo
            .Select(mes => consultas.Count(c => c.DataConsulta.Month == mes.Month && c.DataConsulta.Year == mes.Year))
            .ToList();

        return totalConsultasPorMes;
    }



    //private static List<string> RetornaMeses(DateTime dtInicio, DateTime dtFim)
    //{
    //    List<string> meses = new List<string>();
    //    DateTime primeiroDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
    //    for (int i = 0; i < 6; i++)
    //    {
    //        DateTime mesAtual = primeiroDiaDoMes.AddMonths(-i);
    //        string nomeMes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mesAtual.ToString("MMMM"));
    //        meses.Add(nomeMes);
    //    }

    //    return meses;
    //}
    private static List<string> RetornaMeses(DateTime dtInicio, DateTime dtFim)
    {
        List<string> meses = new List<string>();

        // Verifica se o mês de início e fim são os mesmos
        if (dtInicio.Month == dtFim.Month && dtInicio.Year == dtFim.Year)
        {
            // Ajusta a data de início para o mês anterior
            dtInicio = dtInicio.AddMonths(-1);
            // Ajusta a data de fim para o mês posterior
            dtFim = dtFim.AddMonths(1);
        }

        DateTime inicio = new DateTime(dtInicio.Year, dtInicio.Month, 1);
        DateTime fim = new DateTime(dtFim.Year, dtFim.Month, DateTime.DaysInMonth(dtFim.Year, dtFim.Month));

        for (DateTime mesAtual = inicio; mesAtual <= fim; mesAtual = mesAtual.AddMonths(1))
        {
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

    private List<List<int>> TotalConsultasPorDentista(List<Consulta> consultas, List<Dentista> dentistas, DateTime dtInicio, DateTime dtFim)
    {
        List<List<int>> resultados = new List<List<int>>();

        int totalMeses = ((dtFim.Year - dtInicio.Year) * 12) + dtFim.Month - dtInicio.Month + 1;

        for (int i = 0; i < totalMeses; i++)
        {
            DateTime dataReferencia = dtInicio.AddMonths(i);

            var consultasMensais = consultas
                .Where(c => c.DataConsulta.Month == dataReferencia.Month && c.DataConsulta.Year == dataReferencia.Year)
                .ToList();

            List<int> resultadosDoMes = new List<int>();

            foreach (var dentista in dentistas)
            {
                int totalConsultas = consultasMensais.Count(c => c.Dentista.Id == dentista.Id);
                resultadosDoMes.Add(totalConsultas);
            }

            resultados.Add(resultadosDoMes);
        }


        return resultados;
    }



    private List<int> TotalPorEspec(List<Consulta> consultas, List<Especialidade> especs)
    {
        List<int> totalPorEspec = new List<int>();

        foreach (var espec in especs)
        {
            int totalConsultasEspec = consultas.Count(c => c.Dentista.Especialidade.Id == espec.Id);

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
