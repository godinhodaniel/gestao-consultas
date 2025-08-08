using AppBackEnd.Models;

namespace AppBackEnd.Services
{
    public interface IDataService
    {
        // Usuários
        Task<List<Usuario>> GetUsuariosAsync();
        Task<Usuario?> GetUsuarioByIdAsync(int id);
        Task<Usuario?> GetUsuarioByEmailAsync(string email);
        Task<Usuario> AddUsuarioAsync(Usuario usuario);

        // Especialidades
        Task<List<Especialidade>> GetEspecialidadesAsync();
        Task<Especialidade?> GetEspecialidadeByIdAsync(int id);

        // Médicos
        Task<List<Medico>> GetMedicosAsync();
        Task<Medico?> GetMedicoByIdAsync(int id);
        Task<Medico> AddMedicoAsync(Medico medico);

        // Pacientes
        Task<List<Paciente>> GetPacientesAsync();
        Task<Paciente?> GetPacienteByIdAsync(int id);
        Task<Paciente> AddPacienteAsync(Paciente paciente);

        // Consultas
        Task<List<Consulta>> GetConsultasAsync();
        Task<Consulta?> GetConsultaByIdAsync(int id);
        Task<Consulta> AddConsultaAsync(Consulta consulta);
    }
}
