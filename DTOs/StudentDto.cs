namespace Prueba_Tecnica_Italika.DTOs
{
    public class StudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
        public int SchoolId { get; set; }
    }
    public class StudentBasicDto
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
    }
}
