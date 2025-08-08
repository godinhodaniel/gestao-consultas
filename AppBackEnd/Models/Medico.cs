using System.ComponentModel.DataAnnotations;

namespace AppBackEnd.Models
{
    public class Medico
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(20)]
        public string CRM { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string? Telefone { get; set; }
        
        [MaxLength(100)]
        public string? Email { get; set; }
        
        public int EspecialidadeId { get; set; }
        public Especialidade Especialidade { get; set; } = null!;
        
        public bool Ativo { get; set; } = true;
        
        // Navigation property
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    }
}
