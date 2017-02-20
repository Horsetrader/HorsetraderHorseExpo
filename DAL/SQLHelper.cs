using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SQLHelper
    {
        private const int SQL_NO_CONNECTION = -1;
        private const int SQL_SP_NOT_FOUND = 2812;
        private const int SQL_NO_DATABASE_FOUND = 4060;
        private const int SQL_INVALID_LOGIN = 18456;

        private SqlConnection _conn;
        private SqlCommand _cmd;

        #region Properties
        public string ConnectionString
        {
            get { return _conn.ConnectionString; }
        }

        public SqlCommand Command
        {
            get { return _cmd; }
        }
        #endregion

        #region Constructors
        public SQLHelper(string connString)
        {
            _conn = new SqlConnection(connString);
            _cmd = new SqlCommand();
            _cmd.Connection = _conn;
        }
        #endregion

        #region Public Methods
        public DataSet Execute(string query, CommandType type)
        {
            DataSet dsResults = new DataSet();
            try
            {
                _cmd.CommandType = type;
                _cmd.CommandText = query;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = _cmd;

                _conn.Open();
                adapter.Fill(dsResults);
            }
            catch (SqlException ex)
            {
                processError(ex);
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
            return dsResults;
        }

        public void ExecuteNonQuery(string query, CommandType type)
        {
            try
            {
                _cmd.CommandType = type;
                _cmd.CommandText = query;
                _conn.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                processError(ex);
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }
        #endregion

        #region Private Methods
        private static void processError(SqlException ex)
        {
            switch (ex.Number)
            {
                case SQL_NO_CONNECTION: throw new SQLError("Did not find SQL server from connection string.");
                case SQL_NO_DATABASE_FOUND: throw new SQLError("Did not find the database.");
                case SQL_INVALID_LOGIN: throw new SQLError("Login failed.");
                case SQL_SP_NOT_FOUND: throw new SQLError("Did not find SP.");
                default: throw new SQLError("Unknown sql error: " + ex.Message);
            }
        }

        internal void AddParameter(string parameterName, int value)
        {
            SqlParameter param = new SqlParameter(parameterName, SqlDbType.Int);
            param.Value = value;
            _cmd.Parameters.Add(param);
        }

        internal void AddParameter(string parameterName, int? value)
        {
            SqlParameter param = new SqlParameter(parameterName, SqlDbType.Int);
            if (value == null)
                param.Value = DBNull.Value;
            else
                param.Value = value;

            _cmd.Parameters.Add(param);
        }

        internal void AddParameter(string parameterName, int value, ParameterDirection parameterDirection)
        {
            SqlParameter param = new SqlParameter(parameterName, SqlDbType.Int);
            param.Direction = parameterDirection;
            param.Value = value;
            _cmd.Parameters.Add(param);
        }

        internal void AddParameter(string parameterName, string value)
        {
            SqlParameter param = new SqlParameter(parameterName, SqlDbType.VarChar);

            if (string.IsNullOrEmpty(value))
                param.Value = DBNull.Value;
            else
                param.Value = value;

            _cmd.Parameters.Add(param);
        }

        internal void AddParameter(string parameterName, DateTime value)
        {
            SqlParameter param = new SqlParameter(parameterName, SqlDbType.DateTime);
            param.Value = value;
            _cmd.Parameters.Add(param);
        }

        internal void AddParameter(string parameterName, bool value)
        {
            SqlParameter param = new SqlParameter(parameterName, SqlDbType.Bit);
            param.Value = value;
            _cmd.Parameters.Add(param);
        }
        #endregion
    }
}
