namespace SimpleBanking.Aplication
{
    public record CreateTrasctionRequest(
        decimal value,
        string userSenderId,
        string userReceiverId
    );
}