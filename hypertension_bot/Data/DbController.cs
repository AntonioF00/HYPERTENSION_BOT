using Dapper;
using hypertension_bot.Settings;
using System.Data.Common;
using hypertension_bot.Loggers;
using Newtonsoft.Json;

namespace hypertension_bot.Data
{
    public class DbController
    {
        private readonly DbConnection _connection;
        public DbController()
        {
            _connection = ConnectionFactory.GetConnection(Setting.Istance.Configuration.ConnectionType, Setting.Istance.Configuration.ConnectionString);
        }
        public void InsertMeasures(int diastolic, int sistolic,int heartRate, int id)
        {
            try
            {
                if(_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                _connection.Query(Setting.Istance.Configuration.InsertMeasures, 
                new Dictionary<string, object>()
                {
                    ["diastolic"] = diastolic,
                    ["sistolic"]  = sistolic,
                    ["heartRate"] = heartRate,
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
        public void InsertUser(int id)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                _connection.Query(Setting.Istance.Configuration.InsertUser,
                new Dictionary<string, object>()
                {
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
        public string LastInsert(long id)
        {
            try
            {
                _connection.Open();

                string res = _connection.QueryFirstOrDefault<string>(
                Setting.Istance.Configuration.LastInsert,
                new Dictionary<string, object>()
                {
                    ["id"] = id
                });

                _connection.Close();

                return (string.IsNullOrEmpty(res))?new string("0"):res;

            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
                throw ex;
            }
        }
        public Dictionary<string,object> CalculateMonthAVG(long id)
        {
            try
            {
                _connection.Open();

                dynamic res = _connection.QueryFirstOrDefault<dynamic>(
                Setting.Istance.Configuration.CalculateMonthAVG,
                new Dictionary<string, object>()
                {
                    ["id"] = id
                });

                _connection.Close();

                if (res == null || res.Count == 0)
                {
                    return new Dictionary<string, object>();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(res);
                    return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                }
            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
                throw ex;
            }
        }
        public Dictionary<string, object> CalculateWeekAVG(long id)
        {
            try
            {
                _connection.Open();

                dynamic res = _connection.QueryFirstOrDefault<dynamic>(
                Setting.Istance.Configuration.CalculateWeekAVG,
                new Dictionary<string, object>()
                {
                    ["id"] = id
                });

                _connection.Close();

                if (res == null || res.Count == 0)
                {
                    return new Dictionary<string, object>();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(res);
                    return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                }
            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
                throw ex;
            }
        }
        public Dictionary<string, object> CalculateDayAVG(long id)
        {
            try
            {
                _connection.Open();

                dynamic res = _connection.QueryFirstOrDefault<dynamic>(
                Setting.Istance.Configuration.CalculateDayAVG,
                new Dictionary<string, object>()
                {
                    ["id"] = id
                });

                _connection.Close();

                if (res == null || res.Count == 0)
                {
                    return new Dictionary<string, object>();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(res);
                    return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                }
            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
                throw ex;
            }
        }
        public List<Dictionary<string, object>> getMeasurementList(long id)
        {
            try
            {
                _connection.Open();

                dynamic res = _connection.Query<dynamic>(
                Setting.Istance.Configuration.MeasurementList,
                new Dictionary<string, object>()
                {
                    ["id"] = id
                });

                _connection.Close();

                if (res == null || res.Count == 0)
                {
                    return new List<Dictionary<string, object>>();
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(res));
                }
            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
                throw ex;
            }
        }
        public List<Dictionary<string, object>> getMeasurementDayList(long id)
        {
            try
            {
                _connection.Open();

                dynamic res = _connection.Query<dynamic>(
                Setting.Istance.Configuration.MeasurementDayList,
                new Dictionary<string, object>()
                {
                    ["id"] = id
                });

                _connection.Close();

                if (res == null || res.Count == 0)
                {
                    return new List<Dictionary<string, object>>();
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(res));
                }
            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
                throw ex;
            }
        }
        public List<Dictionary<string, object>> getMeasurementMonthList(long id)
        {
            try
            {
                _connection.Open();

                dynamic res = _connection.Query<dynamic>(
                Setting.Istance.Configuration.MeasurementMonthList,
                new Dictionary<string, object>()
                {
                    ["id"] = id
                });

                _connection.Close();

                if (res == null || res.Count == 0)
                {
                    return new List<Dictionary<string, object>>();
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(res));
                }
            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
                throw ex;
            }
        }
        public List<Dictionary<string, object>> getMeasurementWeekList(long id)
        {
            try
            {
                _connection.Open();

                dynamic res = _connection.Query<dynamic>(
                Setting.Istance.Configuration.MeasurementWeekList,
                new Dictionary<string, object>()
                {
                    ["id"] = id
                });

                _connection.Close();

                if (res == null || res.Count == 0)
                {
                    return new List<Dictionary<string, object>>();
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(res));
                }
            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
                throw ex;
            }
        }
        public bool DeleteMeasurement(long id, int idMisurazione)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                var r = _connection.QueryFirstOrDefault<bool>(Setting.Istance.Configuration.DeleteMeasurement,
                new Dictionary<string, object>()
                {
                    ["id"] = id,
                    ["idMisurazione"] = idMisurazione
                });

                _connection.Close();

                return r; 
            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();

                throw ex;
                 
                LogHelper.Log(ex.Message);
            }
        }
        public bool GetFirstAlert(long id)
        {
            try
            {
                _connection.Open();

                var res = _connection.QueryFirstOrDefault<bool>(
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
