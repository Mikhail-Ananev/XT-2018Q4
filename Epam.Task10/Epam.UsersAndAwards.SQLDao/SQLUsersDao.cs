using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.TextFilesDao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.SQLDao
{
    public class SQLUsersDao : IUsersDao
    {
        public readonly string connectString;

        public SQLUsersDao(string connectionString)
        {
            this.connectString = connectionString;
        }

        public void Add(User user)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.Users (FirstName, LastName, Birthday) VALUES (@FirstName, @LastName, @Birthday); SELECT scope_identity()";
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Birthday", user.BirthDate);

                connect.Open();
                user.Id = (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public void EditUser(User user)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "UPDATE dbo.Users SET FirstName=@FirstName, LastName=@LastName, Birthday=@Birthday WHERE Id=@Id";
                cmd.Parameters.Add(new SqlParameter("@Id", DbType.Int32) { Value = user.Id });

                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Birthday", user.BirthDate);

                connect.Open();
                int result = cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT Id, FirstName, LastName, Birthday FROM dbo.Users";

                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    yield return new User
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        BirthDate = (DateTime)reader["Birthday"],
                    };
                }
            }
        }

        public User GetUserById(int id)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT FirstName, LastName, Birthday FROM dbo.Users WHERE id=@id";
                cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = id });

                connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    return null;
                }

                return new User
                {
                    Id = id,
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    BirthDate = (DateTime)reader["Birthday"],
                };
            }
        }
    

        public bool Remove(int id)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "DELETE FROM dbo.Users WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);

                connect.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }
    }
}
