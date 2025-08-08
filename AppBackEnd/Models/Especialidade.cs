using System.ComponentModel.DataAnnotations;

namespace AppBackEnd.Models
{
    public class Especialidade
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Descricao { get; set; }
        
        public bool Ativa { get; set; } = true;
    }
}
