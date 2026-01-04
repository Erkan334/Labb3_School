using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using Labb3_School;
using Labb3_School.Data;
using Labb3_School.Models;

namespace Labb3_School
{
    internal class Menu
    {
        public static void UserMenu()
        {
            bool isRunning = true;

            while (isRunning) 
            {
                Console.Clear();
                Console.WriteLine("Choose an alternative:");
                Console.WriteLine("1. View students");
                Console.WriteLine("2. View classes");
                Console.WriteLine("3. View staff");
                Console.WriteLine("4. Add student");
                Console.WriteLine("5. Add staff");
                Console.WriteLine("6. Data Menu");
                Console.WriteLine("7. Exit");

                string input = Console.ReadLine();

                if (int.TryParse(input, out int intInput))
                {
                    switch (intInput)
                    {
                        case 1:
                            Methods.ViewStudents();
                            break;

                        case 2:
                            Methods.ViewClasses();
                            break;

                        case 3:
                            Methods.ViewStaff();
                            break;
                        
                        case 4:
                            Methods.AddStudent();
                            break;
                        
                        case 5:
                            Methods.AddStaff();
                            break;

                        case 6:
                            DataMenu.DataUserMenu();
                            break;
                        
                        case 7:
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
