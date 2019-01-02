
using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.FakeLogic;
using Epam.UsersAndAwards.Logic;
using Epam.UsersAndAwards.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task06.ConsoleUI
{
    class Program
    {
        private static IUserLogic usersLogic;

        static void Main(string[] args)
        {
            usersLogic = new UserLogic();

            while (true)
            {
                ShowMenu();
                string choice = ReadMenuChoice();
                switch (choice)
                {
                    case "list":
                    case "l":
                    case "List":
                    case "L":
                        ShowAllUsers();
                        break;

                    case "Add":
                    case "a":
                    case "add":
                    case "A":
                        AddNewUser();
                        break;

                    case "remove":
                    case "r":
                    case "Remove":
                    case "R":
                        RemoveUser();
                        break;

                    case "quit":
                    case "q":
                    case "exit":
                    case "Quit":
                    case "Q":
                    case "Exit":
                    case "e":
                    case "E":
                    case "":
                        return;

                    default:
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("List - show all users");
            Console.WriteLine("Add - add new user");
            Console.WriteLine("Remove - remove user");
            Console.WriteLine("Choose your option");
        }

        static string ReadMenuChoice()
        {
            return Console.ReadLine();
        }

        static void ShowAllUsers()
        {
            IEnumerable<User> users = usersLogic.GetAll();
            foreach (User user in users)
            {
                ShowUser(user);
            }

            PressAnyKey();
        }

        static void ShowUser(User user)
        {
            Console.WriteLine($"{user.Id}: {user.FirstName} {user.LastName}. {user.BirthDate} ({user.Age} ages)");
        }

        static void AddNewUser()
        {
            Console.WriteLine("Enter a user first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter a user last name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter a user birth date (dd.MM.yyyy):");
            string stringBirthDate = Console.ReadLine();
            if (usersLogic.Add(firstName, lastName, stringBirthDate))
            {
                Console.WriteLine("User was added successfully");
            }
            else
            {
                Console.WriteLine("User addition error");
            }

            PressAnyKey();
        }

        static void RemoveUser()
        {
            Console.WriteLine("Enter an ID of user to remove: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                if (usersLogic.Remove(id))
                {
                    Console.WriteLine("User was remove successfully");
                }
                else
                {
                    Console.WriteLine("Cannot remove a user");
                }
            }
            else
            {
                Console.WriteLine("Bad user ID");
            }

            PressAnyKey();
        }

        private static void PressAnyKey()
        {
            Console.WriteLine("Press any key to back to menu");
            Console.ReadKey();
        }
    }
}