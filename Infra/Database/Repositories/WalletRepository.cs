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
                    {"@UserId", userId.ToString()}
                };
                var dbWallets = _databaseConnection.Query(sql, parameters);
                Wallet wallet = null;
                while (dbWallets.Read())
                {
                    wallet = new Wallet(
                        (decimal)dbWallets["balance"]
                    );

                    wallet.SetId(userId);
                    var lastTransactionDateDB = dbWallets["lasttransactiondate"].ToString();
                    if (lastTransactionDateDB != "")
                    {
                        wallet.SetLastTransactionDate((DateTime)dbWallets["lasttransactiondate"]);
                    }

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
                { "@UserId", userId.ToString() },
                { "@Balance", balance }
            };
            try
            {
                _databaseConnection.Open();
                _databaseConnection.Command(sql, parameters);
                _databaseConnection.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {                   
            }
        }

        public void UpdateBalance(Guid userId, decimal balance)
        {
            var sql = "UPDATE wallets SET balance= @Balance, lasttransactiondate= CURRENT_TIMESTAMP WHERE userid= @UserId;";
            var parameters = new Dictionary<string, object>
            {
                { "@UserId", userId.ToString() },
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