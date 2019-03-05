using Epam.MySocialNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MySocialNet.LogicContracts
{
    public interface IImageDao
    {
        int AddImage(Image image);

        Image GetImageById(int id);

        bool DeleteImage(int id);

        bool EditImage(Image image);
    }
}
