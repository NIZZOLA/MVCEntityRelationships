using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empresa.Models
{
    [Table("Categorias")]
    public class CategoriaModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<CategoriasEmpresasModel>? Empresas { get; set; }
    }
}
