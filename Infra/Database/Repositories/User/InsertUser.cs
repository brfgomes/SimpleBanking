using System.Data.Common;
using SimpleBanking.Infra.Database.Interfaces;
using SimpleBanking.Infra.Database.Repositories.Wallet;

namespace SimpleBanking.Infra.Database.Repositories.User
{
    public static class InsertUser
    {
        public static (bool Success, string Message) Execute(IDatabaseConnection databaseConnection, string id, string name,string document,string email,string password, int type, string walletId, decimal walletBalance)
        {
            try
            {
                //primeiro validar de já existe no banco alguem com esse document OU email cadastrados
                var existUser = GetUser.Execute(databaseConnection, document, email);
                while(existUser.Read())
                {
                    if(existUser["document"] == document || existUser["email"] == email)
                    {
                        return (false, "Usuário já existe!");
                    }
                }

                //depois de validado seguir criando a entidade e o cadastro no banco da WALLET - carteira
                InsertWallet.Execute(databaseConnection, walletId, walletBalance);

                //criar o user utilizando  o id da wallet após a mesma já ter sido criada no banco
                var sql = "INSERT INTO users (id, name, document, email, password, type, wallet) VALUES(@Id, @Name, @Document, @Email, @Password, @Type, @Wallet);";
                var parameters = new Dictionary<string, object>
                {
                    { "@Id", id },
                    { "@Name", name },
                    { "@Document", document },
                    { "@Email", email },
                    { "@Password", password },
                    { "@Type", type },
                    { "@Wallet", walletId }
                };

                databaseConnection.Command(sql, parameters);
                return (true, "Usuário criado com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao tentar criar Usuário Exception: {ex.ToString()}");
            }
            finally
            {
                databaseConnection.Close();
            }
        }
    }
}