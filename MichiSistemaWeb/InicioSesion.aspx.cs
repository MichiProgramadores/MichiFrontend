using MichiSistemaWeb.MichiBackend;
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
        private usuario usuario;
        private TrabajadorWSClient trabajador;
        private UsuarioWSClient usuarioWSClient;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            usuario usuario = new usuario();
            string user = txtUsername.Text.Trim();
            string contrasena = txtPassword.Text.Trim();
            usuarioWSClient = new UsuarioWSClient();
            usuario.nombreUsuario = user;

            int resultado = usuarioWSClient.autenticarUsuario(user, contrasena);

            if (resultado != 0)
            {
                trabajador = new TrabajadorWSClient();
                trabajador empleado = trabajador.obtenerTrabajador(resultado);
                Session["empleado"] = empleado;
                FormsAuthenticationTicket tkt;
                string cookiestr;
                HttpCookie ck;

                tkt = new FormsAuthenticationTicket(1,
                usuario.nombreUsuario, DateTime.Now,
                DateTime.Now.AddMinutes(30), true, "datos adicionales");

                cookiestr = FormsAuthentication.Encrypt(tkt);
                ck = new HttpCookie(FormsAuthentication.FormsCookieName,
                    cookiestr);
                ck.Expires = tkt.Expiration;
                ck.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(ck);

                string strRedirect;
                strRedirect = Request["ReturnUrl"];
                if (strRedirect == null)
                    Response.Redirect("Home.aspx");
                Response.Redirect(strRedirect, true);
            }
            else
                lblMensaje.Text = "No ha ingresado correctamente su usuario/password";
        }
    }
}