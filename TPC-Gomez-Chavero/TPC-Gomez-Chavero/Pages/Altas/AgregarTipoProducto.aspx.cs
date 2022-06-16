﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using services;
using domain;

namespace TPC_Gomez_Chavero.Pages.Altas
{
    public partial class AgregarTipoProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string descripcion = txtNombre.Text;
            ABMService abm = new ABMService();

            abm.createTypes(descripcion, "TipoProducto");
        }
    }
}