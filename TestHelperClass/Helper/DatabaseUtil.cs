using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Admins_Transportation.helper
{
    public class DatabaseUtil
    {
        private readonly string connString;

        private readonly string providerName;

        /// N'"+instanceDataModel.summary>
        /// Creates an instance with the given connection string
        /// N'"+instanceDataModel./summary>
        /// N'"+instanceDataModel.param name="connString">The connection stringN'"+instanceDataModel./param>
        public DatabaseUtil()
        {
           this.connString = ConfigurationManager.ConnectionStrings["connectionStringSQL"].ConnectionString;
           this.providerName = "System.Data.SqlClient";
        }

        #region GetLicence

        public string GetLicence()
        {
            return providerName;
        }

        #endregion GetLicence

        /// <summary>
        /// Executes the gives SQL query
        /// </summary>
        /// <param name="query">The SQL query to be executed</param>
        /// <param name="Parameters">Query parameters and their values</param>
        /// <returns>Number of rows affected</returns>
        public int ExecuteNonQuery(string query, Dictionary<string, object> Parameters = null)
        {
            int x = ExecuteCommand(connString, providerName, query, Parameters);
            if (x <= 0)
                MethodesClass.ErrorLogAsync(query + MethodesClass.GetMethodName());
            return x;
        }
        public int ExecuteWithoutLogNonQuery(string query, Dictionary<string, object> Parameters = null)
        {
            int x = ExecuteCommand(connString, providerName, query, Parameters);
            return x;
        }

        /// <summary>
        /// Executes the given SQL query
        /// </summary>
        /// <param name="connString">Connection string to the database</param>
        /// <param name="providerName">Db provider invariant name</param>
        /// <param name="query">The SQL query to be executed</param>
        /// <param name="Parameters">Query parameters and their values</param>
        /// <returns>Number of rows affected</returns>
        public static int ExecuteCommand(string connString, string providerName, string query, Dictionary<string, object> Parameters = null)
        {
            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                //Create Query
                using (DbConnection conn = factory.CreateConnection())
                {
                    conn.ConnectionString = connString;
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        //Add Parameters
                        if (Parameters != null)
                        {
                            foreach (KeyValuePair<string, object> kvp in Parameters)
                            {
                                DbParameter parameter = factory.CreateParameter();
                                parameter.ParameterName = kvp.Key;
                                parameter.Value = kvp.Value;
                                cmd.Parameters.Add(parameter);
                            }
                        }
                        //Execute Query
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Query an SQL database
        /// </summary>
        /// <param name="query">Select query that returns a data table</param>
        /// <param name="Parameters">Query parameters with their values</param>
        /// <returns>Query results as a DataTable</returns>
        public DataTable GetanythingThroughQuery(string query, Dictionary<string, object> Parameters = null)
        {
            return Select(connString, providerName, query, Parameters);
        }

        /// <summary>
        /// Query an SQL database
        /// </summary>
        /// <param name="connString">Connection string to the database</param>
        /// <param name="providerName">DB provider invariant name</param>
        /// <param name="query">Select query that returns a data table</param>
        /// <param name="Parameters">Query parameters with their values</param>
        /// <returns>Query results as a DataTable</returns>
        public static DataTable Select(string connString, string providerName, string query, Dictionary<string, object> Parameters = null)
        {
            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
                DataTable dt = new DataTable();

                //Create Query
                using (DbConnection conn = factory.CreateConnection())
                {
                    conn.ConnectionString = connString;
                    using (DbCommand cmd = conn.CreateCommand())
                    using (DbDataAdapter da = factory.CreateDataAdapter())
                    {
                        cmd.CommandText = query;
                        da.SelectCommand = cmd;

                        //Add Parameters
                        if (Parameters != null)
                        {
                            foreach (KeyValuePair<string, object> kvp in Parameters)
                            {
                                DbParameter parameter = cmd.CreateParameter();
                                parameter.ParameterName = kvp.Key;
                                parameter.Value = kvp.Value;
                                cmd.Parameters.Add(parameter);
                            }
                        }

                        //Execute Query
                        conn.Open();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}
