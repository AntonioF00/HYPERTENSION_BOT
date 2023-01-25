using Microsoft.Data.SqlClient;
using Npgsql;
using System;
using System.Data.Common;


namespace hypertension_bot.Data
{
    public class ConnectionFactory
    {
        public enum ConnectionTypes
        {
            POSTGRESQL = 0,
            SQLSERVER = 1,
            ODBC = 2
        }

        public static DbConnection GetConnection(ConnectionTypes type, String connectionString)
        {

            if (type == ConnectionTypes.POSTGRESQL)
            {
                return new NpgsqlConnection(connectionString);
            }
            else if (type == ConnectionTypes.SQLSERVER)
            {
                return new SqlConnection(connectionString);
            }
            else
            {
                return null;
            }
        }

        public static DbCommand GetCommandCom(string query, DbConnection conn)
        {
            DbCommand cmd;
            if (conn is NpgsqlConnection nc)
            {
                cmd = new NpgsqlCommand(query, nc);
                return cmd;
            }
            else if (conn is SqlConnection sc)
            {
                cmd = new SqlCommand(query, sc);
                return cmd;
            }
            else
            {
                return null;
            }
        }

        public static int CloseConnection(DbConnection conn)
        {
            conn.Close();
            return (0);
        }
    }
}
