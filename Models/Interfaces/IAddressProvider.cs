namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IAddressOperations
    {
        Address GetAddress(int userId);
        Address AddAddress(Address address);
        Address UpdateAddress(Address address);
    }
}
