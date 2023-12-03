/// <summary>
/// Created by Ruiyan Shi 
/// standard CRUD operations for accessing the Addressase table
/// </summary>
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
        // Add Address object to table
        public Address AddAddress(Address address)
        {
            _parkitDb.Addresses.Add(address);
            _parkitDb.SaveChanges();
            return address;
        }

        // delete address object from table
        public void DeleteAddress(Guid id)
        {
            var address = GetAddress(id);
            _parkitDb.Addresses.Remove(address);
            _parkitDb.SaveChanges();
        }

        // get address object by ID
        public Address GetAddress(Guid userId)
        {
            return _parkitDb.Addresses.Find(userId);
        }

        // get all addresses
        public List<Address> GetAddresses()
        {
            List<Address> addresses = _parkitDb.Addresses.ToList();
            return addresses;
        }

        // update address
        public Address UpdateAddress(Address address)
        {
            var a = _parkitDb.Addresses.Attach(address);
            a.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _parkitDb.SaveChanges();
            return address;
        }
    }
}
