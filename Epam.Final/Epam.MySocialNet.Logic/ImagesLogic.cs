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
            throw new NotImplementedException();
        }

        public bool EditImage(Image image)
        {
            throw new NotImplementedException();
        }

        public bool DeleteImage(int id)
        {
            throw new NotImplementedException();
        }

        public Image GetImageById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
