using System;
using System.Data;
using System.Data.SqlClient;

namespace ERP.Utilities
{
    public class ERPDBConnection
    {
        private static string ConnStr = System.Configuration.ConfigurationManager.AppSettings.Get("ConStr").ToString();
        public static SqlConnection SQLCon;

        #region Constructor

        public ERPDBConnection()
        { }

        #endregion

        #region Public Functions

        public static SqlConnection GetConnection()
        {
            try
            {
                if (SQLCon != null)
                {
                    SQLCon.Dispose();
                    SQLCon = null;
                    SQLCon = null;
                }

                SQLCon = new SqlConnection(ConnStr);
                SQLCon.Open();
                return SQLCon;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }
        }

        public static void CloseConnection()
        {
            try
            {
                if (SQLCon != null)
                {
                    if (SQLCon.State != ConnectionState.Closed)
                    {
                        SQLCon.Close();
                        SQLCon.Dispose();
                    }
                    SQLCon = null;
                    SQLCon = null;
                    SQLCon = null;
                    SQLCon = null;
                    SQLCon = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        #endregion
    }
}