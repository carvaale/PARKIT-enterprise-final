/// <summary>
/// Created by Ruiyan Shi 
/// standard services interface for accessing the wallet database table
/// </summary>
namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IAddressProvider
    {
        List<Address> GetAddresses();
        Address GetAddress(Guid userId);
        Address AddAddress(Address address);
        Address UpdateAddress(Address address);
        void DeleteAddress(Guid id);
    }
}
