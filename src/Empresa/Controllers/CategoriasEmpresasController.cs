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
    public class CategoriasEmpresasController : Controller
    {
        private readonly EmpresaContext _context;

        public CategoriasEmpresasController(EmpresaContext context)
        {
            _context = context;
        }

        // GET: CategoriasEmpresas
        public async Task<IActionResult> Index()
        {
            var empresaContext = _context.CategoriaEmpresa.Include(c => c.Categoria).Include(c => c.Empresa);
            return View(await empresaContext.ToListAsync());
        }

        // GET: CategoriasEmpresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoriaEmpresa == null)
            {
                return NotFound();
            }

            var categoriasEmpresasModel = await _context.CategoriaEmpresa
                .Include(c => c.Categoria)
                .Include(c => c.Empresa)
                .FirstOrDefaultAsync(m => m.EmpresasId == id);
            if (categoriasEmpresasModel == null)
            {
                return NotFound();
            }

            return View(categoriasEmpresasModel);
        }

        // GET: CategoriasEmpresas/Create
        public IActionResult Create()
        {
            ViewData["CategoriasId"] = new SelectList(_context.CategoriaModel, "Id", "Nome");
            ViewData["EmpresasId"] = new SelectList(_context.EmpresaModel, "Id", "NomeFantasia");
            return View();
        }

        // POST: CategoriasEmpresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriasId,EmpresasId")] CategoriasEmpresasModel categoriasEmpresasModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriasEmpresasModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriasId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", categoriasEmpresasModel.CategoriasId);
            ViewData["EmpresasId"] = new SelectList(_context.EmpresaModel, "Id", "NomeFantasia", categoriasEmpresasModel.EmpresasId);
            return View(categoriasEmpresasModel);
        }

        // GET: CategoriasEmpresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoriaEmpresa == null)
            {
                return NotFound();
            }

            var categoriasEmpresasModel = await _context.CategoriaEmpresa.FindAsync(id);
            if (categoriasEmpresasModel == null)
            {
                return NotFound();
            }
            ViewData["CategoriasId"] = new SelectList(_context.CategoriaModel, "Id", "Id", categoriasEmpresasModel.CategoriasId);
            ViewData["EmpresasId"] = new SelectList(_context.EmpresaModel, "Id", "Id", categoriasEmpresasModel.EmpresasId);
            return View(categoriasEmpresasModel);
        }

        // POST: CategoriasEmpresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriasId,EmpresasId")] CategoriasEmpresasModel categoriasEmpresasModel)
        {
            if (id != categoriasEmpresasModel.EmpresasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriasEmpresasModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriasEmpresasModelExists(categoriasEmpresasModel.EmpresasId))
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
            ViewData["CategoriasId"] = new SelectList(_context.CategoriaModel, "Id", "Id", categoriasEmpresasModel.CategoriasId);
            ViewData["EmpresasId"] = new SelectList(_context.EmpresaModel, "Id", "Id", categoriasEmpresasModel.EmpresasId);
            return View(categoriasEmpresasModel);
        }

        // GET: CategoriasEmpresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoriaEmpresa == null)
            {
                return NotFound();
            }

            var categoriasEmpresasModel = await _context.CategoriaEmpresa
                .Include(c => c.Categoria)
                .Include(c => c.Empresa)
                .FirstOrDefaultAsync(m => m.EmpresasId == id);
            if (categoriasEmpresasModel == null)
            {
                return NotFound();
            }

            return View(categoriasEmpresasModel);
        }

        // POST: CategoriasEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoriaEmpresa == null)
            {
                return Problem("Entity set 'EmpresaContext.CategoriaEmpresa'  is null.");
            }
            var categoriasEmpresasModel = await _context.CategoriaEmpresa.FindAsync(id);
            if (categoriasEmpresasModel != null)
            {
                _context.CategoriaEmpresa.Remove(categoriasEmpresasModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriasEmpresasModelExists(int id)
        {
          return (_context.CategoriaEmpresa?.Any(e => e.EmpresasId == id)).GetValueOrDefault();
        }
    }
}
