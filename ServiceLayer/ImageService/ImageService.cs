using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Models;

namespace ServiceLayer.ImageService
{
    public class ImageService : IImageService
    {
        private readonly AppDbContext _context;

        public ImageService(AppDbContext context)
        {
            _context = context;
        }

        
    }
}
