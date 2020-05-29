using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class CodeGenerateBR
    {
        #region Variables

        CodeGenerateDAL objCodeGenerateDAL = new CodeGenerateDAL();

        #endregion

        #region Methods

        public int GetAutoCodeGenerate(string CodeType)
        {
            return objCodeGenerateDAL.GetAutoCodeGenerate(CodeType);
        }       

        #endregion
    }
}
