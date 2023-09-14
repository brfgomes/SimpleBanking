using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using SimpleBanking.Infra.Database.Interfaces;

namespace SimpleBanking.Infra.Database
{
    public class SQLiteAdapter : IDatabaseConnection
    {
        public SQLiteAdapter(){
            var path = $"{ AppDomain.CurrentDomain.BaseDirectory}/database";
            var ConnectionString = $"Data Source={path};Version=3;";
            _connection = new SQLiteConnection(ConnectionString);
        }
        
        private readonly SQLiteConnection _connection;
        
        public void Open(){
            if(_connection.State == ConnectionState.Closed)
                _connection.Open();
        }
        public void Close(){
            if(_connection.State == ConnectionState.Open)
                _connection.Close();
        }
        public void Command(string sql, Dictionary<string, object> parameters = null)
        {
            try
            {
                var SQLCommand = _connection.CreateCommand();
            
                SQLCommand.CommandType = CommandType.Text;
                SQLCommand.CommandTimeout = 30;
                SQLCommand.CommandText = sql;

                if(parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        SQLCommand.Parameters.Add(parameter.Key, (DbType)parameter.Value);
                    }
                }

                SQLCommand.ExecuteNonQuery();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public DbDataReader Query(string sql, Dictionary<string, object> parameters)
        {
            try
            {
                var SQLCommand = _connection.CreateCommand();
            
                SQLCommand.CommandType = CommandType.Text;
                SQLCommand.CommandTimeout = 30;
                SQLCommand.CommandText = sql;

                if(parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        SQLCommand.Parameters.Add(parameter.Key, (DbType)parameter.Value);
                    }
                }

                var DbReader = SQLCommand.ExecuteReader();
                return DbReader;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}