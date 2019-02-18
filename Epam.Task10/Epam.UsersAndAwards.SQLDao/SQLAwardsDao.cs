using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.DalContracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Epam.UsersAndAwards.SQLDao
{
    public class SQLAwardsDao : IAwardsDao
    {
        public readonly string connectString;

        public SQLAwardsDao(string connectionString)
        {
            this.connectString = connectionString;
        }

        public bool Add(Award award)
        {
            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.Awards (title, imageId) VALUES (@title, @imageId); SELECT scope_identity()";
                cmd.Parameters.AddWithValue("@title", award.Title);
                cmd.Parameters.Add(new SqlParameter("@imageId", DbType.Int32) { Value = award.ImageId });

                con.Open();
                award.Id = (int)(decimal)cmd.ExecuteScalar();

                return award.Id > 0;
            }
        }

        public bool CheckAwardedUsers(int id)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT UserId FROM dbo.UserAwards WHERE AwardId=@awardId";
                cmd.Parameters.Add(new SqlParameter("@awardId", DbType.Int32) { Value = id });

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

        public bool EditTitle(int id, string title)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "UPDATE dbo.Awards SET Title=@Title WHERE Id=@Id";
                cmd.Parameters.Add(new SqlParameter("@Id", DbType.Int32) { Value = id });

                cmd.Parameters.AddWithValue("@Title", title);

                connect.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool ForcedRemove(int id)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "DELETE FROM dbo.UserAwards WHERE AwardId=@awardId; DELETE FROM dbo.Awards WHERE Id=@awardId";
                cmd.Parameters.Add(new SqlParameter("@awardId", DbType.Int32) { Value = id });

                connect.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public IEnumerable<Award> GetAll()
        {
            List<Award> allAwards = new List<Award>();

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT id, title, imageId FROM dbo.Awards";

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string title = (string)reader["title"];
                    int imageId = int.Parse((string)reader["imageId"]);

                    Award award = new Award()
                    {
                        Id = id,
                        Title = title,
                        ImageId = (int)imageId
                    };

                    allAwards.Add(award);
                }

            }

            return allAwards;
        }

        public Award GetAwardById(int awardId)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "SELECT Title, ImageId FROM dbo.Awards WHERE id=@id";
                cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = awardId });

                connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    return null;
                }

                return new Award
                {
                    Id = awardId,
                    Title = (string)reader["Title"],
                    ImageId = int.Parse((string)reader["ImageId"]),
                };
            }
        }

        public bool Remove(int id)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "DELETE FROM dbo.Awards WHERE Id=@awardId";
                cmd.Parameters.Add(new SqlParameter("@awardId", DbType.Int32) { Value = id });

                connect.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }
    }
}

