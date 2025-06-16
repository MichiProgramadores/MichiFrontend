using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class RegistrarComprobante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }





        public void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarComprobantes.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}