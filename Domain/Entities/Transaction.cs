using SimpleBanking.Domain.Exceptions;

namespace SimpleBanking.Domain
{
    public class Transaction : Entity
    {
        public Transaction(decimal value, User sender, User receiver)
        {
            Value = value;
            Sender = sender;
            Receiver = receiver;
            Date = DateTime.Now;

            CompareValuesException.IsLowerOrEqualsThan(value, 0, "Valor da transação não informado");
            EmptyException.Throw(sender, "Remetente não informado");
            EmptyException.Throw(receiver, "Destinatário não informado");
            CompareValuesException.IsEqualsThan(sender.Id, receiver.Id, "O usuário remetente não pode ser igual ao emitente");
            CompareValuesException.IsEqualsThan(sender.Type, EUserType.Seller, "Lojistas nao podem enviar transferências!");
            

        }

        public decimal Value { get; private set; }
        public User Sender { get; private set; }
        public User Receiver { get; private set; }
        public DateTime Date { get; private set; }

    }
}