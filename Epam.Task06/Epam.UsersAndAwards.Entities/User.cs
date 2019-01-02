using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int Age { get; set; }
        //{
        //    get => Age;
        //    private set
        //    {
        //        int nowDate = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
        //        int birstDay = int.Parse(BirthDate.ToString("yyyyMMdd"));
        //        Age = (nowDate - birstDay) / 10000;
        //    }
        //}
    }
}