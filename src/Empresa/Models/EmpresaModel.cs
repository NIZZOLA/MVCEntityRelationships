using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empresa.Models
{
    [Table("Empresas")]
    public class EmpresaModel
    {   
        [Key]
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }

        public ICollection<CategoriasEmpresasModel>? Categorias { get; set; }
        public ICollection<FuncionarioModel>? Funcionarios { get; set; }
    }
}