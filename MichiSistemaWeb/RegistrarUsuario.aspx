﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="RegistrarUsuario.aspx.cs" Inherits="MichiSistemaWeb.RegistrarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Registrar usuario
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <script src="Scripts/michi/registrarUsuario.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
        <div class="card">
            <div class="card-header">
                <h2>
                    <asp:Label ID="lblTitulo" runat="server" Text="Registrar usuario"></asp:Label>
                </h2>
            </div>
            <div class="card-body">
               
                    <div class="mb-3 row">
                        <asp:Label id="lblID" runat="server" CssClass="col-sm-2 col-form-label" Text="*ID: "></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox id="txtIDTrabajador" runat="server" CssClass="form-control"
                                data-bs-toggle="tooltip" title="Identificador del usuario (Mismo identificador que el trabajador asociado)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="mb-3 row">
                        <asp:Label id="lblUser" runat="server" Text=" Nombre usuario: " CssClass="col-sm-2 col-form-label"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox id="txtNombre" runat="server" CssClass="form-control" 
                                data-bs-toggle="tooltip" title="Nombre con el que el usuario inicia sesión"></asp:TextBox>
                        </div>
                    </div>
                    <div class="mb-3 row">
                        <asp:Label id="lblContra" runat="server" Text="*Contraseña: " CssClass="col-sm-2 col-form-label"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox id="txtContra" runat="server" CssClass="form-control" 
                                data-bs-toggle="tooltip" title="Contraseña (Solo el administrador puede ver este campo)"></asp:TextBox>
                        </div>
                    </div>
               
            </div>
            <div class="card-footer clearfix">
                <asp:LinkButton ID="btnRegresar" runat="server" Text="<i class='fa-solid fa-rotate-left'></i> Regresar" CssClass="float-start btn btn-secondary" OnClick="btnRegresar_Click"/>
                <asp:LinkButton ID="btnGuardar" CssClass="float-end btn btn-primary" runat="server" Text="<i class='fa-solid fa-floppy-disk pe-2'></i> Guardar" OnClick="btnGuardar_Click" style="background-color:  #FF7E5F; border: none;" />
            </div>
        </div>
    </div>
    <div class="modal fade" id="errorModal" tabindex="-1" aria-labelledby="errorModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title" id="errorModalLabel">
                        <i class="bi bi-exclamation-triangle-fill me-2"></i>Mensaje de Error
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblMensajeError" runat="server" Text="Mensaje de ejemplo..." CssClass="form-text text-danger"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>