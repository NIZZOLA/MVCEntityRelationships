using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empresa.Models
{
    [Table("Funcionarios")]
    public class FuncionarioModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome{get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }

        [ForeignKey("Empresa")]
        public int? EmpresaId { get; set; }
        public EmpresaModel? Empresa { get; set; }
    }
}
