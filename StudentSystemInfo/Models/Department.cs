using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students_Info_System.Entities
{
    public class Departament
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public List<Lecture> Lectures { get; set; }
        public Departament(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Students = new List<Student>();
            Lectures = new List<Lecture>();
        }
    }
}
