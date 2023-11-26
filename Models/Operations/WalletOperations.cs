using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;

namespace PARKIT_enterprise_final.Models.Operations
{
    public class WalletOperations : IWalletProvider
    {
        private readonly PARKITDBContext _parkitDb;

        public WalletOperations(PARKITDBContext parkitDb)
        {
            _parkitDb = parkitDb;
        }
        public Wallet AddWallet(Wallet wallet)
        {
            _parkitDb.Wallets.Add(wallet);
            _parkitDb.SaveChanges();
            return wallet;
        }

        public void DeleteWallet(Guid id)
        {
            var wallet = GetWallet(id);
            _parkitDb.Wallets.Remove(wallet);
            _parkitDb.SaveChanges();
        }

        public Wallet GetWallet(Guid userId)
        {
            return _parkitDb.Wallets.Find(userId);
        }

        public List<Wallet> GetWallets()
        {
            List<Wallet> wallets = _parkitDb.Wallets.ToList();
            return wallets;
        }

        public Wallet UpdateWallet(Wallet wallet)
        {
            var w = _parkitDb.Wallets.Attach(wallet);
            w.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _parkitDb.SaveChanges();
            return wallet;
        }
    }
}
