using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Entities;

namespace ServiceLayer.ShipperService
{
    public class ShipperService : IShipperService
    {
        private readonly AppDbContext _context;

        public ShipperService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Shipper> AddShipperAsync(Shipper shipper)
        {
            await _context.Shippers.AddAsync(shipper);
            await _context.SaveChangesAsync();

            return shipper;
        }

        public IEnumerable<Shipper> GetAllShippers()
        {
            return _context.Shippers;
        }

        public Shipper GetShiperById(int id)
        {
            return _context.Shippers.Where(s => s.ID == id)
                .FirstOrDefault();
        }

        public Shipper RemoveShipper(int id)
        {
            var shipper = _context.Shippers.Where(s => s.ID == id)
                .FirstOrDefault();
            if(shipper == null)
            {
                return null;
            }

            _context.Shippers.Remove(shipper);
            _context.SaveChanges();

            return shipper;

        }

        public Shipper UpdateShipper(Shipper shipper)
        {
            _context.Shippers.Update(shipper);
            _context.SaveChanges();

            return shipper;
        }
    }
}
