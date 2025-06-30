namespace Prueba_Tecnica_Italika.DTOs
{
    public class SchoolWithStudentsDto
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public List<StudentBasicDto> Students { get; set; }
    }
}
