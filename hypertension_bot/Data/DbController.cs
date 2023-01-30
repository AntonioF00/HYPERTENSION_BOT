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

        public void InsertMeasures(int diastolic, int sistolic, long id)
        {
            try
            {
                if(_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                _connection.Query(Setting.Istance.Configuration.InsertMeasures, 
                new Dictionary<string, object>()
                {
                    ["diastolic"] = diastolic,
                    ["sistolic"] = sistolic,
                    ["id"] = id
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

        public void InsertUser(long id)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                _connection.Query(Setting.Istance.Configuration.InsertUser,
                new Dictionary<string, object>()
                {
                    ["id"] = id,
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

        public string LastInsert(long id)
        {
            try
            {
                _connection.Open();

                string res = _connection.QueryFirst<string>(
                Setting.Istance.Configuration.LastInsert,
                new Dictionary<string, object>()
                {
                    ["id"] = id
                });

                _connection.Close();

                return res;

            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
                throw ex;
            }
        }

    }
}
