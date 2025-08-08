using Microsoft.EntityFrameworkCore;
using AppBackEnd.Models;

namespace AppBackEnd.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações das entidades
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Senha).IsRequired();
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CRM).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Telefone).HasMaxLength(20);
                entity.HasIndex(e => e.CRM).IsUnique();
                
                entity.HasOne(e => e.Especialidade)
                    .WithMany()
                    .HasForeignKey(e => e.EspecialidadeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CPF).IsRequired().HasMaxLength(14);
                entity.Property(e => e.Telefone).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.HasIndex(e => e.CPF).IsUnique();
            });

            modelBuilder.Entity<Especialidade>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).HasMaxLength(500);
            });

            modelBuilder.Entity<Consulta>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DataHora).IsRequired();
                entity.Property(e => e.Observacoes).HasMaxLength(1000);
                entity.Property(e => e.Status).IsRequired();
                
                entity.HasOne(e => e.Medico)
                    .WithMany(m => m.Consultas)
                    .HasForeignKey(e => e.MedicoId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne(e => e.Paciente)
                    .WithMany(p => p.Consultas)
                    .HasForeignKey(e => e.PacienteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Dados iniciais
            modelBuilder.Entity<Especialidade>().HasData(
                new Especialidade { Id = 1, Nome = "Clínico Geral", Descricao = "Medicina geral", Ativa = true },
                new Especialidade { Id = 2, Nome = "Cardiologia", Descricao = "Especialidade do coração", Ativa = true },
                new Especialidade { Id = 3, Nome = "Ortopedia", Descricao = "Especialidade dos ossos e articulações", Ativa = true },
                new Especialidade { Id = 4, Nome = "Pediatria", Descricao = "Especialidade infantil", Ativa = true },
                new Especialidade { Id = 5, Nome = "Ginecologia", Descricao = "Especialidade feminina", Ativa = true }
            );
        }
    }
}
