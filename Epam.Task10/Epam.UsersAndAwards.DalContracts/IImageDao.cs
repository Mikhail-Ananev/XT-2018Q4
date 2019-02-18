using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.DalContracts
{
    public interface IImageDao
    {
        int AddUserImage(Image image);

        Image GetImageById(int id);

        bool Delete(int id);

        bool EditImage(int id, Image image);
    }
}
