<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="RegistrarProducto.aspx.cs" Inherits="MichiSistemaWeb.RegistrarProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Registrar Producto
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <script src="Scripts/michi/registrarProducto.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
        <div class="card">
            <div class="card-header">
                <h2>
                    <asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label>
                </h2>
            </div>
            <div class="card-body">

                <div class="mb-3 row">
                    <asp:Label id="lblID" runat="server" CssClass="col-sm-2 col-form-label" Text="ID: "></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtIDProducto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                 <div class="mb-3 row">
                     <asp:Label CssClass="col-sm-2 form-label" ID="lblNombre" runat="server" Text="Nombre: "></asp:Label>
                     <div class="col-sm-8">
                          <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
                     </div>
                 </div>
                <div class="mb-3 row">
                    <asp:Label id="lblEstado" runat="server" Text="Estado: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                            <asp:ListItem Value="1">Activo</asp:ListItem>
                            <asp:ListItem Value="2">Inactivo</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="mb-3 row">
                    <asp:Label ID="lblTipo" CssClass="col-sm-2 form-label" runat="server" Text="Categoría: "></asp:Label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                            <asp:ListItem Value="1">Decoración</asp:ListItem>
                            <asp:ListItem Value="2">Regalos</asp:ListItem>
                            <asp:ListItem Value="3">Equipo audiovisual</asp:ListItem>
                            <asp:ListItem Value="4">Vestimenta</asp:ListItem>
                            <asp:ListItem Value="5">Invitación</asp:ListItem>
                            <asp:ListItem Value="6">Mobiliario</asp:ListItem>
                            <asp:ListItem Value="7">Tecnología</asp:ListItem>
                            <asp:ListItem Value="8">Otro</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>    

                <div class="mb-3 row">
                    <asp:Label id="lblPrecio" runat="server" Text="Precio: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblStockMin" runat="server" Text="Stock minimo: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtStockMin" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblStockAct" runat="server" Text="Stock actual: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtStockAct" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblUnidMed" runat="server" Text="Unidad de medida: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                            <asp:ListItem Value="1">Pulgada</asp:ListItem>
                            <asp:ListItem Value="2">Pie</asp:ListItem>
                            <asp:ListItem Value="3">Yarda</asp:ListItem>
                            <asp:ListItem Value="4">Onza</asp:ListItem>
                            <asp:ListItem Value="5">Libra</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblDescripcion" runat="server" Text="Descripción: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtDescrip" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

            </div>
            <div class="card-footer clearfix">
                <asp:LinkButton ID="btnRegresar" runat="server" Text="<i class='fa-solid fa-rotate-left'></i> Regresar" CssClass="float-start btn btn-secondary" OnClick="btnRegresar_Click"/>
                <asp:LinkButton ID="btnGuardar" CssClass="float-end btn btn-primary" runat="server" Text="<i class='fa-solid fa-floppy-disk pe-2'></i> Guardar" OnClick="btnGuardar_Click"/>
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
