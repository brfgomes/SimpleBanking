using SimpleBanking.Domain;

namespace SimpleBanking.Aplication
{
    public interface IWalletRepository
    {
        public void Insert(Guid id, decimal balance);
        public Wallet GetWalletByUserId(Guid id);
        public void UpdateBalance(Guid id, decimal balance);
        public List<Wallet> GetAllWallets();
    }
}