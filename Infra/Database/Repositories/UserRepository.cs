using SimpleBanking.Aplication;
using SimpleBanking.Domain;
using SimpleBanking.Infra.Database.Interfaces;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Collections.Generic;

namespace SimpleBanking.Infra.Database
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public UserRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public List<User> GetAllUsers()
        {
            _databaseConnection.Open();
            var sql = "SELECT * FROM users";
            var dbUsers = _databaseConnection.Query(sql);

            List<User> listUsers = new List<User>();

            while(dbUsers.Read())
            {
                var newUser = new User(
                    dbUsers["name"].ToString(),
                    new Document(dbUsers["document"].ToString()),
                    new Email(dbUsers["email"].ToString()),
                    new Password(dbUsers["password"].ToString()),
                    (EUserType)dbUsers["type"]
                );
                var stringId = dbUsers["id"].ToString();
                Guid guidId = new Guid(stringId);
                newUser.SetId(guidId);

                listUsers.Add(newUser);
            }
            _databaseConnection.Close();
            return listUsers;
        }

        public int IfExistsUserDocument(string document)
        {
            try
            {
                _databaseConnection.Open();
                var parameters = new Dictionary<string, object>();
                var sql = "SELECT * FROM users WHERE document = @Document";
                parameters.Add("@Document", document);
                var dbUsers = _databaseConnection.Query(sql, parameters);
                var rowCount = 0;
                while(dbUsers.Read())
                {
                    rowCount++;
                }
                _databaseConnection.Close();
                return rowCount;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public int IfExistsUserEmail(string email)
        {
            try
            {
                _databaseConnection.Open();
                var parameters = new Dictionary<string, object>();
                var sql = "SELECT * FROM users WHERE email = @Email";
                parameters.Add("@Email", email);
                var dbUsers = _databaseConnection.Query(sql, parameters);
                var rowCount = 0;
                while (dbUsers.Read())
                {
                    rowCount++;
                }
                _databaseConnection.Close();
                return rowCount;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool Insert(User user)
        {
            var sql = "INSERT INTO users (id, name, document, email, password, type) VALUES(@Id, @Name, @Document, @Email, @Password, @Type);";
            var parameters = new Dictionary<string, object>
            {
                { "@Id", user.Id.ToString() },
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
                _databaseConnection.Close();
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    
        public User GetUserById(string id)
        {
            id = id.ToLower();
            if(id.Contains('-') == false)
            {
                id = id.Insert(8, "-").Insert(13, "-").Insert(18, "-").Insert(23, "-");
            }

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
                    Guid guidId = new Guid(id);
                    user.SetId(guidId);

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

        public bool UpdateUser(User user)
        {
            var sql = "UPDATE users SET name = @Name, document = @Document, email = @Email, password = @Password, type = @Type WHERE id = @Id";

            var parameters = new Dictionary<string, object>
            {
                { "@Id", user.Id.ToString() },
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
                _databaseConnection.Close();
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}