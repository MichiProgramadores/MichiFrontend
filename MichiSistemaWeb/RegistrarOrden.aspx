<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="RegistrarOrden.aspx.cs" Inherits="MichiSistemaWeb.RegistrarOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Registrar Orden
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <script src="Scripts/michi/registrarOrden.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
        <div class="card">
            <div class="card-header">
                <h2>
                    <asp:Label ID="lblTitulo" runat="server" Text="Registrar Orden"></asp:Label>
                </h2>
            </div>
            <div class="card-body">

                <div class="mb-3 row">
                    <asp:Label id="lblIdOrden" runat="server" CssClass="col-sm-2 col-form-label" Text="ID: "></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtIdOrden" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                 <div class="mb-3 row">
                     <asp:Label CssClass="col-sm-2 form-label" ID="lblTipoRecepcion" runat="server" Text="Tipo recepción: "></asp:Label>
                     <div class="col-sm-8">
                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                            <asp:ListItem Value="1">Delivery</asp:ListItem>
                            <asp:ListItem Value="2">Recojo en tienda</asp:ListItem>
                         </asp:DropDownList>
                     </div>
                 </div>
                <div class="mb-3 row">
                    <asp:Label id="lblSetUpPersonalizado" runat="server" Text="Set up personalizado: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtSetUpPersonalizado" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3 row">
                    <asp:Label ID="lblMontoTotal" CssClass="col-sm-2 form-label" runat="server" Text="Monto total: "></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtMonto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>    

                <div class="mb-3 row">
                    <asp:Label id="lblSaldo" runat="server" Text="Saldo: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtSaldo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblCantDias" runat="server" Text="Cantidad dias: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtCantDias" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblFechaDevol" runat="server" Text="Fecha devolución: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtFechaDevol" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>


                <div class="mb-3 row">
                    <asp:Label id="lblFechaEmis" runat="server" Text="Fecha emisión: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtFechaEmis" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblFechaReg" runat="server" Text="Fecha registro: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtFechaReg" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblFechaEntr" runat="server" Text="Fecha entrega: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtFechaEntr" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblTipoEstDevol" runat="server" Text="Tipo estado de devolución: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                            <asp:ListItem Value="1">Devuelto en buenas condiciones</asp:ListItem>
                            <asp:ListItem Value="2">Devuelto en malas condiciones</asp:ListItem>
                            <asp:ListItem Value="3">Devuelto en condiciones regulares</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblIdCliente" runat="server" Text="Id del cliente: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtIdCliente" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblIdTrabaj" runat="server" Text="Id del trabajador: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtIdTrabaj" runat="server" CssClass="form-control"></asp:TextBox>
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
