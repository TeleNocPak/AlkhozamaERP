using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class CommonMethodsBR
    {
        public static bool RecordAlredayExist(int Id, string Name, string ColumnId, string ColumnName, string TableName)
        {
            return CommonMethodsDAL.RecordAlredayExist(Id, Name, ColumnId, ColumnName, TableName);
        }
    }
}
