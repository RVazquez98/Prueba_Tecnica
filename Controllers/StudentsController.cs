using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica_Italika.DTOs;
using Prueba_Tecnica_Italika.Services;
using System.Data;
using System.Threading.Tasks;

namespace Prueba_Tecnica_Italika.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly DatabaseService _dbService;

        public StudentsController(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDto dto)
        {
            await _dbService.ExecuteNonQueryAsync("InsertStudent", parameters =>
            {
                parameters.AddWithValue("@FirstName", dto.FirstName);
                parameters.AddWithValue("@LastName", dto.LastName);
                parameters.AddWithValue("@IdentificationNumber", dto.IdentificationNumber);
                parameters.AddWithValue("@SchoolId", dto.SchoolId);
            });

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDto dto)
        {
            await _dbService.ExecuteNonQueryAsync("UpdateStudent", parameters =>
            {
                parameters.AddWithValue("@StudentsID", id);
                parameters.AddWithValue("@FirstName", dto.FirstName);
                parameters.AddWithValue("@LastName", dto.LastName);
                parameters.AddWithValue("@IdentificationNumber", dto.IdentificationNumber);
                parameters.AddWithValue("@SchoolId", dto.SchoolId);
            });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _dbService.ExecuteNonQueryAsync("DeleteStudent", parameters =>
            {
                parameters.AddWithValue("@StudentsID", id);
            });

            return Ok();
        }

        [HttpGet("{id}/schools-students")]
        public async Task<IActionResult> GetSchoolsAndStudents(int id)
        {
            var table = await _dbService.ExecuteQueryAsync("GetSchoolsAndStudentsByTeacher", parameters =>
            {
                parameters.AddWithValue("@TeachersID", id);
            });

            var dict = new Dictionary<int, SchoolWithStudentsDto>();
            foreach (DataRow row in table.Rows)
            {
                int schoolId = (int)row["SchoolId"];
                if (!dict.ContainsKey(schoolId))
                {
                    dict[schoolId] = new SchoolWithStudentsDto
                    {
                        SchoolId = schoolId,
                        SchoolName = row["SchoolName"].ToString(),
                        Students = new List<StudentBasicDto>()
                    };
                }
                dict[schoolId].Students.Add(new StudentBasicDto
                {
                    StudentId = (int)row["StudentId"],
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    IdentificationNumber = row["IdentificationNumber"].ToString()
                });
            }

            return Ok(dict.Values.ToList());
        }

    }
}
