using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.ColorService
{
    public interface IColorService
    {
        IEnumerable<Color> GetColors(int productID);
        Task<Color> AddColorAsync(Color color);
        Task<IEnumerable<Color>> AddColorsAsync(IEnumerable<Color> colors);
        Color DeleteColor(int id);
        Color UpdateColor(Color color);

    }
}
