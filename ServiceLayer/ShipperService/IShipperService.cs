using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ShipperService
{
    public interface IShipperService
    {
        IEnumerable<Shipper> GetAllShippers();
        Shipper GetShiperById(int id);
        Shipper RemoveShipper(int id);
        Task<Shipper> AddShipperAsync(Shipper shipper);
        Shipper UpdateShipper(Shipper shipper);

    }
}
