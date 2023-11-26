namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IWalletProvider
    {
        Wallet GetWallet(Guid userId);
        Wallet AddWallet(Wallet wallet);
        Wallet UpdateWallet(Wallet wallet);
    }
}
