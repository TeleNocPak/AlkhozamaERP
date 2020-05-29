using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using ERP.Utilities;

namespace ERP.DAL
{
    public class DataOperations
    {
        private string SqlConnectionString = ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ToString();

        public int ExecuteStatement(SqlCommand pObjCommand)
        {
            SqlConnection objConnection = new SqlConnection();
            try
            {
                objConnection.ConnectionString = SqlConnectionString;
                objConnection.Open();

                pObjCommand.Connection = objConnection;
                return pObjCommand.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                objConnection.Close();
                objConnection.Dispose();
            }
        }

        public Int64 ExecuteStatementWithIdentity(SqlCommand pObjCommand)
        {
            SqlConnection objConnection = new SqlConnection();

            try
            {
                objConnection.ConnectionString = SqlConnectionString;
                objConnection.Open();

                pObjCommand.Connection = objConnection;

                var value = pObjCommand.ExecuteScalar();
                return (value == null ? 0 : Convert.ToInt64(value));
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                objConnection.Close();
                objConnection.Dispose();
            }
        }

        public object GetScalarData(SqlCommand pObjCommand)
        {
            SqlConnection objConnection = new SqlConnection();

            try
            {
                objConnection.ConnectionString = SqlConnectionString;
                objConnection.Open();

                pObjCommand.Connection = objConnection;
                return pObjCommand.ExecuteScalar();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                objConnection.Close();
            }
        }

        public DataTable GetData(SqlCommand pObjCommand)
        {
            SqlConnection objConnection = new SqlConnection();
            try
            {
                objConnection.ConnectionString = SqlConnectionString;
                objConnection.Open();

                pObjCommand.Connection = objConnection;

                DataTable dataTable = new DataTable();
                SqlDataAdapter objAdapter = new SqlDataAdapter(pObjCommand);
                objAdapter.Fill(dataTable);
                objAdapter.Dispose();
                return dataTable;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                objConnection.Close();
                objConnection.Dispose();
            }
        }

        public bool InsertBulkData(DataTable currentTable)
        {
            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.ColumnMappings.Add("ProjectId", "ProjectId");
                    bulkCopy.ColumnMappings.Add("PageName", "PageName");
                    bulkCopy.DestinationTableName = "csProjectPages";
                    try
                    {
                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(currentTable);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
    }
}