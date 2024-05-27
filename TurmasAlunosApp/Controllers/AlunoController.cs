using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurmasAlunosApp.Data;
using TurmasAlunosApp.Models;
using X.PagedList;
using System.Linq;
using System.Threading.Tasks;

namespace TurmasAlunosApp.Controllers
{
    public class AlunoController : Controller
    {
        private readonly AppDbContext _context;

        public AlunoController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page, int? pageSize)
        {
            int pageNumber = page ?? 1;
            int pageSizeValue = pageSize ?? 10;

            var alunos = _context.Alunos.Include(a => a.Turma)
                                        .OrderBy(a => a.Nome)
                                        .ToPagedList(pageNumber, pageSizeValue);

            ViewBag.PageSize = pageSizeValue;

            return View(alunos);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Turmas = await _context.Turmas.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Turmas = await _context.Turmas.ToListAsync();
            return View(aluno);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            ViewBag.Turmas = await _context.Turmas.ToListAsync();
            return View(aluno);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Turmas = await _context.Turmas.ToListAsync();
            return View(aluno);
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
    }
}
