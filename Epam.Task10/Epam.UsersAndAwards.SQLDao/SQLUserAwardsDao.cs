using Epam.UsersAndAwards.DalContracts;
using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.SQLDao
{
    public class SQLUserAwardsDao : IUserAwardsDao
    {
        public readonly string connectString;

        public SQLUserAwardsDao(string connectionString)
        {
            this.connectString = connectionString;
        }

        public bool Add(Award award, User user)
        {
            if (!UserHasAward(award.Id, user.Id))
            {
                using (SqlConnection connect = new SqlConnection(connectString))
                {
                    SqlCommand cmd = connect.CreateCommand();
                    cmd.CommandText = "INSERT INTO dbo.UserAwards (UserId, AwardId) VALUES (@UserId, @AwardId)";
                    //cmd.Parameters.Add(new SqlParameter("@UserId", DbType.Int32) { Value = user.Id });
                    //cmd.Parameters.Add(new SqlParameter("@AwardId", DbType.Int32) { Value = award.Id });

                    cmd.Parameters.AddWithValue("@UserId", user.Id);
                    cmd.Parameters.AddWithValue("@AwardId", award.Id);

                    connect.Open();

                    int result = cmd.ExecuteNonQuery();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool UserHasAward(int awardId, int userId)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT AwardId, UserId FROM dbo.UserAwards WHERE AwardId=@awardId AND UserId=@userId";
                cmd.Parameters.Add(new SqlParameter("@awardId", DbType.Int32) { Value = awardId });
                cmd.Parameters.Add(new SqlParameter("@userId", DbType.Int32) { Value = userId });

                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public IEnumerable<string> GetUserAwards(int id)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT AwardId FROM dbo.UserAwards WHERE UserId=@id";
                cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = id });

                connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                var listAwardId = new List<string>();

                while(reader.Read())
                {
                    listAwardId.Add(((int)reader["AwardId"]).ToString());
                }

                return listAwardId;
            }
        }

        public bool Remove(Award award, User user)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "DELETE FROM dbo.UserAwards WHERE AwardId=@awardId AND UserId=@userId";
                cmd.Parameters.Add(new SqlParameter("@awardId", DbType.Int32) { Value = award.Id });
                cmd.Parameters.Add(new SqlParameter("@userId", DbType.Int32) { Value = user.Id });

                connect.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public void RemoveUserAwards(int userId)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "DELETE FROM dbo.UserAwards WHERE UserId=@userId";
                cmd.Parameters.Add(new SqlParameter("@userId", DbType.Int32) { Value = userId });

                connect.Open();
            }
        }

        public bool UserHasAwards(int userId)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT AwardId FROM dbo.UserAwards WHERE UserId=@userId";
                cmd.Parameters.Add(new SqlParameter("@userId", DbType.Int32) { Value = userId });

                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
