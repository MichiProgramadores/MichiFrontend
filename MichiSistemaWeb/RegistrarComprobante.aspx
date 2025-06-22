<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="RegistrarComprobante.aspx.cs" Inherits="MichiSistemaWeb.RegistrarComprobante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Registrar Comprobante
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <script src="Scripts/michi/registrarComprobante.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                    <asp:Label ID="lblIdOrden" runat="server" Text="Orden:" CssClass="col-sm-2 col-form-label"></asp:Label>

                    <div class="col-sm-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtIdOrden" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdnOrdenId" runat="server" />
                            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalOrdenes">
                                <i class="fas fa-search"></i>
                            </button>
                        </div>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblIdCliente" runat="server" CssClass="col-sm-2 col-form-label" Text="Cliente: "></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtIdCliente" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:HiddenField ID="hdnClienteId" runat="server" />
                    </div>
                </div>

                <%--
                 <div class="mb-3 row">
                    <asp:Label ID="lblIdCliente" runat="server" Text="Cliente:" CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtIdCliente" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdnClienteId" runat="server" />
                            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalClientes">
                                <i class="fas fa-search"></i>
                            </button>
                        </div>
                    </div>
                </div>
                    --%>
                
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
                        <asp:DropDownList ID="ddlTipoComprobante" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                
                <div class="mb-3 row">
                    <asp:Label id="lblTax" runat="server" Text="Tax: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtTax" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:HiddenField ID="hdnTax" runat="server" />
                    </div>
                </div>

            </div>

                    <!-- Tabla de detalles -->
<div class="row mb-3">
    <div class="col">

        <asp:GridView ID="gvDetalles" runat="server" CssClass="table table-striped" 
            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:BoundField DataField="producto_id" HeaderText="Código" />
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="unidad_medida" HeaderText="Medida/Unidad" />
                <asp:BoundField DataField="subtotal" HeaderText="Precio" DataFormatString="{0:C2}" />
              
                <%--
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-danger btn-sm"
                            OnClick="btnEliminar_Click" CommandArgument='<%# Container.DataItemIndex %>'>
                            <i class="fas fa-trash"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                    
                </asp:TemplateField>
                 --%>

            </Columns>
        </asp:GridView>
    </div>
</div>

<div class="card-footer clearfix">
    <asp:LinkButton ID="btnRegresar" runat="server" Text="<i class='fa-solid fa-rotate-left'></i> Regresar" CssClass="float-start btn btn-secondary" OnClick="btnRegresar_Click"/>
    <asp:LinkButton ID="btnGuardar" CssClass="float-end btn btn-primary" runat="server" Text="<i class='fa-solid fa-floppy-disk pe-2'></i> Guardar" OnClick="btnGuardar_Click"/>
</div>
<div class="col-md-12 mt-4 alert alert-danger" id="divError" runat="server" style="display: none;">
    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
</div>
<div class="col-md-6 text-end">
     <h3>Total: <asp:Label ID="lblTotal" runat="server" Text="S/. 0.00"></asp:Label></h3>
</div>


<!-- Modal Clientes -->
            <%-- 
<div class="modal fade" id="modalClientes" tabindex="-1">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Seleccionar Cliente</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>
            <div class="modal-body">
                <asp:GridView ID="dgvClientes" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="persona_id" HeaderText="ID" />
                     
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSeleccionarCliente" runat="server" CssClass="btn btn-primary btn-sm"
                                    OnClick="btnSeleccionarCliente_Click" CommandArgument='<%# Eval("persona_id") %>'>
                                    Seleccionar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</div>
    --%>


            <!-- Modal Ordenes -->
<div class="modal fade" id="modalOrdenes" tabindex="-1">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Seleccionar Orden</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>
            <div class="modal-body">
                <asp:GridView ID="dgvOrdenes" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="idOrden" HeaderText="ID" />
                       
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSeleccionarOrden" runat="server" CssClass="btn btn-primary btn-sm"
                                    OnClick="btnSeleccionarOrden_Click" CommandArgument='<%# Eval("idOrden") %>'>
                                    Seleccionar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
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
