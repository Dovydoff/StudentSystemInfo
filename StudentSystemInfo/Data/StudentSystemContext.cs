using Microsoft.EntityFrameworkCore;
using Students_Info_System.Entities;

namespace StudentSystemInfo.Data
{
    public  class StudentSystemContext : DbContext
    {
        public DbSet<Departament> Departaments { get; set; } = null;
        public DbSet<Lecture> Lectures { get; set; } = null;
        public DbSet<Student> Students { get; set; } = null;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentsDB");
        }
    }
}
