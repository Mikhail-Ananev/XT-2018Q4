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

            //foreach (var award in allAwards)
            //{
            //    using (var con = new SqlConnection(connectString))
            //    {
            //        SqlCommand cmd = con.CreateCommand();
            //        cmd.CommandText = "SELECT userId FROM dbo.UserAward WHERE awardId = @awardId";
            //        cmd.Parameters.Add(new SqlParameter("@awardId", DbType.Int32) { Value = award.Id });

            //        con.Open();
            //        SqlDataReader reader = cmd.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            int? userId = reader["userId"] as int?;
            //            award.UserId = (int)userId;
            //        }
            //    }
            //}

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
                    Title = (string)reader["Title"],
                    ImageId = int.Parse((string)reader["ImageId"]),
                };
            }
        }

        //public Award GetAwardById(int id)
        //{
        //    if (id == 0)
        //    {
        //        return null;
        //    }
        //    Award award = new Award();
        //    using (var con = new SqlConnection(connectString))
        //    {
        //        SqlCommand cmd = con.CreateCommand();
        //        cmd.CommandText = "SELECT id, title, imageId FROM dbo.Awards WHERE id=@id";
        //        cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = id });

        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (!reader.Read())
        //        {
        //            return null;
        //        }

        //        string title = (string)reader["title"];
        //        int? imageId = reader["imageId"] as int?;

        //        award.Id = id;
        //        award.Title = title;
        //        award.ImageId = (int)imageId;
        //    }

        //    using (var con = new SqlConnection(connectString))
        //    {
        //        SqlCommand cmd = con.CreateCommand();
        //        cmd.CommandText = "SELECT userId FROM dbo.UserAward WHERE awardId = @awardId";
        //        cmd.Parameters.Add(new SqlParameter("@awardId", DbType.Int32) { Value = award.Id });

        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            int? userId = reader["userId"] as int?;
        //            award.UserId = (int)userId;
        //        }
        //    }

        //    return award;
        //}
    }
}



//public bool Delete(int id)
//{
//    using (var con = new SqlConnection(conStr))
//    {
//        SqlCommand cmd = con.CreateCommand();
//        cmd.CommandText = "DELETE FROM dbo.Awards WHERE id=@id";
//        cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = id });

//        con.Open();
//        int result = cmd.ExecuteNonQuery();

//        return result > 0;
//    }
//}

//public void DeleteUser(int id)
//{
//    Award[] awards = GetAll().ToArray();
//    foreach (var awd in awards)
//    {
//        awd.UsersIds.Remove(id);
//    }
//}
