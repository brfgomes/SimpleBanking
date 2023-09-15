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

            if(value <= 0){
                throw new Exception("Valor da transação não informado");
            }

            if(sender == null || receiver == null)
            {
                throw new Exception("Usuários não informados");
            }

            // if(value < sender.Wallet.Balance)
            // {
            //     throw new Exception("Saldo insuficiente");
            // }

        }

        public decimal Value { get; private set; }
        public User Sender { get; private set; }
        public User Receiver { get; private set; }
        public DateTime Date { get; private set; }

    }
}