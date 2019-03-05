using Epam.MySocialNet.Entities;
using Epam.MySocialNet.LogicContracts;
using Epam.MySocialNet.SQLDao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.Logic
{
    public class ImagesLogic : IImagesLogic
    {
        private readonly IImageDao imageDao;

        public ImagesLogic()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MSN"].ConnectionString;
            this.imageDao = new ImagesDao(connectionString);
        }

        public int AddUserImage(Image image)
        {
            if (!CheckImage(image))
            {
                return -1;
            }

            return imageDao.AddImage(image);
        }

        public bool EditImage(Image image)
        {
            if (!CheckImage(image))
            {
                return false;
            }

            return imageDao.EditImage(image);
        }

        public bool DeleteImage(int id)
        {
            if (id < 1)
            {
                return false;
            }

            return imageDao.DeleteImage(id);
        }

        public Image GetImageById(int id)
        {
            if (id < 1)
            {
                return null;
            }

            return imageDao.GetImageById(id);
        }

        private bool CheckImage(Image image)
        {
            if (image == null || image.Id < 1 || image.Data.Length == 0 || string.IsNullOrWhiteSpace(image.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
