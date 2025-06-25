<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="RegistrarOrden.aspx.cs" Inherits="MichiSistemaWeb.RegistrarOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Registrar Orden
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <script src="Scripts/michi/registrarOrden.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="card">
            <div class="card-header">
                <h2>
                    <asp:Label ID="lblTitulo" runat="server" Text="Registrar orden"></asp:Label>
                </h2>
            </div>
            <div class="card-body">
                <div class="mb-3 row">
                    
                     <asp:Label id="lblIdOrden" runat="server" CssClass="col-md-6 col-form-label" Text="ID: " ></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtIdOrden" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    </div>
                    <div class="mb-3 row">
                        <asp:Label ID="lblIdCliente" runat="server" Text="*Cliente:" CssClass="col-sm-2 col-form-label"></asp:Label>

                        <div class="col-sm-8">
                            <div class="input-group">
                                <asp:TextBox ID="txtIdCliente" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdnClienteId" runat="server" />
                                <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalClientes"  style="background-color: #FBCB43; border: none;">
                                    <i class="fas fa-search"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                

                    <div class="mb-3 row">
                        <asp:Label ID="lblIdTrabaj" runat="server" Text="*Trabajador:" CssClass="col-sm-2 col-form-label"></asp:Label>
    
                        <div class="col-sm-8">
                            <div class="input-group">
                                <asp:TextBox ID="txtIdTrabaj" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdnTrabajadorId" runat="server" />
                                <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalTrabajadores"  style="background-color: #FBCB43; border: none;">
                                    <i class="fas fa-search"></i>
                                </button>
                            </div>
                        </div>
                    </div>

                 <div class="mb-3 row">
                     <asp:Label CssClass="col-sm-2 form-label" ID="lblTipoRecepcion" runat="server" Text="*Tipo recepción: "></asp:Label>
                     <div class="col-sm-8">
                         <asp:DropDownList ID="ddlTipoRecepcion" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                            <asp:ListItem Value="DELIVERY">Delivery</asp:ListItem>
                            <asp:ListItem Value="RECOJO_EN_TIENDA">Recojo en tienda</asp:ListItem>
                        </asp:DropDownList>
                     </div>
                 </div>
                <div class="mb-3 row">
                    <asp:Label id="lblSetUpPersonalizado" runat="server" Text="*Set up personalizado: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtSetUpPersonalizado" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3 row">
                    <asp:Label ID="lblMontoTotal" CssClass="col-sm-2 form-label" runat="server" Text="*Monto total: "></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtMonto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>    
                
                <div class="mb-3 row">
                    <asp:Label id="lblSaldo" runat="server" Text="*Saldo: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtSaldo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblCantDias" runat="server" Text="*Cantidad dias: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtCantDias" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>




                <div class="mb-3 row">
                    <asp:Label id="lblFechaEmis" runat="server" Text="*Fecha emisión: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtFechaEmis" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblFechaReg" runat="server" Text="*Fecha registro: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtFechaReg" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3 row">
                    <asp:Label id="lblFechaEntr" runat="server" Text="*Fecha entrega: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtFechaEntr" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3 row">
                    <asp:Label id="lblFechaDevol" runat="server" Text="*Fecha devolución: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox id="txtFechaDevol" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3 row">
                    <asp:Label id="lblTipoEstDevol" runat="server" Text="Tipo estado de devolución: " CssClass="col-sm-2 col-form-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="ddlTipoEstDevol" runat="server" CssClass="form-select">
                            <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                            <asp:ListItem Value="1">Devuelto en buenas condiciones</asp:ListItem>
                            <asp:ListItem Value="2">Devuelto en malas condiciones</asp:ListItem>
                            <asp:ListItem Value="3">Devuelto en condiciones regulares</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                 
                
                
            </div>
                   <!-- Tabla de detalles -->
           <div class="row mb-3">
               <div class="col">
                  <%-- <button type="button" class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#modalDetalle">
                       <i class="fas fa-plus"></i> Agregar Producto
                   </button>--%>
                   <asp:PlaceHolder ID="phAgregarProducto" runat="server">
                        <button type="button" class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#modalDetalle" >
                            <i class="fas fa-plus"></i> Agregar producto
                        </button>
                    </asp:PlaceHolder>

                   <asp:GridView ID="gvDetalles" runat="server" CssClass="table table-striped" 
                       AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                       <Columns>
                           <asp:BoundField DataField="producto" HeaderText="Código" />
                           
                           <%-- Aqui va el nombre --%>
                           <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <%# GetProductName(Eval("producto")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

                           <asp:BoundField DataField="cantidadSolicitada" HeaderText="Cant. Solicitada" />
                           <asp:BoundField DataField="unidadMedida" HeaderText="Medida/Unidad" />
                           <asp:BoundField DataField="precioAsignado" HeaderText="Precio" DataFormatString="{0:C2}" />
                         
                           <asp:TemplateField HeaderText="Acciones">
                               <ItemTemplate>
                                   <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-danger btn-sm"
                                       OnClick="btnEliminar_Click" CommandArgument='<%# Container.DataItemIndex %>'>
                                       <i class="fas fa-trash"></i>
                                   </asp:LinkButton>
                               </ItemTemplate>
                               
                           </asp:TemplateField>
                       </Columns>
                   </asp:GridView>
               </div>
           </div>


            <div class="card-footer clearfix">
                <asp:LinkButton ID="btnRegresar" runat="server" Text="<i class='fa-solid fa-rotate-left'></i> Regresar" CssClass="float-start btn btn-secondary" OnClick="btnRegresar_Click"/>
                <asp:LinkButton ID="btnGuardar" CssClass="float-end btn btn-primary" runat="server" Text="<i class='fa-solid fa-floppy-disk pe-2'></i> Guardar" OnClick="btnGuardar_Click" style="background-color:  #FF7E5F; border: none;"/>
            </div>
            <div class="col-md-12 mt-4 alert alert-danger" id="divError" runat="server" style="display: none;">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-md-6 text-end">
                 <h3>Total: <asp:Label ID="lblTotal" runat="server" Text="S/. 0.00"></asp:Label></h3>
            </div>
        </div>
    </div>
    
    <!-- Modal Clientes -->
    <div class="modal fade" id="modalClientes" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Seleccionar cliente</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="dgvClientes" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="persona_id" HeaderText="ID" />
                            <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                            <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSeleccionarCliente" runat="server" CssClass="btn btn-primary btn-sm" style="background-color:  #FF7E5F; border: none; color: black;"
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

    <!-- Modal Trabajadores -->
    <div class="modal fade" id="modalTrabajadores" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Seleccionar trabajador</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="dgvTrabajadores" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="persona_id" HeaderText="ID" />
                            <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                            <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSeleccionarTrabajador" runat="server" CssClass="btn btn-primary btn-sm" style="background-color:  #FF7E5F; border: none; color: black;"
                                        OnClick="btnSeleccionarTrabajador_Click" CommandArgument='<%# Eval("persona_id") %>'>
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

    <!-- Modal Detalle -->
    <div class="modal fade" id="modalDetalle" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Agregar producto</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upModalDetalle" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group mb-3">
                                <label>Producto:</label>
                                <asp:DropDownList ID="ddlProductos" runat="server" CssClass="form-select" 
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlProductos_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group mb-3">
                                <label>Stock:</label>
                                <asp:TextBox ID="txtStockProducto" runat="server" TextMode="Number" CssClass="form-control" 
                                    ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="form-group mb-3">
                                <label>Stock Minimo:</label>
                                <asp:TextBox ID="txtStockMinimo" runat="server" TextMode="Number" CssClass="form-control" 
                                    ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="form-group mb-3">
                                <label>Precio Unitario:</label>
                                <asp:TextBox ID="txtPrecioUnitario" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>Cantidad:</label>
                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" 
                                    TextMode="Number"></asp:TextBox>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnAgregarDetalle" runat="server" Text="Agregar" 
                        CssClass="btn btn-primary" OnClick="btnAgregarDetalle_Click" />
                </div>
            </div>
        </div>
    </div>
    

    
    <div class="modal fade" id="errorModal" tabindex="-1" aria-labelledby="errorModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title" id="errorModalLabel">
                        <i class="bi bi-exclamation-triangle-fill me-2"></i>Error
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblMensajeError" runat="server" Text="Mensaje de ejemplo..." CssClass="form-text error-message"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <style>

        .modal-header.bg-danger {
            background-color: #dc3545 !important;
            color: white !important;
        }

        .modal-body .form-text {
            font-size: 1.25rem;
            font-weight: bold;
        }

        .modal-footer {
            border-top: 1px solid #f5c6cb;
        }
    </style>
</asp:Content>
