using AppBackEnd.Models;

namespace AppBackEnd.Data
{
    public class InMemoryDataService
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

            // Usuário padrão para teste
            _usuarios.Add(new Usuario
            {
                Id = 1,
                Nome = "Administrador",
                Email = "admin@teste.com",
                Senha = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", // "123456" em SHA256
                DataCriacao = DateTime.UtcNow,
                Ativo = true
            });
        }

        // Usuários
        public List<Usuario> GetUsuarios() => _usuarios.Where(u => u.Ativo).ToList();
        public Usuario? GetUsuarioById(int id) => _usuarios.FirstOrDefault(u => u.Id == id && u.Ativo);
        public Usuario? GetUsuarioByEmail(string email) => _usuarios.FirstOrDefault(u => u.Email == email && u.Ativo);
        public void AddUsuario(Usuario usuario)
        {
            usuario.Id = _usuarios.Count > 0 ? _usuarios.Max(u => u.Id) + 1 : 1;
            _usuarios.Add(usuario);
        }

        // Especialidades
        public List<Especialidade> GetEspecialidades() => _especialidades.Where(e => e.Ativa).ToList();
        public Especialidade? GetEspecialidadeById(int id) => _especialidades.FirstOrDefault(e => e.Id == id && e.Ativa);

        // Médicos
        public List<Medico> GetMedicos() => _medicos.Where(m => m.Ativo).ToList();
        public Medico? GetMedicoById(int id) => _medicos.FirstOrDefault(m => m.Id == id && m.Ativo);
        public void AddMedico(Medico medico)
        {
            medico.Id = _medicos.Count > 0 ? _medicos.Max(m => m.Id) + 1 : 1;
            _medicos.Add(medico);
        }

        // Pacientes
        public List<Paciente> GetPacientes() => _pacientes.Where(p => p.Ativo).ToList();
        public Paciente? GetPacienteById(int id) => _pacientes.FirstOrDefault(p => p.Id == id && p.Ativo);
        public void AddPaciente(Paciente paciente)
        {
            paciente.Id = _pacientes.Count > 0 ? _pacientes.Max(p => p.Id) + 1 : 1;
            _pacientes.Add(paciente);
        }

        // Consultas
        public List<Consulta> GetConsultas() => _consultas.ToList();
        public Consulta? GetConsultaById(int id) => _consultas.FirstOrDefault(c => c.Id == id);
        public void AddConsulta(Consulta consulta)
        {
            consulta.Id = _consultas.Count > 0 ? _consultas.Max(c => c.Id) + 1 : 1;
            _consultas.Add(consulta);
        }
    }
}
