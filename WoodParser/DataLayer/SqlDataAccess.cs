using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;

namespace WoodParser.DataLayer
{
    public class SqlDataAccess
    {
        private SqlConnectionStringBuilder _connectionStringBuilder;
        private IConfiguration _config;

        public SqlDataAccess()
        {
            _connectionStringBuilder =
                new SqlConnectionStringBuilder(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Lesegais;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        }

        public List<T> LoadData<T>(string sql, Dictionary<string, object> paramsValue = null)
        {
            using (IDbConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString))
            {
                return connection.Query<T>(sql, new DynamicParameters(paramsValue)).ToList();
            }
        }

        public T LoadSingle<T>(string sql, Dictionary<string, object> paramsValue = null)
        {
            using (IDbConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString))
            {
                return connection.QueryFirst<T>(sql, new DynamicParameters(paramsValue));
            }
        }

        public int SaveData<T>(string sql, T data)
        {
            using (IDbConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString))
            {
                return connection.Execute(sql, data);
            }
        }
        public T Insert<T>(string tableName, Dictionary<string, object> fieldValues, string idProperty = "Id")
        {
            var commandText = $"INSERT INTO {tableName}({string.Join(", ", fieldValues.Select(x => $"[{x.Key}]"))}) OUTPUT INSERTED.{idProperty} VALUES({string.Join(", ", fieldValues.Select(x => "@" + x.Key))});";
            using (IDbConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString))
            {
                return (T)connection.ExecuteScalar(commandText, fieldValues);
            }
        }

        public static Dictionary<string, object> BuildIDParam(int id)
        {
            return new Dictionary<string, object>()
            {
                { "ID", id }
            };
        }

        public static Dictionary<string, object> BuildSingleParam(string name, object value)
        {
            return new Dictionary<string, object>()
            {
                { name , value }
            };
        }

        public void Execute<T>(string database, string sql, T data)
        {
            using (IDbConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString))
            {
                connection.Execute(sql, data);
            }
        }
    }
}
