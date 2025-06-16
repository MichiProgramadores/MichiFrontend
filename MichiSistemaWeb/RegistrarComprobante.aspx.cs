using MichiSistemaWeb.MichiBackend;
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
        protected ComprobanteWSClient comprobanteService;
        protected comprobante comprobante;
        protected Estado estado;
        protected void Page_Init(object sender, EventArgs e)
        {
            comprobanteService = new ComprobanteWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // Obtener los valores del enum tipoCliente
                //ddl.DataSource = Enum.GetValues(typeof(tipoCliente));
                //ddlTipoCliente.DataBind();

                // Opcional: agregar un valor por defecto
                //ddlTipoCliente.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }

            //string accion = Request.QueryString["accion"];
            //if (accion == null)
            //{
            //    estado = Estado.Nuevo;
            //    orden = new orden();
            //    lblTitulo.Text = "Registrar Cliente";

            //    lblID.Visible = false;
            //    txtIDCliente.Visible = false;

            //    lblPuntuacion.Visible = false;
            //    txtPuntuacion.Visible = false;

            //    lblActivo.Visible = false;
            //    txtActivo.Visible = false;

            //}
            //else if (accion == "modificar")
            //{
            //    estado = Estado.Modificar;
            //    lblTitulo.Text = "Modificar Cliente";
            //    cliente = (cliente)Session["clienteSeleccionado"];
            //    if (!IsPostBack)
            //    {
            //        AsignarValoresTexto();
            //    }
            //    lblID.Visible = true;
            //    txtIDCliente.Visible = true;
            //    txtIDCliente.Enabled = false;

            //    lblPuntuacion.Visible = true;
            //    txtPuntuacion.Visible = true;
            //    txtPuntuacion.Enabled = false;

            //    lblActivo.Visible = false;
            //    txtActivo.Visible = false;
            //}
            //else if (accion == "ver")
            //{
            //    lblTitulo.Text = "Ver Cliente";
            //    cliente = (cliente)Session["clienteSeleccionado"];
            //    AsignarValoresTexto();

            //    lblID.Visible = true;
            //    txtIDCliente.Visible = true;
            //    txtIDCliente.Enabled = false;

            //    lblPuntuacion.Visible = true;
            //    txtPuntuacion.Visible = true;
            //    txtPuntuacion.Enabled = false;

            //    lblActivo.Visible = true;
            //    txtActivo.Visible = true;
            //    txtActivo.Enabled = false;

            //    txtNombres.Enabled = false;
            //    txtApellidos.Enabled = false;
            //    txtCelular.Enabled = false;
            //    txtEmail.Enabled = false;
            //    txtNumeroTipoCliente.Enabled = false;

            //    ddlTipoCliente.Enabled = false;

            //    btnGuardar.Visible = false;
            //}
        }

        protected void AsignarValores()
        {
            //txtDNIEmpleado.Text = empleado.DNI;
            //txtNombre.Text = empleado.Nombre;
            //txtApellidoPaterno.Text = empleado.ApellidoPaterno;
            //txtCargo.Text = empleado.Cargo;
            //txtSueldo.Text = empleado.Sueldo.ToString("F2");
            //ddlAreas.SelectedValue = empleado.Area.IdArea.ToString();
            //if (empleado.Genero.Equals('M')) rbMasculino.Checked = true;
            //else rbFemenino.Checked = true;
            //dtpFechaNacimiento.Value = empleado.FechaNacimiento.ToString("yyyy-MM-dd");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // boEmpleado = new EmpleadoBO();
            //empleado.DNI = txtDNIEmpleado.Text;
            //empleado.Nombre = txtNombre.Text;
            //empleado.ApellidoPaterno = txtApellidoPaterno.Text;

            //try
            //{
            //    empleado.FechaNacimiento = DateTime.Parse(dtpFechaNacimiento.Value);
            //}
            //catch (Exception ex)
            //{ lanzarMensajedeError("Debe seleccionar una fecha de nacimiento"); return; }


            //if (rbMasculino.Checked) empleado.Genero = 'M';
            //else if (rbFemenino.Checked) empleado.Genero = 'F';
            //try
            //{
            //    empleado.Sueldo = Double.Parse(txtSueldo.Text);
            //}
            //catch (Exception ex)
            //{ lanzarMensajedeError("Debe colocar un valor de sueldo apropiado"); return; }

            //empleado.Cargo = txtCargo.Text;
            //Area area = new Area();
            //area.IdArea = Int32.Parse(ddlAreas.SelectedValue);
            //empleado.Area = area;

            //try
            //{
            //    if (estado == Estado.Nuevo)
            //    {
            //        boEmpleado.insertar(empleado);
            //    }
            //    else if (estado == Estado.Modificar)
            //    {
            //        boEmpleado.modificar(empleado);
            //   }
            //}
            //catch (Exception ex)
            //{ lanzarMensajedeError(ex.Message); return; }

            //Response.Redirect("ListarTrabajadores.aspx");
        }

        public void lanzarMensajedeError(String mensaje)
        {
            lblMensajeError.Text = mensaje;
            string script = "mostrarModalError();";
            ScriptManager.RegisterStartupScript(this, GetType(), "modalError", script, true);
        }

        public void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarComprobantes.aspx");
        }

    }
}