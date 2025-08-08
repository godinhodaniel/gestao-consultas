using System.ComponentModel.DataAnnotations;

namespace AppBackEnd.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(14)]
        public string CPF { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string? Telefone { get; set; }
        
        [MaxLength(100)]
        public string? Email { get; set; }
        
        public DateTime? DataNascimento { get; set; }
        
        [MaxLength(200)]
        public string? Endereco { get; set; }
        
        public bool Ativo { get; set; } = true;
        
        // Navigation property
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    }
}
