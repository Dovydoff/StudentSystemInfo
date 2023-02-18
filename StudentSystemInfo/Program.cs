using Microsoft.EntityFrameworkCore;
using Students_Info_System.Entities;
using StudentSystemInfo.Data;

namespace StudentSystemInfo
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new StudentSystemContext())
            {
                var departmentId = context.Departaments.First().Id;
                var studentId = context.Students.First().Id;
                var lectureId = context.Lectures.First().Id;
                Console.WriteLine("Press any key to continue to Student System Info");              
                // Creates Testing Data and asks to press any key
                FirstDataCreated(context); 
                Console.ReadLine();
                // Main menu to show for user
                bool isAlive = true;
                while (isAlive)
                {
                Console.WriteLine("1. Add New Department");
                Console.WriteLine("2. Add student to an Existing Department");
                Console.WriteLine("3. Create a Lecture and add it to Existing Department");
                Console.WriteLine("4. Create a Student and add it to Existing Department and add Existing Lectures");
                Console.WriteLine("5. Move student to other Department and change his Lextures");
                Console.WriteLine("6. Print All Students from Department");
                Console.WriteLine("7. Print All Departments Lectures");
                Console.WriteLine("8. Print All Lectures by Student");
                Console.WriteLine("\n " + "-------------------END OF LIST-------------------" + "\n");             
                    // Users selection
                    var input = GetSelection();
                switch(input)
                    {
                    case 0: 
                            continue;
                        case 1:
                            Console.WriteLine("\n " + "-------------------START OF LIST-------------------" + "\n");
                            Console.WriteLine("Please enter new Department:");
                            var addNewDep = Convert.ToString(Console.ReadLine());
                            AddANewDepartment(context, addNewDep);
                            Console.WriteLine("New Department added named: " + addNewDep);
                            Console.WriteLine("\n " + "-------------------END OF LIST-------------------" + "\n");
                            break;
                        case 2:
                            Console.WriteLine("\n " + "-------------------START OF LIST-------------------" + "\n");
                            Console.WriteLine("Please enter new students Name :");
                            var addNewStudentName = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Please enter new students Surname :");
                            var addNewStudentSurname = Convert.ToString(Console.ReadLine());
                            AddNewStudentToAnExistingDepartment(context, addNewStudentName, addNewStudentSurname, departmentId);
                            Console.WriteLine("\n " + "-------------------END OF LIST-------------------" + "\n");
                            break;
                        case 3:
                            Console.WriteLine("\n " + "-------------------START OF LIST-------------------" + "\n");
                            Console.WriteLine("Please enter new Lectures Subject :");
                            var addNewSubjectFromUser = Convert.ToString(Console.ReadLine());
                            AddLectureToAnExistingDepartment(context, addNewSubjectFromUser, departmentId);
                            Console.WriteLine("\n " + "-------------------END OF LIST-------------------" + "\n");
                            break;
                        case 4:
                            Console.WriteLine("\n " + "-------------------START OF LIST-------------------" + "\n");
                            Console.WriteLine("Please enter new Students Name :");
                            var addNewStudentNameFromUser = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Please enter new Students Surname :");
                            var addNewStudentSurnameFromUser = Convert.ToString(Console.ReadLine());
                            AddStudentToAnExistingDepartmentAndExistingLecture(context, addNewStudentNameFromUser, addNewStudentSurnameFromUser);
                            Console.WriteLine("\n " + "-------------------END OF LIST-------------------" + "\n");
                            break;
                        case 5:
                            Console.WriteLine("\n " + "-------------------START OF LIST-------------------" + "\n");
              /*              Console.WriteLine("Enter students Id :");
                            var enterStudentsId = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Enter Departments Id :");
                            var enterDepartmentsId = Convert.ToString(Console.ReadLine());*/
                            ChangeDepartment(context, studentId, departmentId);
                            Console.WriteLine("\n " + "-------------------END OF LIST-------------------" + "\n");
                            break;
                       case 6:
                            Console.WriteLine("\n " + "-------------------START OF LIST-------------------" + "\n");
                            Console.WriteLine("All students from department");
                            ShowAllStudents(context, departmentId);
                            Console.WriteLine("\n " + "-------------------END OF LIST-------------------" + "\n");
                            break;
                    case 7:
                            Console.WriteLine("\n " + "-------------------START OF LIST-------------------" + "\n");
                            Console.WriteLine("All Departments Lectures");
                            ShowAllLectures(context, departmentId);
                            Console.WriteLine("\n " + "-------------------END OF LIST-------------------" + "\n");
                            break;
                    case 8:
                            Console.WriteLine("\n " + "-------------------START OF LIST-------------------" + "\n");
                            Console.WriteLine("All Lectures by Student");

                            ShowAllLecturesByStudent(context, studentId);
                            Console.WriteLine("\n " + "-------------------END OF LIST-------------------" + "\n");
                            break;
                    }
                }
                // Gets user input to choose specific menu option
                int GetSelection()
                {
                    bool isSuccess = Int32.TryParse(Console.ReadLine(), out int result);
                    if (isSuccess)
                    {
                        return result;
                    }
                    Console.WriteLine("Try enter number from the list");
                    return 0;
                }
            }
        }
        // This function creates First Test Data to Database
        public static void FirstDataCreated(StudentSystemContext context)
        {
            var mainDepartment = context.Departaments.FirstOrDefault();
            if(mainDepartment == null) {
                var firstDepartment = new Departament("Sociologijos");
                var firstLecture = new Lecture("Lietuviu kalba");
                var secondLecture = new Lecture("Anglu kalba");
                firstDepartment.Lectures.Add(firstLecture);
                firstDepartment.Lectures.Add(secondLecture);
                var sociologyStudent = new Student("Gabriele", "Ivanauske");
                var sociologyStudent2 = new Student("Dovydas", "Ivanauskas");
                var sociologyStudent3 = new Student("Markas", "Ivanauskas");
                var sociologyStudent4 = new Student("Tomas", "Tomejus");
                firstDepartment.Students.Add(sociologyStudent);
                firstDepartment.Students.Add(sociologyStudent2);
                firstDepartment.Students.Add(sociologyStudent3);
                firstDepartment.Students.Add(sociologyStudent4);
                context.Departaments.Add(firstDepartment);
                context.SaveChanges();
            }
        }
        // 1. Create Department and add students to it
        private static void AddANewDepartment(StudentSystemContext context, string title)
        {
            var addDepartment = new Departament(title);
            context.Departaments.Add(addDepartment);
            context.SaveChanges();
        }
        // 2.Add Students to an existing Department
        private static void AddNewStudentToAnExistingDepartment(StudentSystemContext context, string name, string surname, Guid departmenId)
        {
            var addNewStudent = new Student(name, surname);
            context.Students.Add(addNewStudent);
            var existingDepartment = context.Departaments.FirstOrDefault(x => x.Id == departmenId);
            if (existingDepartment == null)
            {
                Console.WriteLine("Not existing department");
                return;
            }
            existingDepartment.Students.Add(addNewStudent);
            context.SaveChanges();
        }
        // 3. Create Lecture and add it to existing department
        private static void AddLectureToAnExistingDepartment(StudentSystemContext context, string subject, Guid departmentId)
        {
            var addedLecture = new Lecture(subject);
            context.Lectures.Add(addedLecture);
            var existingDepartment = context.Departaments.FirstOrDefault(x => x.Id == departmentId);
            if (existingDepartment == null)
            {
                Console.WriteLine("This department was not found");
                return;
            }
            existingDepartment.Lectures.Add(addedLecture);
            context.SaveChanges();
        }
        // 4. Create a student add it to an existing Department and add existing Lectures.
        private static void AddStudentToAnExistingDepartmentAndExistingLecture(StudentSystemContext context, string name, string surname)
        {
            var addedStudent = new Student(name,surname);
            context.Students.Add(addedStudent);
            var existingDepartment = context.Departaments.FirstOrDefault(x => x.Name == name);
            if (existingDepartment == null)
            {
                Console.WriteLine("This department was not found");
                return;
            }
            existingDepartment.Students.Add(addedStudent);
            context.SaveChanges();
        }
        // 5. Move student to other department and change his lectures
        private static void ChangeDepartment(StudentSystemContext context, Guid studentId, Guid departmentToAdd)
        {
            var exchangeStudent = context.Students.FirstOrDefault(x => x.Id == studentId);
            if (exchangeStudent == null)
            {
                Console.WriteLine("This department was not found");
                return;
            }
            exchangeStudent.DepartmentId = departmentToAdd;
            context.SaveChanges();
        }
        // 6. Shows all students from department
        private static void ShowAllStudents(StudentSystemContext context, Guid departmentId) // Linked department students thru their Dep ID
        {
            var department = context.Departaments.Include("Students").FirstOrDefault(x => x.Id == departmentId);
            if(department == null)
            {
                Console.WriteLine("Not existing department");
                return;
            }
            var students = department.Students;
            foreach(var student in students)
            {
                Console.WriteLine("Student " + student.Name + " " + student.Surname);
            }
        }
        // 7. Shows all students from department
        private static void ShowAllLectures(StudentSystemContext context, Guid departmentId)
        {
            var department = context.Departaments.Include("Lectures").FirstOrDefault(x => x.Id == departmentId);
                {
                if(department == null)
                {
                    Console.WriteLine("Not existing department");
                    return;
                }
                var lectures = department.Lectures;
                foreach (var lecture in lectures)
                {
                    Console.WriteLine("Lecture " + lecture.Subject);
                }
            }
        }
        // 8. Show all lectures by student
        private static void ShowAllLecturesByStudent(StudentSystemContext context, Guid studentId)
        {
            var student = context.Students.Include("Lectures").FirstOrDefault(x => x.Id == studentId);
            if (student == null)
            {
                Console.WriteLine("We cannot find such a student");
                return;
            }
            var lectureOfStudents = student.Lectures;
            foreach (var lectureOfStudent in lectureOfStudents)
            {
                Console.WriteLine(value: "Student " + student.Name + " " + student.Surname + " has these lectures: ");
                foreach (var lecture in student.Lectures)
                {
                    Console.WriteLine(lecture.Subject);
                };
            }
        }
    }
}
