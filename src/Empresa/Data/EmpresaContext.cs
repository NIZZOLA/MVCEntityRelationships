using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Empresa.Models;

namespace Empresa.Data
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext (DbContextOptions<EmpresaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriasEmpresasModel>().HasKey(a => new {a.EmpresasId , a.CategoriasId });
        }


        public DbSet<Empresa.Models.EmpresaModel> EmpresaModel { get; set; } = default!;

        public DbSet<Empresa.Models.CategoriaModel>? CategoriaModel { get; set; }

        public DbSet<Empresa.Models.FuncionarioModel>? FuncionarioModel { get; set; }

        public DbSet<Empresa.Models.CategoriasEmpresasModel>? CategoriaEmpresa { get; set; }
    }
}
