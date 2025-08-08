using System.ComponentModel.DataAnnotations;

namespace AppBackEnd.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Senha { get; set; } = string.Empty;
        
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        
        public bool Ativo { get; set; } = true;
    }
}
