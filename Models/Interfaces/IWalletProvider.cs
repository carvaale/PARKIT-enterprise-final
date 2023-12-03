/// <summary>
/// Created by Ruiyan Shi 
/// standard services interface for accessing the wallet database table
/// </summary>
namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IWalletProvider
    {
        List<Wallet> GetWallets();
        Wallet GetWallet(Guid userId);
        Wallet AddWallet(Wallet wallet);
        Wallet UpdateWallet(Wallet wallet);
        void DeleteWallet(Guid id);
    }
}
