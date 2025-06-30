using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica_Italika.DTOs;
using Prueba_Tecnica_Italika.Services;
using System.Threading.Tasks;

namespace Prueba_Tecnica_Italika.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly DatabaseService _dbService;

        public TeachersController(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherDto dto)
        {
            await _dbService.ExecuteNonQueryAsync("InsertTeacher", parameters =>
            {
                parameters.AddWithValue("@FirstName", dto.FirstName);
                parameters.AddWithValue("@LastName", dto.LastName);
                parameters.AddWithValue("@IdentificationNumber", dto.IdentificationNumber);
                parameters.AddWithValue("@SchoolId", dto.SchoolId);
            });

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] TeacherDto dto)
        {
            await _dbService.ExecuteNonQueryAsync("UpdateTeacher", parameters =>
            {
                parameters.AddWithValue("@TeachersID", id);
                parameters.AddWithValue("@FirstName", dto.FirstName);
                parameters.AddWithValue("@LastName", dto.LastName);
                parameters.AddWithValue("@IdentificationNumber", dto.IdentificationNumber);
                parameters.AddWithValue("@SchoolId", dto.SchoolId);
            });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            await _dbService.ExecuteNonQueryAsync("DeleteTeacher", parameters =>
            {
                parameters.AddWithValue("@TeachersID", id);
            });

            return Ok();
        }
    }
}
