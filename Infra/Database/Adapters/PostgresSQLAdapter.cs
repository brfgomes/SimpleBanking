using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace SimpleBanking.Infra.Database
{
    public class PostgresSQLAdapter : IDatabaseConnection
    {
        public PostgresSQLAdapter()
        {
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=simplebanking;";
            _connection = new NpgsqlConnection(connectionString);
        }

        private readonly NpgsqlConnection _connection;

        public void Open()
        {
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
        }

        public void Close()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }

        public void Command(string sql, Dictionary<string, object> parameters = null)
        {
            try
            {
                var sqlCommand = new NpgsqlCommand(sql, _connection);

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = 30;
                sqlCommand.CommandText = sql;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        sqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DbDataReader Query(string sql, Dictionary<string, object> parameters)
        {
            try
            {
                var sqlCommand = new NpgsqlCommand(sql, _connection);

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = 30;
                sqlCommand.CommandText = sql;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        sqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }

                var dbReader = sqlCommand.ExecuteReader();
                return dbReader;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
