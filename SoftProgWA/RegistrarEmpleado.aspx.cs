using SoftProgBusiness.RRHH;
using SoftProgDomain.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftProgWA
{
    public partial class RegistrarEmpleado : System.Web.UI.Page
    {
        private AreaBO boArea;
        private EmpleadoBO boEmpleado;
        private Empleado empleado;
        private Estado estado;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                boArea = new AreaBO();
                ddlAreas.DataSource = boArea.listarTodos();
                ddlAreas.DataTextField = "Nombre";
                ddlAreas.DataValueField = "IdArea";
                ddlAreas.DataBind();
            }

            string accion = Request.QueryString["accion"];
            if (accion == null)
            {
                estado = Estado.Nuevo;
                empleado = new Empleado();
                lblTitulo.Text = "Registrar Empleado";
            }
            else if (accion == "modificar")
            {
                estado = Estado.Modificar;
                lblTitulo.Text = "Modificar Empleado";
                empleado = (Empleado)Session["empleadoSeleccionado"];
                if (!IsPostBack)
                {
                    AsignarValores();
                }
            }
            else if (accion == "ver")
            {
                lblTitulo.Text = "Ver Empleado";
                empleado = (Empleado)Session["empleadoSeleccionado"];
                AsignarValores();
                txtDNIEmpleado.Enabled = false;
                txtNombre.Enabled = false;
                txtApellidoPaterno.Enabled = false;
                txtCargo.Enabled = false;
                txtSueldo.Enabled = false;
                ddlAreas.Enabled = false;
                rbMasculino.Disabled = true;
                rbFemenino.Disabled = true;
                btnGuardar.Visible = false;
                dtpFechaNacimiento.Disabled = true;
            }

        }

        protected void AsignarValores()
        {
            txtDNIEmpleado.Text = empleado.DNI;
            txtNombre.Text = empleado.Nombre;
            txtApellidoPaterno.Text = empleado.ApellidoPaterno;
            txtCargo.Text = empleado.Cargo;
            txtSueldo.Text = empleado.Sueldo.ToString("F2");
            ddlAreas.SelectedValue = empleado.Area.IdArea.ToString();
            if (empleado.Genero.Equals('M')) rbMasculino.Checked = true;
            else rbFemenino.Checked = true;
            dtpFechaNacimiento.Value = empleado.FechaNacimiento.ToString("yyyy-MM-dd");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            boEmpleado = new EmpleadoBO();            
            empleado.DNI = txtDNIEmpleado.Text;
            empleado.Nombre = txtNombre.Text;
            empleado.ApellidoPaterno = txtApellidoPaterno.Text;
            
            try
            {
                empleado.FechaNacimiento = DateTime.Parse(dtpFechaNacimiento.Value);
            }
            catch (Exception ex)
                { lanzarMensajedeError("Debe seleccionar una fecha de nacimiento"); return; }

            
                if (rbMasculino.Checked) empleado.Genero = 'M';
                else if (rbFemenino.Checked) empleado.Genero = 'F';
            try
            {
                empleado.Sueldo = Double.Parse(txtSueldo.Text);
            }catch(Exception ex)
                { lanzarMensajedeError("Debe colocar un valor de sueldo apropiado"); return;}

            empleado.Cargo = txtCargo.Text;
            Area area = new Area();
            area.IdArea = Int32.Parse(ddlAreas.SelectedValue);
            empleado.Area = area;

            try
            {
                if (estado == Estado.Nuevo)
                {
                    boEmpleado.insertar(empleado);
                }
                else if (estado == Estado.Modificar)
                {
                    boEmpleado.modificar(empleado);
                }
            }
            catch (Exception ex)
                {lanzarMensajedeError(ex.Message); return; }

            Response.Redirect("ListarEmpleados.aspx");
        }

        public void lanzarMensajedeError(String mensaje)
        {
            lblMensajeError.Text = mensaje;
            string script = "mostrarModalError();";
            ScriptManager.RegisterStartupScript(this, GetType(), "modalError", script, true);
        }

        public void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarEmpleados.aspx");
        }
    }
}