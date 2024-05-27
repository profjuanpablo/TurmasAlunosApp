using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurmasAlunosApp.Data;
using TurmasAlunosApp.Models;
using System.Threading.Tasks;
using System.Linq;

namespace TurmasAlunosApp.Controllers
{
    public class TurmaController : Controller
    {
        private readonly AppDbContext _context;

        public TurmaController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var turmas = await _context.Turmas.Include(t => t.Alunos).ToListAsync();
            return View(turmas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Turma turma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turma);
        }
    }
}
