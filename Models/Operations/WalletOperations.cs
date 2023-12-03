/// <summary>
/// Created by Ruiyan Shi 
/// standard CRUD operations for accessing the wallet database table
/// </summary>
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

        // add wallet
        public Wallet AddWallet(Wallet wallet)
        {
            _parkitDb.Wallets.Add(wallet);
            _parkitDb.SaveChanges();
            return wallet;
        }

        // delete wallet
        public void DeleteWallet(Guid id)
        {
            var wallet = GetWallet(id);
            _parkitDb.Wallets.Remove(wallet);
            _parkitDb.SaveChanges();
        }

        // get wallet by ID
        public Wallet GetWallet(Guid userId)
        {
            return _parkitDb.Wallets.Find(userId);
        }

        // get lists of wallets
        public List<Wallet> GetWallets()
        {
            List<Wallet> wallets = _parkitDb.Wallets.ToList();
            return wallets;
        }

        // update the wallet
        public Wallet UpdateWallet(Wallet wallet)
        {
            var w = _parkitDb.Wallets.Attach(wallet);
            w.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _parkitDb.SaveChanges();
            return wallet;
        }
    }
}
