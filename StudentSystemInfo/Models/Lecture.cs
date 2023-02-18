namespace Students_Info_System.Entities
{
    public class Lecture
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public List<Departament> Departaments { get; set; }
        public List<Student> Students { get; set; }
        public Lecture(string subject)
        {
            Id = Guid.NewGuid();
            Subject = subject;
            Departaments = new List<Departament>();
            Students = new List<Student>();
        }
    }
}
