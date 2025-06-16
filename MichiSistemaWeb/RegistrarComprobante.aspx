<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="RegistrarComprobante.aspx.cs" Inherits="MichiSistemaWeb.RegistrarComprobante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Registrar Comprobante
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <script src="Scripts/michi/registrarComprobante.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
        <div class="card">
            <div class="card-header">
                <h2>
                    <asp:Label ID="lblTitulo" runat="server" Text="Registrar Comprobante"></asp:Label>
                </h2>
            </div>
            <div class="card-body">

                <div class="mb-3 row">
                    <asp:Label id="lblIdComprobante" runat="server" CssClass="col-sm-2 col-form-label" Text="ID: "></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtIdComprobante" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                
                <div class="mb-3 row">
                    <asp:Label ID="lblMontoTotal" CssClass="col-sm-2 form-label" runat="server" Text="Monto total: "></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtMonto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div> 

                <div class="mb-3 row">
                    <asp:Label id="lblEstado" runat="server" Text="Estado: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtEstado" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblFechaEmis" runat="server" Text="Fecha emisión: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtFechaEmis" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label CssClass="col-sm-2 form-label" ID="lblTipoComprobante" runat="server" Text="Tipo comprobante: "></asp:Label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                           <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                           <asp:ListItem Value="1">INVOICE</asp:ListItem>
                           <asp:ListItem Value="2">RECEIPT</asp:ListItem>
                           <asp:ListItem Value="3">CREDIT_NOTE</asp:ListItem>
                           <asp:ListItem Value="4">DEBIT_NOTE</asp:ListItem>
                           <asp:ListItem Value="5">PURCHASE_ORDER</asp:ListItem>
                           <asp:ListItem Value="6">SALES_ORDER</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                
                <div class="mb-3 row">
                    <asp:Label id="lblTax" runat="server" Text="Tax: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtTax" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblIdCliente" runat="server" Text="Id del cliente: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtIdCliente" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblIdOrden" runat="server" Text="Id de la orden: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtIdOrden" runat="server" CssClass="form-control"></asp:TextBox>
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
                    <asp:Label ID="lblMensajeError" runat="server" Text="Error en los datos del comprobante" CssClass="form-text text-danger"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
