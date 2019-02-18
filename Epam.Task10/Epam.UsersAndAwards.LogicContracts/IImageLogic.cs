using Epam.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAndAwards.LogicContracts
{
    public interface IImageLogic
    {
        int AddUserImage(Image image);

        Image GetImageById(int id);

        bool EditImage(int id, Image image);
    }
}
