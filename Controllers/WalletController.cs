using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Models;

namespace PARKIT_enterprise_final.Controllers
{
    public class WalletController : Controller
    {
        private readonly IWalletProvider _walletProvider;

        public WalletController(IWalletProvider walletProvider)
        {
            _walletProvider = walletProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

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

        public IActionResult AllWallets()
        {
            return View(_walletProvider.GetWallets());
        }
    }
}
