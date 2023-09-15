using SimpleBanking.Aplication;
using SimpleBanking.Domain;
using SimpleBanking.Infra.Database.Interfaces;

namespace SimpleBanking.Infra.Database.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public WalletRepository (IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public Wallet GetWalletByUserId(Guid userId)
        {
            try
            {
                _databaseConnection.Open();

                var sql = "SELECT * FROM wallets WHERE userid = @UserId";
                var parameters = new Dictionary<string, object>{
                    {"@UserId", userId}
                };
                var dbWallets = _databaseConnection.Query(sql, parameters);
                Wallet wallet = null;
                while (dbWallets.Read())
                {
                    wallet = new Wallet(
                        (decimal)dbWallets["balnace"]
                    );

                    wallet.SetId((Guid)dbWallets["userid"]);
                    wallet.SetLastTransactionDate((DateTime)dbWallets["lasttransactiondate"]);
                }
                return wallet!;
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

        public void Insert(Guid userId, decimal balance)
        {
            var sql = "INSERT INTO wallets (userid, balance) VALUES(@UserId, @Balance);";
            var parameters = new Dictionary<string, object>
            {
                { "@UserId", userId },
                { "@Balance", balance }
            };
            try
            {
                _databaseConnection.Open();
                _databaseConnection.Command(sql, parameters);
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