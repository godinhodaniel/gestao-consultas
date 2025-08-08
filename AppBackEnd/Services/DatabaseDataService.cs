using AppBackEnd.Data;
using AppBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace AppBackEnd.Services
{
    public class DatabaseDataService : IDataService
    {
        private readonly ApplicationDbContext _context;

        public DatabaseDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Usuários
        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await _context.Usuarios.Where(u => u.Ativo).ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id && u.Ativo);
        }

        public async Task<Usuario?> GetUsuarioByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Ativo);
        }

        public async Task<Usuario> AddUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        // Especialidades
        public async Task<List<Especialidade>> GetEspecialidadesAsync()
        {
            return await _context.Especialidades.Where(e => e.Ativa).ToListAsync();
        }

        public async Task<Especialidade?> GetEspecialidadeByIdAsync(int id)
        {
            return await _context.Especialidades.FirstOrDefaultAsync(e => e.Id == id && e.Ativa);
        }

        // Médicos
        public async Task<List<Medico>> GetMedicosAsync()
        {
            return await _context.Medicos
                .Include(m => m.Especialidade)
                .Where(m => m.Ativo)
                .ToListAsync();
        }

        public async Task<Medico?> GetMedicoByIdAsync(int id)
        {
            return await _context.Medicos
                .Include(m => m.Especialidade)
                .FirstOrDefaultAsync(m => m.Id == id && m.Ativo);
        }

        public async Task<Medico> AddMedicoAsync(Medico medico)
        {
            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();
            return medico;
        }

        // Pacientes
        public async Task<List<Paciente>> GetPacientesAsync()
        {
            return await _context.Pacientes.Where(p => p.Ativo).ToListAsync();
        }

        public async Task<Paciente?> GetPacienteByIdAsync(int id)
        {
            return await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
        }

        public async Task<Paciente> AddPacienteAsync(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        // Consultas
        public async Task<List<Consulta>> GetConsultasAsync()
        {
            return await _context.Consultas
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .ToListAsync();
        }

        public async Task<Consulta?> GetConsultaByIdAsync(int id)
        {
            return await _context.Consultas
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Consulta> AddConsultaAsync(Consulta consulta)
        {
            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();
            return consulta;
        }
    }
}
