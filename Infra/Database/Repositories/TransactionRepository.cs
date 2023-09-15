using SimpleBanking.Aplication;
using SimpleBanking.Domain;
using SimpleBanking.Infra.Database.Interfaces;

namespace SimpleBanking.Infra.Database
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public TransactionRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public List<Transaction> GetAllTransactions()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Transaction transaction)
        {
            var sql = " INSERT INTO transactions (id, value, sender, receiver) VALUES (@Id, @Value, @Sender, @Receiver);";
            var parameters = new Dictionary<string, object>
            {
                {"@Id", transaction.Id},
                {"@Value", transaction.Value},
                {"@Sender", transaction.Sender.Id},
                {"@Receiver", transaction.Receiver.Id},
            };
            try
            {
                _databaseConnection.Open();
                _databaseConnection.Command(sql, parameters);
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                _databaseConnection.Close();
            }
        }
    }
}