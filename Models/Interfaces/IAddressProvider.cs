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
