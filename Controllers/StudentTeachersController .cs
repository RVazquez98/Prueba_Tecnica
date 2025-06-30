using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica_Italika.DTOs;
using Prueba_Tecnica_Italika.Models;
using Prueba_Tecnica_Italika.Services;
using System.Data;
using System.Threading.Tasks;

namespace Prueba_Tecnica_Italika.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentTeachersController : ControllerBase
    {
        private readonly DatabaseService _dbService;

        public StudentTeachersController(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignStudentToTeacher([FromBody] StudentTeacherHelper dto)
        {
            await _dbService.ExecuteNonQueryAsync("AssignStudentToTeacher", parameters =>
            {
                parameters.AddWithValue("@StudentsID", dto.StudentId);
                parameters.AddWithValue("@TeachersID", dto.TeacherId);
            });

            return Ok();
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveStudentFromTeacher([FromBody] StudentTeacherHelper dto)
        {
            await _dbService.ExecuteNonQueryAsync("RemoveStudentFromTeacher", parameters =>
            {
                parameters.AddWithValue("@StudentsID", dto.StudentId);
                parameters.AddWithValue("@TeachersID", dto.TeacherId);
            });

            return Ok();
        }

        [HttpGet("by-teacher/{teacherId}")]
        public async Task<IActionResult> GetStudentsByTeacher(int teacherId)
        {
            var table = await _dbService.ExecuteQueryAsync("GetStudentsByTeacher", parameters =>
            {
                parameters.AddWithValue("@TeachersID", teacherId);
            });

            var list = new List<StudentWithSchoolDto>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new StudentWithSchoolDto
                {
                    StudentId = (int)row["StudentId"],
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    IdentificationNumber = row["IdentificationNumber"].ToString(),
                    SchoolId = (int)row["SchoolId"],
                    SchoolName = row["SchoolName"].ToString()
                });
            }
            return Ok(list);
        }

    }
}
