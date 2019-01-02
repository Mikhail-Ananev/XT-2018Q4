using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.LogicContracts;
using Epam.UsersAndAwards.TextFilesDao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.UsersAndAwards.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUsersDao usersDao;

        public UserLogic()
        {
            this.usersDao = new TextFilesDao.UsersDao();
        }

        public bool Add(string firstName, string lastName, string stringBirthDate)
        {
            if (!Check(firstName))
            {
                return false;
            }

            if (!Check(lastName))
            {
                return false;
            }

            DateTime birthDate;
            try
            {
                birthDate = DateTime.Parse(stringBirthDate);
            }
            catch
            {
                Console.WriteLine("Incorrect input birthday");
                return false;
            }

            int nowDate = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int birstDay = int.Parse(birthDate.ToString("yyyyMMdd"));
            int age = (nowDate - birstDay) / 10000;
            if (age < 4 || age > 125)
            {
                Console.WriteLine("Incorrect input birthday");
                return false;
            }
            //Не отдал Id - это забота userDao
            User user = new User { FirstName = firstName, LastName = lastName, BirthDate = birthDate, Age = age };
            try
            {
                this.usersDao.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //где-то 1:00:00
        public IEnumerable<User> GetAll()
        {
            return this.usersDao.GetAll().ToArray();
        }

        public bool Remove(int id)
        {
            return this.usersDao.Remove(id);
        }

        private bool Check(string str)
        {
            if (str == null)
            {
                Console.WriteLine("User first or last name must contains symbols!");
                return false;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsLetter(str[i]))
                {
                    Console.WriteLine("User name must contain only letters!");
                    return false;
                }
            }

            if (!char.IsUpper(str[0]))
            {
                Console.WriteLine("The first symbol user first or last name must be upper!");
                return false;
            }

            return true;
        }

    }
}
//private readonly INotesDao notesDao;
//public NoteLogic()
//{
//    this.notesDao = new TextFilesDal.NotesDao();
//}

//public bool Add(string noteText)
//{
//    if (noteText == null)
//    {
//        throw new ArgumentException(nameof(noteText));
//    }

//    if (noteText.Length == 0 || noteText.Length > 140)
//    {
//        throw new ArgumentException("Wrong note length. Should be between 0 and 140", nameof(noteText));
//    }

//    Note note = new Note { Text = noteText, CreateDate = DateTime.Now };

//    try
//    {
//        notesDao.Add(note);
//        return true;
//    }
//    catch (Exception)
//    {

//        return false;
//    }
//}

//public IEnumerable<Note> GetAll()
//{
//    return notesDao.GetAll().ToArray();
//}

//public bool Remove(int id)
//{
//    return notesDao.Remove(id);
//}
