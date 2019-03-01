using Epam.MySocialNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.LogicContracts
{
    public interface IImagesLogic
    {
        int AddUserImage(Image image);

        Image GetImageById(int id);

        bool EditImage(int id, Image image);

    }
}
