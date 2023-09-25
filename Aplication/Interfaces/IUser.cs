
using SimpleBanking.Domain;

namespace SimpleBanking.Aplication
{
    public interface IUser
    {
        public GenericResponse Create(CreateUserRequest user);

        public GenericResponse GetAll();

        public GenericResponse Change(ChangeUserRequest request);
    }
}