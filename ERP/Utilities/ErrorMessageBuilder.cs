using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ERP.Utilities
{
    public class ErrorMessageBuilder
    {
        public ErrorMessageBuilder()
        {
        }
        public static string BuildMessage(string messageId)
        {
            DataSet ErrorMessageDataSet = new DataSet();
            string ErrorMessageFilePath = HttpContext.Current.Server.MapPath(@"~\xml\") + "ErrorMessages.xml";
            ErrorMessageDataSet.ReadXml(ErrorMessageFilePath);

            string whereClause = "Id = '" + messageId + "'";
            DataRow[] filterDataRow = ErrorMessageDataSet.Tables["Message"].Select(whereClause);

            if (filterDataRow.Count() > 0)
                return filterDataRow[0]["text"].ToString();
            else
                return "";
        }
    }
}
