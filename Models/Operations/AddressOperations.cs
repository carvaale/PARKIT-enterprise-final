using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;

namespace PARKIT_enterprise_final.Models.Operations
{
    public class AddressOperations : IAddressOperations
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

        public Address GetAddress(Guid userId)
        {
            return _parkitDb.Addresses.Find(userId);
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
