using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models;

public class Organizacao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string CNPJ { get; set; }
    public required string Telefone1 { get; set;}
    public required string Telefone2 { get; set; }
    public required string Whastapp { get; set; }
    public required string Email {  get; set; }
    public bool Ativo {  get; set; } = true;
    public int EnderecoId { get; set; }
    public Endereco? Endereco { get; set; }

    public ICollection<Funcionario>? Funcionarios { get; set;}

    public ICollection<Dentista>? Dentistas { get; set; }

    public ICollection<Cargo>? Cargos { get; set; }

    public ICollection<Paciente>? Pacientes { get; set; }

    public ICollection<Consulta>? Consultas { get; set; }



}
