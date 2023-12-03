/// <summary>
/// Created by Ruiyan Shi 
/// standard controller for the Wallet object, used for control the traffic of wallet
/// </summary>
using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Models;

namespace PARKIT_enterprise_final.Controllers
{
    public class WalletController : Controller
    {
        // interface to access wallet provider
        private readonly IWalletProvider _walletProvider;

        public WalletController(IWalletProvider walletProvider)
        {
            _walletProvider = walletProvider;
        }

        // index method
        public IActionResult Index()
        {
            return View();
        }


        // edit method for wallet, using ID to access the target wallet
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Wallet wallet = _walletProvider.GetWallet(id);
            return View(wallet);
        }
        [HttpPost]
        public IActionResult Edit(Wallet wallet)
        {
            _walletProvider.UpdateWallet(wallet); ;

            // need to redirect to another distination
            return RedirectToAction("AllWallets");
        }

        // get all wallets
        public IActionResult AllWallets()
        {
            return View(_walletProvider.GetWallets());
        }
    }
}
