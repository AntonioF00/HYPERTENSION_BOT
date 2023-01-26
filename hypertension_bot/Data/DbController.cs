using Dapper;
using hypertension_bot.Settings;
using System.Data.Common;
using hypertension_bot.Loggers;

namespace hypertension_bot.Data
{
    public class DbController
    {
        private readonly DbConnection _connection;

        public DbController()
        {
            _connection = ConnectionFactory.GetConnection(Setting.Istance.Configuration.ConnectionType, Setting.Istance.Configuration.ConnectionString);
        }

        /// <summary>
        /// Metodo generale per eseguire una query,
        /// nel progetto saranno query di insert quindi
        /// query il cui ritorno è void
        /// </summary>
        /// <param name="query">stringa contenente la query definita nel configuration.xml</param>
        public void execute(string query, string diastolic, string sistolic)
        {
            try
            {
                if(_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                _connection.Query(query, 
                new Dictionary<string, object>()
                {
                    ["diastolic"] = diastolic,
                    ["sistolic"] = sistolic
                });

                _connection.Close();
            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();

                LogHelper.Log(ex.Message);
            }
        }
    }
}
