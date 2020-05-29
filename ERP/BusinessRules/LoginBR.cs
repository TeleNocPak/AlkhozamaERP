using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class LoginBR
    {
        #region Variables

        LoginDAL objLoginDAL = new LoginDAL();

        #endregion

        #region Methods

        public DataTable GetLogin(string UserName, string Password)
        {
            return objLoginDAL.GetLogin(UserName, Password);
        }

        public DataTable GetLocations(string LocationId)
        {
            return objLoginDAL.GetLocations(LocationId);
        }

        public Boolean PasswordConfirmation(int UserId, string Password)
        {
            return objLoginDAL.PasswordConfirmation(UserId, Password);
        }

        public void UpdateLoginPassword(int UserId, string NewPassword)
        {
            objLoginDAL.UpdateLoginPassword(UserId, NewPassword);
        }

        #endregion
    }
}

