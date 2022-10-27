﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto3
{
    public partial class Page : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["autenticado"] != null)
            {
                Excecoes.Visible = true;
                representantes.Visible = true;
                Login.Visible = false;
                Logout.Visible = true;
            }
            else
            {
                Excecoes.Visible = false;
                representantes.Visible = false;
                Login.Visible = true;
                Logout.Visible = false;
            }
            
        }
    }
}