using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto3.App_Code
{
    public class AppSettings
    {
        public static string conectionDB()
        {
            // String de conexão com o banco de dados ACCESS
            // https://www.connectionstrings.com

           return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/ADS2022T.accdb") + ";Persist Security Info=False;";
        }
    }
}