namespace SimpleBanking.Aplication.Response.Transaction
{
    public class TransactionResponse
    {
        public TransactionResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
