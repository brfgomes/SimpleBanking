using SimpleBanking.Domain;

namespace SimpleBanking.Aplication
{
    public class CreateTrasction
    {
        public static bool Execute(CreateTrasctionRequest request)
        {
            if (request == null)
                return false;

            // var transaction = new Transaction(
            //     request.value,
            //     //Pegar user no banco com o ID passado e criar ele aqui
            //     new User()
            //     request.userSenderId,
            //     request.userReceiverId
                
            // );
            return true;
        }
    }
}