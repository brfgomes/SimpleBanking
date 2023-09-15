namespace SimpleBanking.Aplication
{
    public interface ITransaction
    {
        public GenericResponse Create(CreateTransactionRequest transaction);
    }
}