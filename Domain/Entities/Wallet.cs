namespace SimpleBanking.Domain
{
    public class Wallet : Entity
    {
        public Wallet(decimal balance)
        {
            Balance = balance;

            if (balance <= 0)
            {
                throw new Exception("Saldo invalido");
            }
        }

        public decimal Balance { get; private set; }
        public Guid UserId {get; private set;}
        public DateTime? LastTransactionDate { get; private set; }
        public List<Transaction>? TransactionsHistory { get; private set; }

        public void SetLastTransactionDate(DateTime? lastTransactionDate)
        {
            LastTransactionDate = lastTransactionDate;
        }
    }
}