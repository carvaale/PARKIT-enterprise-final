using Microsoft.EntityFrameworkCore;
using PARKIT_enterprise_final.Migrations;
using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;
using System.Reflection;

namespace PARKIT_enterprise_final.Models.Operations
{
    public class AddressOperations : IAddressProvider
    {
        private readonly PARKITDBContext _parkitDb;

        public AddressOperations(PARKITDBContext parkitDb)
        {
            _parkitDb = parkitDb;
        }
        public Address AddAddress(Address address)
        {
            _parkitDb.Addresses.Add(address);
            _parkitDb.SaveChanges();
            return address;
        }

        public void DeleteAddress(Guid id)
        {
            var address = GetAddress(id);
            _parkitDb.Addresses.Remove(address);
            _parkitDb.SaveChanges();
        }

        public Address GetAddress(Guid userId)
        {
            return _parkitDb.Addresses.Find(userId);
        }

        public List<Address> GetAddresses()
        {
            List<Address> addresses = _parkitDb.Addresses.ToList();
            return addresses;
        }

        public Address UpdateAddress(Address address)
        {
            var a = _parkitDb.Addresses.Attach(address);
            a.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _parkitDb.SaveChanges();
            return address;
        }
    }
}
