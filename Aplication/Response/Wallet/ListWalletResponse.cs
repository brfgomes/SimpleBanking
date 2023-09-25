using SimpleBanking.Domain;

namespace SimpleBanking.Aplication.Response.User
{
    public class ListWalletResponse
    {
        public ListWalletResponse(Guid id, string name, decimal wallet, string lastTransactionDate)
        {
            Id = id;
            Name = name;
            Wallet = wallet;
            LastTransactionDate = lastTransactionDate;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Wallet { get; private set; }
        public string LastTransactionDate { get; private set; }
    }
}
