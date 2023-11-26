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
