
using SimpleBanking.Domain;

namespace SimpleBanking.Aplication
{
    public interface IUser
    {
        public GenericResponse Create(CreateUserRequest user);
    }
}