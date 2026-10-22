using UnityEngine;

namespace Wallet
{
    public class WalletInstaller : MonoBehaviour
    {
        [SerializeField] private WalletUI _walletUI;

        private WalletService _walletService;

        public WalletService WalletService => _walletService;

        private void Awake()
        {
            _walletService = new WalletService();

            if (_walletUI == null)
            {
                Debug.LogError("WalletInstaller: не назначен _walletUI.");
                return;
            }

            _walletUI.Initialize(_walletService);
        }
    }
}