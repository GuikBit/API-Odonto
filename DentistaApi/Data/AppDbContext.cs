using DentistaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DentistaApi.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<Dentista> Dentistas { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Anamnese> Anamneses { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
        }
    }
}