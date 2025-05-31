using SoftProgBusiness.RRHH;
using SoftProgDomain.RRHH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftProgWA
{
    public partial class ListarAreas : System.Web.UI.Page
    {
        private AreaBO boArea;
        private BindingList<Area> areas;
        protected void Page_Load(object sender, EventArgs e)
        {
            boArea = new AreaBO();
            areas = boArea.listarTodos();
            gvAreas.DataSource = areas;
            gvAreas.DataBind();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarArea.aspx");
        }

        protected void gvAreas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAreas.PageIndex = e.NewPageIndex;
            gvAreas.DataBind();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            int idArea = Int32.Parse(((LinkButton)sender).CommandArgument);
            Area areaSeleccionada = areas.SingleOrDefault(x => x.IdArea == idArea);
            Session["areaSeleccionada"] = areaSeleccionada;
            Response.Redirect("RegistrarArea.aspx?accion=modificar");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idArea = Int32.Parse(((LinkButton)sender).CommandArgument);
            boArea.eliminar(idArea);
            Response.Redirect("ListarAreas.aspx");
        }
    }
}