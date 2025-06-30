using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_Italika.Models;

namespace Prueba_Tecnica_Italika.Data
{
    public class MusicSchoolContext : DbContext
    {
        public MusicSchoolContext(DbContextOptions<MusicSchoolContext> options) : base(options) { }
        public DbSet<School> Schools { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
    }

}
