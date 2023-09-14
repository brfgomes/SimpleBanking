using SimpleBanking.Domain;

namespace SimpleBanking.Aplication
{
    public record CreateUserRequest(
        string name,
        string document,
        string email,
        string password,
        decimal wallet,
        int type
    );
}