using System.ComponentModel.DataAnnotations;

namespace AppBackEnd.Models
{
    public enum StatusConsulta
    {
        Agendada = 1,
        Confirmada = 2,
        EmAndamento = 3,
        Concluida = 4,
        Cancelada = 5,
        NaoCompareceu = 6
    }

    public class Consulta
    {
        public int Id { get; set; }
        
        [Required]
        public DateTime DataHora { get; set; }
        
        public int MedicoId { get; set; }
        public Medico Medico { get; set; } = null!;
        
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;
        
        [MaxLength(1000)]
        public string? Observacoes { get; set; }
        
        [Required]
        public StatusConsulta Status { get; set; } = StatusConsulta.Agendada;
        
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        
        public DateTime? DataAtualizacao { get; set; }
    }
}
