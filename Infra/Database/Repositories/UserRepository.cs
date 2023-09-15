using SimpleBanking.Aplication;
using SimpleBanking.Domain;
using SimpleBanking.Infra.Database.Interfaces;

namespace SimpleBanking.Infra.Database
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public UserRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public List<User> GetUserAll()
        {
            return new List<User>();
            // _databaseConnection.Open();
            // var parameters = new Dictionary<string, object>();
            // var sql = "SELECT * FROM users";
            // var dbUsers = _databaseConnection.Query(sql.ToString(), parameters);

            // List<User> listUsers = new List<User>();
            // while(dbUsers.Read())
            // {
            //     listUsers.Add(new User(
            //         dbUsers["name"].ToString(),
            //         new Document(dbUsers["document"].ToString()),
            //         new Email(dbUsers["email"].ToString()),
            //         new Password(dbUsers["password"].ToString()),
            //         (EUserType)dbUsers["type"],
            //         new Wallet(dbUsers["wallet"].ToString())
            //     ));
            // }
            // _databaseConnection.Close();
            // return users;
        }

        public bool IfExistsUserDocument(string document)
        {
            try
            {
                _databaseConnection.Open();
                var parameters = new Dictionary<string, object>();
                var sql = "SELECT * FROM users WHERE document = @Document";
                parameters.Add("@Document", document);
                var dbUsers = _databaseConnection.Query(sql, parameters);
                return dbUsers.HasRows;
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

        public bool IfExistsUserEmail(string email)
        {
            try
            {
                _databaseConnection.Open();
                var parameters = new Dictionary<string, object>();
                var sql = "SELECT * FROM users WHERE email = @Email";
                parameters.Add("@Email", email);
                var dbUsers = _databaseConnection.Query(sql, parameters);
                return dbUsers.HasRows;
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

        public bool Insert(User user)
        {
            // walletRepository.Insert(user.Wallet.Id, user.Wallet.Balance);

            var sql = "INSERT INTO users (id, name, document, email, password, type) VALUES(@Id, @Name, @Document, @Email, @Password, @Type);";
            var parameters = new Dictionary<string, object>
            {
                { "@Id", user.Id },
                { "@Name", user.Name },
                { "@Document", user.Document.Code },
                { "@Email", user.Email.Address },
                { "@Password", user.Password.Value },
                { "@Type", user.Type }
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
    
        public User GetUserById(string id)
        {
            try
            {
                _databaseConnection.Open();   

                var sql = "SELECT * FROM users WHERE id = @Id";
                var parameters = new Dictionary<string, object>{
                    {"@Id", id}
                };
                var dbUsers = _databaseConnection.Query(sql, parameters);
                User user = null;
                while (dbUsers.Read())
                {
                    user = new User(
                        dbUsers["name"].ToString(),
                        new Document(dbUsers["document"].ToString()),
                        new Email(dbUsers["email"].ToString()),
                        new Password(dbUsers["password"].ToString()),
                        (EUserType)dbUsers["type"]
                    );

                    user.SetId((Guid)dbUsers["id"]);
                }
                return user!;
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