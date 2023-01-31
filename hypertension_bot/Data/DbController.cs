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

        public List<object> CalculateMonthAVG(long id)
        {
            try
            {
                _connection.Open();

                List<object> res = _connection.QueryFirstOrDefault<List<object>>(
                Setting.Istance.Configuration.CalculateMonthAVG,
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

        public List<object> CalculateWeekAVG(long id)
        {
            try
            {
                _connection.Open();

                List<object> res = _connection.QueryFirstOrDefault<List<object>>(
                Setting.Istance.Configuration.CalculateWeekAVG,
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

        public bool GetFirstAlert(long id)
        {
            try
            {
                _connection.Open();

                var res = _connection.QueryFirst<bool>(
                Setting.Istance.Configuration.GetFirstAlert,
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

        public void UpdateFirstAlert(long id, bool b)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                _connection.Query(Setting.Istance.Configuration.UpdateFirstAlert,
                new Dictionary<string, object>()
                {
                    ["id"] = id,
                    ["val"] = b
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
