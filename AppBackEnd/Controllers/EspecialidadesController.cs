using Microsoft.AspNetCore.Mvc;
using AppBackEnd.Data;
using AppBackEnd.Models;
using AppBackEnd.Services;

namespace AppBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IDataService _dataService;

        public EspecialidadesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Lista todas as especialidades ativas
        /// </summary>
        /// <returns>Lista de especialidades</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Especialidade>>> GetEspecialidades()
        {
            var especialidades = await _dataService.GetEspecialidadesAsync();
            return Ok(especialidades);
        }

        /// <summary>
        /// Obtém uma especialidade específica por ID
        /// </summary>
        /// <param name="id">ID da especialidade</param>
        /// <returns>Especialidade encontrada</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Especialidade>> GetEspecialidade(int id)
        {
            var especialidade = await _dataService.GetEspecialidadeByIdAsync(id);
            
            if (especialidade == null)
            {
                return NotFound(new { message = "Especialidade não encontrada" });
            }

            return Ok(especialidade);
        }
    }
}
