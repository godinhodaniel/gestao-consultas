using Microsoft.AspNetCore.Mvc;
using AppBackEnd.Data;
using AppBackEnd.Models;

namespace AppBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadesController : ControllerBase
    {
        private readonly InMemoryDataService _dataService;

        public EspecialidadesController(InMemoryDataService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Lista todas as especialidades ativas
        /// </summary>
        /// <returns>Lista de especialidades</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Especialidade>> GetEspecialidades()
        {
            var especialidades = _dataService.GetEspecialidades();
            return Ok(especialidades);
        }

        /// <summary>
        /// Obtém uma especialidade específica por ID
        /// </summary>
        /// <param name="id">ID da especialidade</param>
        /// <returns>Especialidade encontrada</returns>
        [HttpGet("{id}")]
        public ActionResult<Especialidade> GetEspecialidade(int id)
        {
            var especialidade = _dataService.GetEspecialidadeById(id);
            
            if (especialidade == null)
            {
                return NotFound(new { message = "Especialidade não encontrada" });
            }

            return Ok(especialidade);
        }
    }
}
