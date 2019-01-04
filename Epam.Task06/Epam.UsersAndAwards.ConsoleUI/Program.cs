
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
        private static IAwardLogic awardsLogic;
        private static IUserAwardsLogic userAwardsLogic;

        static void Main(string[] args)
        {
            usersLogic = new UserLogic();
            awardsLogic = new AwardLogic();
            userAwardsLogic = new UserAwardsLogic();

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

                    case "Medal":
                    case "medal":
                    case "M":
                    case "m":
                        AwardMenu();
                        //choice = ReadMenuChoice();
                        //AwardWork(choice);
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
            Console.WriteLine("Medal - give or remove user award");
            Console.WriteLine("Choose your option");
        }

        private static void AwardMenu()
        {
            while (true)
            {
                ShowAwardMenu();
                string awardChoice = ReadMenuChoice();
                switch (awardChoice)
                {
                    case "list":
                    case "l":
                    case "List":
                    case "L":
                        ShowAllAwards();
                        break;

                    case "Add":
                    case "a":
                    case "add":
                    case "A":
                        AddUserAward();
                        break;

                    case "remove":
                    case "r":
                    case "Remove":
                    case "R":
                        RemoveUserAward();
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

        private static void RemoveUserAward()
        {
            Console.WriteLine("Enter a award ID:");
            int awardId;
            bool resultAwardId = int.TryParse(Console.ReadLine(), out awardId);
            Console.WriteLine("Enter a user ID:");
            int userId;
            bool resultUserId = int.TryParse(Console.ReadLine(), out userId);
            if (resultAwardId && resultUserId)
            {
                if (awardsLogic.Remove(awardId, userId))
                {
                    Console.WriteLine("Award was remove successfully");
                }
                else
                {
                    Console.WriteLine("Award removing error");
                }
            }
            else
            {
                Console.WriteLine("One or both ID incorrect");
            }

            PressAnyKey();
        }

        private static void AddUserAward()
        {
            Console.WriteLine("Enter a award ID:");
            int awardId;
            bool resultAwardId = int.TryParse(Console.ReadLine(), out awardId);
            Console.WriteLine("Enter a user ID:");
            int userId;
            bool resultUserId = int.TryParse(Console.ReadLine(), out userId);
            if (resultAwardId && resultUserId)
            {
                if (awardsLogic.Add(awardId, userId))
                {
                    Console.WriteLine("Award was added successfully");
                }
                else
                {
                    Console.WriteLine("Award addition error");
                }
            }
            else
            {
                Console.WriteLine("One or both ID incorrect");
            }

            PressAnyKey();
        }

        private static void ShowAllAwards()
        {
            IEnumerable<Award> awards = awardsLogic.GetAll();
            foreach (Award award in awards)
            {
                ShowAward(award);
            }

            PressAnyKey();
        }

        private static void ShowAward(Award award)
        {
            Console.WriteLine($"{award.Id}: {award.Title}");
        }

        static void ShowAwardMenu()
        {
            Console.Clear();
            Console.WriteLine("List - show all awards");
            Console.WriteLine("Add - award to user");
            Console.WriteLine("Remove - award from user");
            Console.WriteLine("Exit - exit from awards menu");
            Console.WriteLine("Make your choice");
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
            //if (user.AwardExists)
            //{
            //string listAwards = null;
            //foreach (var award in userAwardsLogic.GetUserAwards(user))
            //{
            //    listAwards += award;
            //}
            Console.WriteLine($"{user.Id}: {user.FirstName} {user.LastName}. {user.BirthDate.ToShortDateString()} ({user.Age} ages) Awards:{userAwardsLogic.GetUserAwards(user).ToArray()}");
            //}
            //else
            //{
            //    Console.WriteLine($"{user.Id}: {user.FirstName} {user.LastName}. {user.BirthDate} ({user.Age} ages) Awards:{user.AwardExists}");
            //}
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