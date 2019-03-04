using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.MySocialNet.Entities;
using Epam.MySocialNet.LogicContracts;

namespace Epam.MySocialNet.SQLDao
{
    public class ImagesDao : IImageDao
    {
        private readonly string connectString;

        public ImagesDao(string connectionString)
        {
            this.connectString = connectionString;
        }

        public int AddImage(Image image)
        {
            int id = 0;
            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.Images (Data, Name, ContentType) VALUES (@data, @name, @contentType);" +
                                    "SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@data", image.Data);
                cmd.Parameters.AddWithValue("@name", image.Name);
                cmd.Parameters.AddWithValue("@contentType", image.ContentType);

                con.Open();
                id = (int)(decimal)cmd.ExecuteScalar();
            }

            return id;
        }

        public Image GetImageById(int id)
        {
            if (id == 0)
            {
                return null;
            }

            Image image = new Image();

            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Id, Data, Name, ContentType FROM dbo.Images WHERE Id=@id";
                cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = id });

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    return null;
                }

                byte[] data = (byte[])reader["Data"];
                string name = (string)reader["Name"];
                string contentType = (string)reader["ContentType"];

                image.Id = id;
                image.Data = data;
                image.Name = name;
                image.ContentType = contentType;
            }
            return image;
        }

        public bool DeleteImage(int id)
        {
            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM dbo.Images WHERE Id=@id";
                cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = id });

                con.Open();
                int result = cmd.ExecuteNonQuery();
            }

            return true;
        }

        public bool EditImage(int id, Image image)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "UPDATE dbo.Images SET Name=@Name, ContentType=@ContentType, Data=@Data WHERE Id=@Id";
                cmd.Parameters.Add(new SqlParameter("@Id", DbType.Int32) { Value = id });

                cmd.Parameters.AddWithValue("@Name", image.Name);
                cmd.Parameters.AddWithValue("@ContentType", image.ContentType);
                cmd.Parameters.AddWithValue("@Data", image.Data);

                connect.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }
    }
}
