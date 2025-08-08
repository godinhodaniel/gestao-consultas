using AppBackEnd.Models;
using AppBackEnd.Services;

namespace AppBackEnd.Data
{
    public class InMemoryDataService : IDataService
    {
        private readonly List<Usuario> _usuarios;
        private readonly List<Especialidade> _especialidades;
        private readonly List<Medico> _medicos;
        private readonly List<Paciente> _pacientes;
        private readonly List<Consulta> _consultas;

        public InMemoryDataService()
        {
            _usuarios = new List<Usuario>();
            _especialidades = new List<Especialidade>();
            _medicos = new List<Medico>();
            _pacientes = new List<Paciente>();
            _consultas = new List<Consulta>();

            // Dados iniciais
            InitializeData();
        }

        private void InitializeData()
        {
            // Especialidades
            _especialidades.AddRange(new[]
            {
                new Especialidade { Id = 1, Nome = "Clínico Geral", Descricao = "Medicina geral", Ativa = true },
                new Especialidade { Id = 2, Nome = "Cardiologia", Descricao = "Especialidade do coração", Ativa = true },
                new Especialidade { Id = 3, Nome = "Ortopedia", Descricao = "Especialidade dos ossos e articulações", Ativa = true },
                new Especialidade { Id = 4, Nome = "Pediatria", Descricao = "Especialidade infantil", Ativa = true },
                new Especialidade { Id = 5, Nome = "Ginecologia", Descricao = "Especialidade feminina", Ativa = true }
            });
        }

        // Usuários
        public async Task<List<Usuario>> GetUsuariosAsync() => await Task.FromResult(_usuarios.Where(u => u.Ativo).ToList());
        public async Task<Usuario?> GetUsuarioByIdAsync(int id) => await Task.FromResult(_usuarios.FirstOrDefault(u => u.Id == id && u.Ativo));
        public async Task<Usuario?> GetUsuarioByEmailAsync(string email) => await Task.FromResult(_usuarios.FirstOrDefault(u => u.Email == email && u.Ativo));
        public async Task<Usuario> AddUsuarioAsync(Usuario usuario)
        {
            usuario.Id = _usuarios.Count > 0 ? _usuarios.Max(u => u.Id) + 1 : 1;
            _usuarios.Add(usuario);
            return await Task.FromResult(usuario);
        }

        // Especialidades
        public async Task<List<Especialidade>> GetEspecialidadesAsync() => await Task.FromResult(_especialidades.Where(e => e.Ativa).ToList());
        public async Task<Especialidade?> GetEspecialidadeByIdAsync(int id) => await Task.FromResult(_especialidades.FirstOrDefault(e => e.Id == id && e.Ativa));

        // Médicos
        public async Task<List<Medico>> GetMedicosAsync() => await Task.FromResult(_medicos.Where(m => m.Ativo).ToList());
        public async Task<Medico?> GetMedicoByIdAsync(int id) => await Task.FromResult(_medicos.FirstOrDefault(m => m.Id == id && m.Ativo));
        public async Task<Medico> AddMedicoAsync(Medico medico)
        {
            medico.Id = _medicos.Count > 0 ? _medicos.Max(m => m.Id) + 1 : 1;
            _medicos.Add(medico);
            return await Task.FromResult(medico);
        }

        // Pacientes
        public async Task<List<Paciente>> GetPacientesAsync() => await Task.FromResult(_pacientes.Where(p => p.Ativo).ToList());
        public async Task<Paciente?> GetPacienteByIdAsync(int id) => await Task.FromResult(_pacientes.FirstOrDefault(p => p.Id == id && p.Ativo));
        public async Task<Paciente> AddPacienteAsync(Paciente paciente)
        {
            paciente.Id = _pacientes.Count > 0 ? _pacientes.Max(p => p.Id) + 1 : 1;
            _pacientes.Add(paciente);
            return await Task.FromResult(paciente);
        }

        // Consultas
        public async Task<List<Consulta>> GetConsultasAsync() => await Task.FromResult(_consultas.ToList());
        public async Task<Consulta?> GetConsultaByIdAsync(int id) => await Task.FromResult(_consultas.FirstOrDefault(c => c.Id == id));
        public async Task<Consulta> AddConsultaAsync(Consulta consulta)
        {
            consulta.Id = _consultas.Count > 0 ? _consultas.Max(c => c.Id) + 1 : 1;
            _consultas.Add(consulta);
            return await Task.FromResult(consulta);
        }
    }
}
