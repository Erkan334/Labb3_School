using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using Labb3_School.Data;
using Labb3_School.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3_School
{
    internal class Methods
    {
        public static void ViewStudents()
        {
            using var context = new SchoolContext();

            var students = context.Students.ToList();

            Console.WriteLine("How would you like to sort the students?");
            Console.WriteLine("1. By first name");
            Console.WriteLine("2. By last name");

            if (int.TryParse(Console.ReadLine(), out int orderSort))

           
            Console.WriteLine("How would you like to sort the names?");
            Console.WriteLine("1. Ascending order");
            Console.WriteLine("2. Descending order?");

            if (int.TryParse(Console.ReadLine(), out int nameSort))

                //Switch-expression for different types of sorting
                students = (orderSort, nameSort) switch
                {
                    (1, 1) => students.OrderBy(s => s.FirstName).ToList(), //Sort by first name, ascending order
                    (1, 2) => students.OrderByDescending(s => s.FirstName).ToList(), //Sort by first name, descending order
                    (2, 1) => students.OrderBy(s => s.LastName).ToList(), //Sort by last name, ascending order
                    (2, 2) => students.OrderByDescending(s => s.LastName).ToList(), //Sort by last name, descending order
                    _ => students

                };

            Console.WriteLine("Students: \n");

            foreach (var student in students)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}");
            }

            Console.WriteLine("\nPress anywhere to to back");
            Console.ReadKey();
        }


        public static void ViewClasses()
        {
            while (true)
            {
                Console.Clear();
                using var context = new SchoolContext();

                var classes = context.Classes.ToList();

                Console.WriteLine("List of all classes: \n");

                //Shows classes
                foreach (var class1 in classes)
                {
                    Console.WriteLine($"{class1.Id} {class1.Name}");
                }


                Console.WriteLine($"What class would you like to view? (1-{classes.Count})");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int classId) && classes.Any(c => c.Id == classId))
                {
                    var chosenClass = context.Students
                                      .Include(c => c.Class)
                                      .Where(c => c.Class.Id == classId)
                                      .ToList();
                    
                    //Shows the students in chosen class
                    foreach (var students in chosenClass)
                    {
                        Console.WriteLine($"{students.FirstName} {students.LastName}");
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Press anywhere to try again");
                    Console.ReadKey();
                }
            }
            Console.WriteLine("\nPress anywhere to to back");
            Console.ReadKey();

        }


        public static void ViewStaff()
        {
            while (true)
            { 
            
                using var context = new SchoolContext();
                
                Console.Clear();
                Console.WriteLine("Choose an alternative");

                var staffs = context.Staff.ToList();
                
                Console.WriteLine("1. All staff");
                Console.WriteLine("2. Staff by role");
                Console.WriteLine("3. Back");


                string input = Console.ReadLine();


                switch (input)
                {
                    //View All Staff
                    case "1":
                        Console.Clear();
                        Console.WriteLine("All staff:");
                        foreach (var staff in staffs)
                        {
                            Console.WriteLine($"Name: {staff.FirstName} {staff.LastName}\nRole: {staff.Role}\n");
                        }
                        Console.WriteLine("Press anywhere to go back");
                        Console.ReadKey();
                        break;

                    //View Staff from Role
                    case "2":
                        Console.Clear();
                        Console.WriteLine($"Choose a role: ");

                        var roles = staffs
                                    .Select(s => s.Role)
                                    .Distinct()             //Ignores duplicates
                                    .ToList();


                        //Numbered role-list
                        for (int i = 0; i < roles.Count; i++)
                        {
                            Console.WriteLine($"{i+1}.{roles[i]}");
                        }

                        string roleChoice = Console.ReadLine();

                        if (int.TryParse(roleChoice, out int roleNumber) && roleNumber >= 1 && roleNumber <= roles.Count)
                        {
                            string chosenRole = roles[roleNumber - 1];

                            var filteretStaff = staffs.Where(s => s.Role == chosenRole).ToList();

                            foreach (var staff in filteretStaff)
                            {
                                Console.WriteLine($"Role: {staff.Role}\n{staff.FirstName} {staff.LastName}\nEmail:{staff.Email}\n{staff.PhoneNumber}\n");
                            }

                            Console.WriteLine("Press anywhere to go back");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Press anywhere to go back");
                            Console.ReadKey();
                        }
                            break;

                    //Exit
                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }

        }


        public static void AddStudent()
        {
            using var context = new SchoolContext();

            string firstName;
            string lastName;
            int personalNumber;
            int classId;

            Console.WriteLine("Add student:");

            //First Name
            while (true)
            {
                Console.WriteLine("Please enter the students first name:");
                firstName = Console.ReadLine();

                if (firstName.All(char.IsLetter))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid name, try again");
                }
            }

            //Last Name
            while (true)
            { 
            
                Console.WriteLine("Please enter the students last name:");
                lastName = Console.ReadLine();

                if (lastName.All(char.IsLetter))
                {
                    break;
                }
                else
                    {
                        Console.WriteLine("Invalid name, try again");
                    }
            }

            //Personal Number
            while (true)
            { 
                Console.WriteLine("Please enter the students personal number:");

                if (int.TryParse(Console.ReadLine(), out personalNumber))
                    {
                        break;
                    }
                else
                    {
                        Console.WriteLine("Invalid input, try again");
                    }
            }

            //ClassId
            while (true)
            { 
            
                Console.WriteLine("Please enter the students classId:");

                if (!int.TryParse(Console.ReadLine(), out classId))
                    {
                        Console.WriteLine("Invalid input, try again");
                    }
                else if (!context.Classes.Any(c => c.Id == classId))
                    {
                        Console.WriteLine("No class found with entered id");
                    }
                else 
                    {
                        break;
                    }
            }

            
            var newStudent = new Student()
            {
                FirstName = firstName,
                LastName = lastName,
                PersonalNumber = personalNumber,
                ClassId = classId
            };
            
            //Adds new student
            context.Students.Add(newStudent);
            context.SaveChanges();
            Console.WriteLine($"{newStudent.FirstName} {newStudent.LastName} has been added!");
            Console.WriteLine("Press anywhere to exit");
            Console.ReadKey();
        }


        public static void AddStaff()
        {
            using var context = new SchoolContext();

            var staffs = context.Staff.ToList();

            string firstName;
            string lastName;
            string role;
            int age;
            string email;
            string phoneNumber;

            Console.WriteLine("Add staff:");

            //First Name
            while (true)
            {
                Console.WriteLine("Please enter the staffs first name:");
                firstName = Console.ReadLine();

                if (firstName.All(char.IsLetter))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid name, try again");
                }
            }

            //Last Name
            while (true)
            {

                Console.WriteLine("Please enter the staffs last name:");
                lastName = Console.ReadLine();

                if (lastName.All(char.IsLetter))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid name, try again");
                }
            }

            //Role
            while (true)
            {
                Console.WriteLine("Please enter the staffs role:");

                var roles = staffs
                                    .Select(s => s.Role)
                                    .Distinct()             //Ignores duplicates
                                    .ToList();


                //Numbered role-list
                for (int i = 0; i < roles.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.{roles[i]}");
                }



                if (int.TryParse(Console.ReadLine(), out int roleNumber) && roleNumber >= 1 && roleNumber <= roles.Count)
                {
                    role = roles[roleNumber - 1];

                    break;
                }
                else
                {
                    Console.WriteLine("Invalid role, try again");
                }

            }

            //Age
            while (true)
            {

                Console.WriteLine("Please enter the staffs age (or press enter to skip):");

                string inputAge = Console.ReadLine();
                if (!int.TryParse(inputAge, out age))
                {
                    break;
                }
                else if (string.IsNullOrEmpty(inputAge))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid age, try again");
                }

            }

            //Email
            while (true)
            {

                Console.WriteLine("Please enter the staffs email (or press enter to skip):");

                email = Console.ReadLine()?.Trim();
                break;

            }

            while (true)
            {
                Console.WriteLine("Please enter the staffs phonenumber: ");

                phoneNumber = Console.ReadLine();

                if (phoneNumber.All(char.IsDigit) && phoneNumber.Length <= 15)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid phonenumber, try again");
                }
            }

            var newStaff = new Staff()
            {
                FirstName = firstName,
                LastName = lastName,
                Role = role,
                Age = age,
                Email = email,
                PhoneNumber = phoneNumber
            };

            //Adds new staff
            context.Staff.Add(newStaff);
            context.SaveChanges();
            Console.WriteLine($"{newStaff.FirstName} {newStaff.LastName} has been added!");
            Console.WriteLine("Press anywhere to exit");
            Console.ReadKey();

        }


        public static void StaffAmount()
        {

            using var context = new SchoolContext();

            Console.WriteLine("Choose department:");
            Console.WriteLine("1. Teachers");
            Console.WriteLine("2. Management");
            Console.WriteLine("3. IT & Service");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid choice. Press anywhere to exit");
                Console.ReadKey();
                return;
            }

            //Switch expression for each department
            string departmentFilter = choice switch
            {
                1 => "Teachers",
                2 => "Management",
                3 => "IT and service",
                _ => "Invalid choice"
            };

            if (departmentFilter == "Invalid choice")
            {
                Console.WriteLine("Invalid choice. Press anywhere to exit");
                Console.ReadKey();
                return;
            }

            int departmentId = choice;

            var staff = context.Staff
                               .Where(s => s.DepartmentId == departmentId)
                               .ToList();

            staff.ForEach(s =>
                Console.WriteLine($"Id: {s.Id}, {s.FirstName} {s.LastName} - {s.Role}")
            );

            Console.WriteLine($"\nTotal staff: {staff.Count}");
            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey();

        }


        public static void StudentInformation()
        {
            using var context = new SchoolContext();

            var students = context.Students
                                  .Include(s => s.Class)
                                  .Include(s => s.Grades)
                                    .ThenInclude(g => g.Course)
                                  .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"\nId: {student.Id}, {student.FirstName} {student.LastName}\n{student.Class.Name}");

                foreach (var grade in student.Grades)
                {
                    Console.WriteLine($"Course: {grade.Course.Course1}, Grade: {grade.Grade1}\n");
                }
                Console.WriteLine("----------------------------------");
            }

            Console.ReadKey();
        }


        public static void ActiveCourses()
        {
            var context = new SchoolContext();

            //Converts DateOnly datatype in Course to todays date
            var activeCourses = context.Courses
                                       .Where(c => c.StartDate <= DateOnly.FromDateTime(DateTime.Now) && c.EndDate >= DateOnly.FromDateTime(DateTime.Now))
                                       .ToList();

            var completedCourses = context.Courses
                                          .Where(c => c.EndDate <= DateOnly.FromDateTime(DateTime.Now))
                                          .ToList();

            Console.WriteLine("Choose an alternative:");
            Console.WriteLine("1. Show active courses");
            Console.WriteLine("2. Show completed courses");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid choice. Press anywhere to exit");
                return;
            }

            switch (choice)
            {
                case 1:
                    //checks if activeCourses contains anything in the list
                    if (activeCourses.Any())
                    {
                        foreach (var course in activeCourses)
                        {
                            Console.WriteLine($"{course.Course1}\nStart-date: {course.StartDate}\nEnd-date: {course.EndDate}\n");

                        }

                        Console.WriteLine("Press anywhere to exit");
                        Console.ReadKey();
                        break;
                    }
                    else 
                    { 
                    Console.WriteLine("No active courses");
                    Console.WriteLine("Press anywhere to exit");
                    Console.ReadKey();
                    break;
                    }

                case 2:
                    //checks if completedCourses contains anything in the list
                    if (completedCourses.Any())
                    { 
                        foreach (var course in completedCourses)
                        {
                            Console.WriteLine($"{course.Course1}\nStart-date: {course.StartDate}\nEnd-date: {course.EndDate}\n");
                            
                        }

                        Console.WriteLine("Press anywhere to exit");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine("No completed courses");
                    Console.WriteLine("Press anywhere to exit");
                    Console.ReadKey();
                    break;

            }


        }


        public static void SetGrade()
        {
            using var context = new SchoolContext();

            try
            {
                //Sets StudentId
                Console.WriteLine("Enter Student Id: ");
                if (!int.TryParse(Console.ReadLine(), out int studentId))
                {
                    Console.WriteLine("Invalid Student Id. Press anywhere to exit.");
                    Console.ReadKey();
                    return;
                }

                //Sets CourseId
                Console.WriteLine("Enter Course Id: ");
                if (!int.TryParse(Console.ReadLine(), out int courseId))
                {
                    Console.WriteLine("Invalid Course Id. Press anywhere to exit.");
                    Console.ReadKey();
                    return;
                }

                //Sets Grade
                Console.WriteLine("Enter Course Grade: ");
                string gradeLetter = Console.ReadLine();

                //Checks if student is in the written course. If not, exit method
                var studentCourse = context.StudentCourses
                                           .FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);

                if (studentCourse == null)
                {
                    Console.WriteLine("Student is not in this course. Press anywhere to exit.");
                    Console.ReadKey();
                    return;
                }

                //All grades that are valid
                string[] validGrades = { "A", "B", "C", "D", "E", "F" };

                if (!validGrades.Contains(gradeLetter))
                {
                    Console.WriteLine("Invalid grade. Press anywhere to exit.");
                    Console.ReadKey();
                    return;
                }

                //Sets StaffId
                Console.WriteLine("Enter Teacher Id: ");
                if (!int.TryParse(Console.ReadLine(), out int staffId))
                {
                    Console.WriteLine("Invalid Staff Id. Press anywhere to exit.");
                    Console.ReadKey();
                    return;
                }

                using var transaction = context.Database.BeginTransaction();

                //Sets the grading date to DateTime.Now (today), and makes it DateOnly
                DateTime now = DateTime.Now;
                DateOnly today = DateOnly.FromDateTime(now);

                //Creates the grade with the user input.
                var grade = new Grade
                {
                    Grade1 = gradeLetter,
                    StudentId = studentId,
                    CourseId = courseId,
                    GradeSetDate = today,
                    StaffId = staffId,
                    StudentCourse = studentCourse
                };

                //Adds the grade and saves changes
                context.Grades.Add(grade);
                context.SaveChanges();
                transaction.Commit();

                Console.WriteLine("Grade added successfully.");
            }
            catch
            {
                Console.WriteLine("Error. Please try again.");
            }

            Console.WriteLine("Press anywhere to exit.");
            Console.ReadKey();
        }

    }
}
