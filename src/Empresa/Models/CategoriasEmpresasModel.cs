using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empresa.Models
{
    [Table("CategoriasEmpresas")]
    public class CategoriasEmpresasModel
    {
        [ForeignKey("Categoria")]
        public int CategoriasId { get; set; }
        public CategoriaModel? Categoria { get; set; } = default!;

        [ForeignKey("Empresa")]
        public int EmpresasId { get; set; }
        public EmpresaModel? Empresa { get; set; } = default!;
    }
}
