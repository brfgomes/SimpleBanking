
using SimpleBanking.Domain;

namespace SimpleBanking.Aplication
{
    public interface IUser
    {
        GenericResponse Create(CreateUserRequest user);

        GenericResponse GetAll();

        GenericResponse Change(ChangeUserRequest request);
    }
}