using SimpleBanking.Domain;

namespace SimpleBanking.Aplication
{
    public record ChangeUserRequest(
        string id,
        string name,
        string document,
        string email,
        string password,
        EUserType type
    );
}