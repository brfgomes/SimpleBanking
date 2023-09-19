using SimpleBanking.Domain;

namespace SimpleBanking.Aplication
{
    public record ChangeUserRequest(
        string id,
        string name,
        string document,
        string email,
        string password,
        decimal wallet,
        EUserType type
    );
}