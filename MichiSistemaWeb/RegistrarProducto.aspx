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
                    <asp:Label ID="lblTitulo" runat="server" Text="Registrar Producto"></asp:Label>
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
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre: " CssClass="col-sm-2 form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>


                <div class="mb-3 row">
                    <asp:Label ID="lblPrecio" runat="server" Text="Precio: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label ID="lblEdadMin" runat="server" Text="Edad Mínima: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtEdadMin" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label ID="lblStockAct" runat="server" Text="Stock actual: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtStockAct" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label ID="lblStockMin" runat="server" Text="Stock minimo: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtStockMin" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label ID="lblTipo" CssClass="col-sm-2 form-label" runat="server" Text="Categoría: "></asp:Label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label ID="lblVolumen" runat="server" Text="Volumen: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtVolumen" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtDescrip" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label ID="lblUnidMed" runat="server" Text="Unidad de medida: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="ddlUnidadMedida" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label ID="lblEstado" runat="server" Text="Estado: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

            </div>
            <div class="card-footer clearfix">
                <asp:LinkButton ID="btnRegresar" runat="server" Text="<i class='fa-solid fa-rotate-left'></i> Regresar" CssClass="float-start btn btn-secondary" OnClick="btnRegresar_Click" />
                <asp:LinkButton ID="btnGuardar" CssClass="float-end btn btn-primary" runat="server" Text="<i class='fa-solid fa-floppy-disk pe-2'></i> Guardar" OnClick="btnGuardar_Click" />
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
