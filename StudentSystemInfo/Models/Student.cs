using System.ComponentModel.DataAnnotations.Schema;

namespace Students_Info_System.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Lecture> Lectures { get; set; }

        [ForeignKey("Departament")]
        public Guid DepartmentId { get; set; }
        public Departament Departament { get; set; }
        public Student(string name, string surname)
        {
            Id= Guid.NewGuid();
            Name = name;
            Surname = surname;
            Lectures = new List<Lecture>();
        }
    }
}
