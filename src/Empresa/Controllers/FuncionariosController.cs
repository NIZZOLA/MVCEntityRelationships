using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Empresa.Data;
using Empresa.Models;

namespace Empresa.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly EmpresaContext _context;

        public FuncionariosController(EmpresaContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            var empresaContext = _context.FuncionarioModel.Include(f => f.Empresa);
            return View(await empresaContext.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FuncionarioModel == null)
            {
                return NotFound();
            }

            var funcionarioModel = await _context.FuncionarioModel
                .Include(f => f.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionarioModel == null)
            {
                return NotFound();
            }

            return View(funcionarioModel);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            ViewData["EmpresaId"] = new SelectList(_context.EmpresaModel, "Id", "NomeFantasia");
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,CPF,EmpresaId")] FuncionarioModel funcionarioModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionarioModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaId"] = new SelectList(_context.EmpresaModel, "Id", "NomeFantasia", funcionarioModel.EmpresaId);
            return View(funcionarioModel);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FuncionarioModel == null)
            {
                return NotFound();
            }

            var funcionarioModel = await _context.FuncionarioModel.FindAsync(id);
            if (funcionarioModel == null)
            {
                return NotFound();
            }
            ViewData["EmpresaId"] = new SelectList(_context.EmpresaModel, "Id", "Id", funcionarioModel.EmpresaId);
            return View(funcionarioModel);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,CPF,EmpresaId")] FuncionarioModel funcionarioModel)
        {
            if (id != funcionarioModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionarioModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioModelExists(funcionarioModel.Id))
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
            ViewData["EmpresaId"] = new SelectList(_context.EmpresaModel, "Id", "Id", funcionarioModel.EmpresaId);
            return View(funcionarioModel);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FuncionarioModel == null)
            {
                return NotFound();
            }

            var funcionarioModel = await _context.FuncionarioModel
                .Include(f => f.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionarioModel == null)
            {
                return NotFound();
            }

            return View(funcionarioModel);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FuncionarioModel == null)
            {
                return Problem("Entity set 'EmpresaContext.FuncionarioModel'  is null.");
            }
            var funcionarioModel = await _context.FuncionarioModel.FindAsync(id);
            if (funcionarioModel != null)
            {
                _context.FuncionarioModel.Remove(funcionarioModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioModelExists(int id)
        {
          return (_context.FuncionarioModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
