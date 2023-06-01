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
    public class EmpresasController : Controller
    {
        private readonly EmpresaContext _context;

        public EmpresasController(EmpresaContext context)
        {
            _context = context;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
              return _context.EmpresaModel != null ? 
                          View(await _context.EmpresaModel.ToListAsync()) :
                          Problem("Entity set 'EmpresaContext.EmpresaModel'  is null.");
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmpresaModel == null)
            {
                return NotFound();
            }

            var empresaModel = await _context.EmpresaModel.Include(a => a.Categorias)
                .ThenInclude(a => a.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresaModel == null)
            {
                return NotFound();
            }

            return View(empresaModel);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeFantasia,RazaoSocial,CNPJ,Email")] EmpresaModel empresaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empresaModel);
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmpresaModel == null)
            {
                return NotFound();
            }

            var empresaModel = await _context.EmpresaModel.FindAsync(id);
            if (empresaModel == null)
            {
                return NotFound();
            }
            return View(empresaModel);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFantasia,RazaoSocial,CNPJ,Email")] EmpresaModel empresaModel)
        {
            if (id != empresaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaModelExists(empresaModel.Id))
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
            return View(empresaModel);
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmpresaModel == null)
            {
                return NotFound();
            }

            var empresaModel = await _context.EmpresaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresaModel == null)
            {
                return NotFound();
            }

            return View(empresaModel);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmpresaModel == null)
            {
                return Problem("Entity set 'EmpresaContext.EmpresaModel'  is null.");
            }
            var empresaModel = await _context.EmpresaModel.FindAsync(id);
            if (empresaModel != null)
            {
                _context.EmpresaModel.Remove(empresaModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaModelExists(int id)
        {
          return (_context.EmpresaModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
