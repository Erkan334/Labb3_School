using System;
using System.Collections.Generic;
using System.Text;

namespace Labb3_School
{
    internal class DataMenu
    {
        public static void DataUserMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Choose an alternative:");
                Console.WriteLine("1. View amount of staff by department");
                Console.WriteLine("2. View student information");
                Console.WriteLine("3. View courses");
                Console.WriteLine("4. Set grades");
                Console.WriteLine("5. Exit");

                string input = Console.ReadLine();

                if (int.TryParse(input, out int intInput))
                {
                    switch (intInput)
                    {
                        case 1:
                            Methods.StaffAmount();
                            break;

                        case 2:
                            Methods.StudentInformation();
                            break;

                        case 3:
                            Methods.ActiveCourses();
                            break;

                        case 4:
                            Methods.SetGrade();
                            break;

                        case 5:
                            isRunning = false;
                            break;

                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                }
            }
        }
    }
}
