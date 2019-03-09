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
        private readonly IImagesDao imagesDao;

        public ImagesLogic()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MSN"].ConnectionString;
            this.imagesDao = new ImagesDao(connectionString);
        }

        public int AddUserImage(Image image)
        {
            if (!CheckImage(image))
            {
                return -1;
            }

            return imagesDao.AddImage(image);
        }

        public bool EditImage(Image image)
        {
            if (!CheckImage(image))
            {
                return false;
            }

            return imagesDao.EditImage(image);
        }

        public bool DeleteImage(int id)
        {
            if (id < 1)
            {
                return false;
            }

            return imagesDao.DeleteImage(id);
        }

        public Image GetImageById(int id)
        {
            if (id < 1)
            {
                return null;
            }

            return imagesDao.GetImageById(id);
        }

        private bool CheckImage(Image image)
        {
            if (image == null || image.Data.Length == 0 || string.IsNullOrWhiteSpace(image.Name))
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
