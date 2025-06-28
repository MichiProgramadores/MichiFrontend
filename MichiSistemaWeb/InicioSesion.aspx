<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="MichiSistemaWeb.InicioSesion" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/Fonts/css/all.css" rel="stylesheet" />
    <link href="Content/site.css" rel="stylesheet" />

    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/bootstrap.bundle.js"></script>
    <script src="Scripts/jquery-3.7.1.js"></script>
    
    <script src="Scripts/michi/iniciarSesion.js"></script>
    <script>
    function togglePasswordVisibility() {
        var passwordField = document.getElementById('<%= txtPassword.ClientID %>');
        var eyeIcon = document.getElementById('eyeIcon');
        
        // Si la contraseña está oculta, la hacemos visible
        if (passwordField.type === "password") {
            passwordField.type = "text";
            eyeIcon.classList.remove('fa-eye-slash');
            eyeIcon.classList.add('fa-eye');
        } else {
            passwordField.type = "password";
            eyeIcon.classList.remove('fa-eye');
            eyeIcon.classList.add('fa-eye-slash');
        }
    }
    </script>
    <title>Inicio de Sesion</title>
    
    <style>
        body {
            background-image: url('Images/logo_act.png');
            background-size: contain; /* Ajusta la imagen para que se vea completa */
            background-position: center;
            background-repeat: no-repeat;
            background-color: #F4F4F4;
           // background-attachment: fixed;
        }
    </style>
</head>
<body>
    <div class="container d-flex justify-content-center align-items-center" style="height: 100vh;">
        <div class="card p-4" style="width: 100%; max-width: 400px;">
            <h3 class="card-title text-center mb-4">Inicio de Sesión</h3>
            <form id="formLogin" runat="server">
                <div class="form-group">
                    <label for="txtUsername">Usuario</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Ingrese su usuario"></asp:TextBox>
                </div>
                <div class="form-group mt-3">
                    <label for="txtPassword">Contraseña</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese su contraseña"></asp:TextBox>
                        <span class="input-group-text" id="eyeIcon" onclick="togglePasswordVisibility()" style="cursor: pointer;" data-bs-toggle="tooltip" title="Visualizar contraseña">
                            <i class="fas fa-eye-slash"></i>
                        </span>
                    </div>
                </div>


                <div class="form-group mt-2">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger text-center d-block" EnableViewState="false"></asp:Label>
                </div>
                <div class="text-center mt-3" >
                    <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" style="background-color: #FBCB43; border: none; color: #000000;"/>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
