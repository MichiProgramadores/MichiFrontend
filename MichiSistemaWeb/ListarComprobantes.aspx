<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="ListarComprobantes.aspx.cs" Inherits="MichiSistemaWeb.ListarComprobantes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Listar Comprobante
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <script type="text/javascript">
    function mostrarModalEliminar(id) {
        const campoOculto = document.getElementById('<%= hfIdEliminar.ClientID %>');
        campoOculto.value = id;

        const modal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar'));
        modal.show();
    }
    </script>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">    
     <div class="container">
         <h2>Listado de Comprobantes</h2>
        <div class="container row">
            <div class="row align-items-center">
                <div class="col-auto">
                    <asp:Label ID="lblNombre" CssClass="form-label" runat="server" Text="Ingrese el ID del comprobante:"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="lbBuscar" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscar_Click" style="background-color: #FBCB43; border: none;" />
                </div>
                <div class="col text-end p-3">
                    <asp:LinkButton ID="lbRegistrar" CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-plus pe-2'></i> Registrar Comprobante" OnClick="lbRegistrar_Click" style="background-color:  #FF7E5F; border: none;"/>
                </div>
            </div>
            <div class="container">
                <div class="table-responsive">
                    <asp:GridView ID="dgvComprobantes" runat="server" AutoGenerateColumns="false"
                        OnRowDataBound="dgvComprobantes_RowDataBound" AllowPaging="true"
                        OnPageIndexChanging="dgvComprobantes_PageIndexChanging" PageSize="8"
                        Width="100%"
                        CssClass="table table-hover table-striped">
                        <Columns>
                            <asp:BoundField DataField="id_comprobante" HeaderText="ID" ItemStyle-CssClass="align-middle" />
                            <asp:BoundField DataField="monto_total" HeaderText="Monto total" ItemStyle-CssClass="align-middle" />
                            <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-CssClass="align-middle" />

                            <asp:BoundField DataField="fecha_emision" HeaderText="Fecha emisión" ItemStyle-CssClass="align-middle" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="tipoComprobante" HeaderText="Tipo de comprobante" ItemStyle-CssClass="align-middle" />
                            <asp:BoundField DataField="tax" HeaderText="Tax" ItemStyle-CssClass="align-middle" />
                            <asp:BoundField DataField="cliente_id" HeaderText="Id cliente" ItemStyle-CssClass="align-middle" />
                            <asp:BoundField DataField="orden_id" HeaderText="Id orden" ItemStyle-CssClass="align-middle" />

                            <asp:TemplateField>
                                <ItemTemplate>

                                    <asp:LinkButton runat="server" Text="<i class='fa-solid fa-edit pe-4' style='color: #FF7E5F;'></i>" CommandArgument='<%# Eval("id_comprobante") %>' OnClick="lbModificar_Click" />
                                    
                                    <%--
                                    <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash pe-4'></i>" CommandArgument='<%# Eval("id_comprobante") %>' OnClick="lbEliminar_Click" />
                                    --%>

                                    <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash pe-4' style='color: #FBCB43;'></i>" CommandArgument='<%# Eval("id_comprobante") %>' OnClientClick='<%# "mostrarModalEliminar(" + Eval("id_comprobante") + "); return false;" %>' />

                                    <asp:LinkButton runat="server" Text="<i class='fa-solid fa-eye pe-4' style='color: #FF7E5F;'></i>" CommandArgument='<%# Eval("id_comprobante") %>' OnClick="lbVisualizar_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <asp:HiddenField ID="hfIdEliminar" runat="server" />
                    <asp:Label ID="lblError" runat="server" Text="" CssClass="text-danger"></asp:Label>

<!-- Modal Bootstrap -->
<div class="modal fade" id="modalConfirmarEliminar" tabindex="-1" aria-labelledby="modalEliminarLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header bg-danger text-white">
                <h5 class="modal-title" id="modalEliminarLabel">Confirmar Eliminación</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
            </div>
            <div class="modal-body">
                ¿Está seguro de que desea eliminar este comprobante?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                <asp:LinkButton ID="btnConfirmarEliminar" runat="server" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click"> Eliminar </asp:LinkButton>
            </div>
        </div>
    </div>
</div>




                </div>
            </div>
        </div>
    </div>
</asp:Content>
