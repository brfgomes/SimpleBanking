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
            var sql = " INSERT INTO transactions (id, value, sender, receiver, date) VALUES (@Id, @Value, @Sender, @Receiver, CURRENT_TIMESTAMP);";
            var parameters = new Dictionary<string, object>
            {
                {"@Id", transaction.Id.ToString()},
                {"@Value", transaction.Value},
                {"@Sender", transaction.Sender.Id.ToString()},
                {"@Receiver", transaction.Receiver.Id.ToString()},
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