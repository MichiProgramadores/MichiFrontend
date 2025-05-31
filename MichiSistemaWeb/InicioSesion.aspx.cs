using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class InicioSesion : System.Web.UI.Page
    {
        //private CuentaUsuarioBO boCuentaUsuario;
        //private EmpleadoBO boEmpleado;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //lblMensaje.Text = "";
            //CuentaUsuario cuentaUsuario = new CuentaUsuario();
            //cuentaUsuario.Username = txtUsername.Text;
            //cuentaUsuario.Password = txtPassword.Text;
            //boCuentaUsuario = new CuentaUsuarioBO();

            //int resultado = boCuentaUsuario.verificarCuentaUsuario(cuentaUsuario);

            //if (resultado != 0)
            //{
            //    boEmpleado = new EmpleadoBO();
            //    Empleado empleado = boEmpleado.obtenerEmpleadoPorId(resultado);
            //    Session["empleado"] = empleado;
            //    FormsAuthenticationTicket tkt;
            //    string cookiestr;
            //    HttpCookie ck;

            //    tkt = new FormsAuthenticationTicket(1,
            //    cuentaUsuario.Username, DateTime.Now,
            //    DateTime.Now.AddMinutes(30), true, "datos adicionales");

            //    cookiestr = FormsAuthentication.Encrypt(tkt);
            //    ck = new HttpCookie(FormsAuthentication.FormsCookieName,
            //        cookiestr);
            //    ck.Expires = tkt.Expiration;
            //    ck.Path = FormsAuthentication.FormsCookiePath;
            //    Response.Cookies.Add(ck);

            //    string strRedirect;
            //    strRedirect = Request["ReturnUrl"];
            //    if (strRedirect == null)
            //        Response.Redirect("Home.aspx");
            //    Response.Redirect(strRedirect, true);
            //}
            //else
            //    lblMensaje.Text = "No ha ingresado correctamente su usuario/password";
        }
    }
}