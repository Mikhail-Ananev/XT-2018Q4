using Epam.UsersAndAwards.Entities;
using Epam.UsersAndAwards.LogicContracts;
using Epam.UsersAndAwards.DalContracts;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.Logic
{
    public class ImageLogic : IImageLogic
    {
        private IImageDao imageDao;

        public ImageLogic()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UaADB"].ConnectionString;
            this.imageDao = new SQLImagesDao(connectionString);
        }
        public int AddUserImage(Image image)
        {

            if (imageDao == null)
            {
                return 0;
            }
            return imageDao.AddUserImage(image);
        }

        public Image GetImageById(int id)
        {
            if (imageDao == null)
            {
                return null;
            }
            return imageDao.GetImageById(id);
        }
    }
}
