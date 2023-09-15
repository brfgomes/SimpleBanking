using SimpleBanking.Domain;

namespace SimpleBanking.Aplication
{
    public interface ITransactionRepository
    {
        public bool Insert(Transaction transaction);
        public List<Transaction> GetAllTransactions();
    }
}