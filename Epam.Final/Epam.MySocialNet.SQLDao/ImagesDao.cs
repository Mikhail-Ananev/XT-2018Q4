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
    public class ImagesDao : IImagesDao
    {
        private readonly string connectString;

        public ImagesDao(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("Connection string empty!", nameof(connectionString));
            }

            this.connectString = connectionString;
        }

        public int AddImage(Image image)
        {
            if (CheckNull(image))
            {
                throw new ArgumentNullException("Input data is empty!", nameof(image));
            }

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
            if (id < 1)
            {
                return null;
            }

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

                Image image = new Image()
                {
                    Id = id,
                    Data = data,
                    Name = name,
                    ContentType = contentType,
                };

                return image;
            }
        }

        public bool DeleteImage(int id)
        {
            using (var con = new SqlConnection(connectString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM dbo.Images WHERE Id=@id";
                cmd.Parameters.Add(new SqlParameter("@id", DbType.Int32) { Value = id });

                con.Open();

                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool EditImage(Image image)
        {
            if (CheckNull(image))
            {
                throw new ArgumentNullException("Input data is empty!", nameof(image));
            }

            using (SqlConnection connect = new SqlConnection(connectString))
            {
                SqlCommand cmd = connect.CreateCommand();
                cmd.CommandText = "UPDATE dbo.Images SET Name=@Name, ContentType=@ContentType, Data=@Data WHERE Id=@Id";
                cmd.Parameters.Add(new SqlParameter("@Id", DbType.Int32) { Value = image.Id });

                cmd.Parameters.AddWithValue("@Name", image.Name);
                cmd.Parameters.AddWithValue("@ContentType", image.ContentType);
                cmd.Parameters.AddWithValue("@Data", image.Data);

                connect.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        private bool CheckNull (Image image)
        {
            if (image == null)
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
