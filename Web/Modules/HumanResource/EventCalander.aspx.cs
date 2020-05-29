using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using ERP.Utilities;


namespace ERPWeb
{
    public partial class EventCalander : System.Web.UI.Page
    {
 
       #region Methods

        [WebMethod]
        public static List<object> GetChartData()
        {
            
            string query = "Select a.ID, a.NameFirst, b.Abrivation, a.ManagerID ";
            query += " from Employees a, Positions b Where a.PositionsID = b.ID";
            //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    List<object> chartData = new List<object>();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new object[]
                            {
                         sdr["Id"], sdr["NameFirst"], sdr["Abrivation"] , sdr["ManagerID"]
                            });
                        }
                    }
                    con.Close();
                    return chartData;
                }
            }
        }

        #endregion
    }
}
