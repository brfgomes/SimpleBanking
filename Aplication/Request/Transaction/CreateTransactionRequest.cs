namespace SimpleBanking.Aplication
{
    public record CreateTransactionRequest(
        decimal value,
        string userSenderId,
        string userReceiverId
    );
}