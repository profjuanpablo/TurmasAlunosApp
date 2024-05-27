using Microsoft.EntityFrameworkCore;
using TurmasAlunosApp.Models;

namespace TurmasAlunosApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Turma>()
                .HasMany(t => t.Alunos)
                .WithOne(a => a.Turma)
                .HasForeignKey(a => a.TurmaId);
        }
    }
}
