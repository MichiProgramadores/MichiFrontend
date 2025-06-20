﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="~/ListarOrdenes.aspx.cs" Inherits="MichiSistemaWeb.ListarOrdenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container mt-4">
        <h2>Listado de Órdenes</h2>
        <div class="row align-items-center">
            <div class="col-auto">
                <asp:Label ID="lblNombreID" CssClass="form-label" runat="server" Text="Ingrese el ID"></asp:Label>
            </div>
                        <div class="col-sm-3">
                <asp:TextBox ID="txtNombreID" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-2">
                <asp:LinkButton ID="lbBuscar" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscar_Click" />
            </div>
<%--            <div class="col text-end p-3">
                <asp:LinkButton ID="lbRegistrar" CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-plus pe-2'></i> Registrar Cliente" OnClick="lbRegistrar_Click" />
            </div>--%>
        </div>
        
        <div class="text-end pb-3">
            <asp:Button ID="btnNuevaOrden" OnClick="BtnNuevaOrden_Click" runat="server" 
                Text="Nueva Orden" CssClass="btn btn-success" />
        </div>
        <asp:GridView ID="dgvOrdenes" runat="server" AutoGenerateColumns="false" 
            AllowPaging="true" PageSize="10" OnPageIndexChanging="dgvOrdenes_PageIndexChanging"
            CssClass="table table-striped table-responsive table-hover">
     <Columns>
                <asp:BoundField DataField="idOrden" HeaderText="N° Orden" />
                <%--<asp:BoundField DataField="clienteID" HeaderText="Cliente" />
                <asp:BoundField DataField="trabajadorID" HeaderText="Empleado" />--%>
<%--                <asp:BoundField DataField="fecha" HeaderText="Emisión" 
                    DataFormatString="{0:dd/MM/yyyy}" />--%>
                <asp:BoundField DataField="totalPagar" HeaderText="Total" 
                    DataFormatString="{0:C2}" />
                <asp:TemplateField HeaderText="Recepción">
                    <ItemTemplate>
                        <%# Enum.GetName(typeof(MichiSistemaWeb.MichiBackend.tipoRecepcion), Eval("tipoRecepcion")) %>
                    </ItemTemplate>
                </asp:TemplateField>
         
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEditar" runat="server" 
                            CommandName="Editar"
                            Text="<i class='fa-solid fa-edit pe-4'></i>" 
                            CommandArgument='<%# Eval("idOrden") %>' OnClick="lbModificar_Click" />

                        <%-- 
                       <asp:LinkButton ID="lbEliminar" runat="server"
                            CommandName="Eliminar"
                           Text="<i class='fa-solid fa-trash pe-4'></i> "
                            CommandArgument='<%# Eval("idOrden") %>'
                            OnClick="lbEliminar_Click"
                           Style="text-decoration: none !important;"
                            OnClientClick="return confirm('¿Está seguro de eliminar esta orden?');"
                            />
                        --%>    
                      
                        <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash pe-4'></i>" CommandArgument='<%# Eval("idOrden") %>' OnClientClick='<%# "mostrarModalEliminar(" + Eval("idOrden") + "); return false;" %>' />


                       
                        <asp:LinkButton ID="lbVerDetalles" runat="server" Text="<i class='fa-solid fa-eye pe-4'></i>"
                         CommandName="Ver" CommandArgument='<%# Eval("idOrden") %>' OnClick="lbVerDetalles_Click" />
                    </ItemTemplate>
               </asp:TemplateField>

            </Columns>
            <EmptyDataTemplate>
                <div class="text-center">No hay ordenes registradas</div>
            </EmptyDataTemplate>
        </asp:GridView>
              <asp:Label ID="lblError" runat="server" Text="" CssClass="text-danger"></asp:Label>
    </div>
    
    <!-- Modal Detalles -->
    <div class="modal fade" id="modalDetalles" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Detalles de Venta</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="dgvDetalles" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                        CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="producto" HeaderText="Producto" />
                            <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                            <asp:BoundField DataField="cantidadSolicitada" HeaderText="Cantidad" />
                            <asp:BoundField DataField="cantidadEntregada" HeaderText="Cantidad" />
                            <asp:BoundField DataField="PrecioAsignado" HeaderText="Precio Unit." 
                                DataFormatString="{0:C2}" />
                            <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" 
                                DataFormatString="{0:C2}" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
                    </div>
    </div>


    <asp:HiddenField ID="hfIdEliminar" runat="server" />

    <!-- Modal Bootstrap -->
    <div class="modal fade" id="modalConfirmarEliminar" tabindex="-1" aria-labelledby="modalEliminarLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title" id="modalEliminarLabel">Confirmar Eliminación</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    ¿Está seguro de que desea eliminar esta orden?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:LinkButton ID="btnConfirmarEliminar" runat="server" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click"> Eliminar </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Scripts" runat="server">
    <script type="text/javascript">
        function showModalForm() {
            var modalForm = new bootstrap.Modal(document.getElementById('modalDetalles'));
            modalForm.toggle();
        }
    </script>

    <script type="text/javascript">
        function mostrarModalEliminar(id) {
            const campoOculto = document.getElementById('<%= hfIdEliminar.ClientID %>');
            campoOculto.value = id;

            const modal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar'));
            modal.show();
        }
    </script>




</asp:Content>