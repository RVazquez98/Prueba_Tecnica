using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica_Italika.DTOs;
using Prueba_Tecnica_Italika.Services;
using System.Threading.Tasks;

namespace Prueba_Tecnica_Italika.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolsController : ControllerBase
    {
        private readonly DatabaseService _dbService;

        public SchoolsController(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchool([FromBody] SchoolDto dto)
        {
            await _dbService.ExecuteNonQueryAsync("InsertSchool", parameters =>
            {
                parameters.AddWithValue("@Name", dto.Name);
                parameters.AddWithValue("@Description", dto.Description);
                parameters.AddWithValue("@Code", dto.Code);
            });

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchool(int id, [FromBody] SchoolDto dto)
        {
            await _dbService.ExecuteNonQueryAsync("UpdateSchool", parameters =>
            {
                parameters.AddWithValue("@SchoolsID", id);
                parameters.AddWithValue("@Name", dto.Name);
                parameters.AddWithValue("@Description", dto.Description);
                parameters.AddWithValue("@Code", dto.Code);
            });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            await _dbService.ExecuteNonQueryAsync("DeleteSchool", parameters =>
            {
                parameters.AddWithValue("@SchoolsID", id);
            });

            return Ok();
        }
    }
}
