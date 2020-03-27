using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.ColorService
{
    public class ColorService : IColorService
    {
        private readonly AppDbContext _context;

        public ColorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Color> AddColorAsync(Color color)
        {
            var existColor = ColorExist(color);

            if (existColor)
            {
                return null;
            }

            await _context.Colors.AddAsync(color);
            await _context.SaveChangesAsync();

            return color;
        }

        private bool ColorExist(Color color)
        {
            return _context.Colors
                .Where(c => (c.ColorCode == color.ColorCode || c.Name == color.Name) && c.ProductID == color.ProductID)
                .AsNoTracking()
               .FirstOrDefault() != null;
        }

        public async Task<IEnumerable<Color>> AddColorsAsync(IEnumerable<Color> colors)
        {
            foreach (var color in colors)
            {
                bool exist = ColorExist(color);
                if (exist)
                {
                    return null;
                }
            }

            await _context.Colors.AddRangeAsync(colors);
            await _context.SaveChangesAsync();

            return colors;
        }

        public Color DeleteColor(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Color> GetColors(int productID)
        {
            return _context.Colors.Where(c => c.ProductID == productID);
        }

        public Color UpdateColor(Color color)
        {
            throw new NotImplementedException();
        }
    }
}
