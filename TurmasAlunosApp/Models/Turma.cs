using System.Collections.Generic;

namespace TurmasAlunosApp.Models
{
    public class Turma
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public ICollection<Aluno>? Alunos { get; set; }
    }
}
