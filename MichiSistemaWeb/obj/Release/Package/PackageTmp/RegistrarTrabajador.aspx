<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="RegistrarTrabajador.aspx.cs" Inherits="MichiSistemaWeb.RegistrarTrabajador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Registrar Trabajador
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <script src="Scripts/michi/registrarTrabajador.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
        <div class="card">
            <div class="card-header">
                <h2>
                    <asp:Label ID="lblTitulo" runat="server" Text="Registrar Trabajador"></asp:Label>
                </h2>
            </div>
            <div class="card-body">
                
                <div class="mb-3 row">
                    <asp:Label id="lblID" runat="server" CssClass="col-sm-2 col-form-label" Text="ID: "></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtIDTrabajador" runat="server" CssClass="form-control"
                            data-bs-toggle="tooltip" title="Identificador del trabajador"></asp:TextBox>
                    </div>
                </div>
                

                 <div class="mb-3 row">
                     <asp:Label ID="lblNombres" runat="server" Text="*Nombres: " CssClass="col-sm-2 form-label"></asp:Label>
                     <div class="col-sm-8">
                          <asp:TextBox id="txtNombres" runat="server" CssClass="form-control"
                              data-bs-toggle="tooltip" title="Nombre/s del trabajador"></asp:TextBox>
                     </div>
                 </div>

                <div class="mb-3 row">
                    <asp:Label id="lblApellidos" runat="server" Text="*Apellidos: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtApellidos" runat="server" CssClass="form-control" 
                            data-bs-toggle="tooltip" title="Apellidos del trabajador"></asp:TextBox>
                    </div>
                </div>


                <div class="mb-3 row">
                    <asp:Label id="lblCelular" CssClass="col-sm-2 form-label" runat="server" Text="*Celular:"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtCelular" runat="server" CssClass="form-control"
                            data-bs-toggle="tooltip" title="Número telefónico del trabajador"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblEmail" runat="server" Text="*Email: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtEmail" runat="server" CssClass="form-control" 
                            data-bs-toggle="tooltip" title="Correo electrónico del trabajador"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label ID="lblTipoTrabajador" runat="server" Text="*Tipo de Trabajador:" CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="ddlTipoTrabajador" runat="server" CssClass="form-select" 
                            data-bs-toggle="tooltip" title="Indica la categoría del trabajador">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="mb-3 row">
                    <asp:Label id="lblActivo" runat="server" Text="Activo: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtActivo" runat="server" CssClass="form-control" 
                            data-bs-toggle="tooltip" title="Indica el estado actual del trabajador"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="card-footer clearfix">
                <asp:LinkButton ID="btnRegresar" runat="server" Text="<i class='fa-solid fa-rotate-left'></i> Regresar" CssClass="float-start btn btn-secondary" OnClick="btnRegresar_Click"/>
                <asp:LinkButton ID="btnGuardar" CssClass="float-end btn btn-primary" runat="server" Text="<i class='fa-solid fa-floppy-disk pe-2'></i> Guardar" OnClick="btnGuardar_Click" style="background-color:  #FF7E5F; border: none;"/>
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

