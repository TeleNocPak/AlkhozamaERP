using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ERP.Utilities
{
     public class ERPApplicationHelper
    {
        private string strSQLP;
        private string strSQLC;

        public DataSet GetDataSetForMenu()
        {
            strSQLP = "SELECT * FROM Links where SubId=0 and active = 1 order by ID";
            strSQLC = "SELECT * FROM Links where SubId<>0 and active = 1 order by name";

            SqlDataAdapter adPartent = new SqlDataAdapter(strSQLP, System.Configuration.ConfigurationManager.AppSettings.Get("ConStr").ToString());
            SqlDataAdapter adChild = new SqlDataAdapter(strSQLC, System.Configuration.ConfigurationManager.AppSettings.Get("ConStr").ToString());

            DataSet ds = new DataSet();
            adPartent.Fill(ds, "Parent");
            adChild.Fill(ds, "Child");
            ds.Relations.Add("Children", ds.Tables["Parent"].Columns["ID"], ds.Tables["Child"].Columns["SubId"]);
            return ds;
        }
    }
}
