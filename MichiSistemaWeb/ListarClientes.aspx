<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="ListarClientes.aspx.cs" Inherits="MichiSistemaWeb.ListarClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Listar Clientes
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
        <h2>Listado de Clientes</h2>
        <div class="container row">
            <div class="row align-items-center">
                 <div class="col-auto">
                     <asp:DropDownList ID="DdlTipoCliente" runat="server" CssClass="form-select"
                         OnSelectedIndexChanged="DdlTipoCliente_SelectedIndexChanged" AutoPostBack="True">
                         <asp:ListItem Text="Seleccione un tipo" Value="0" />
                         <asp:ListItem Text="Empleador" Value="EIN" />
                         <asp:ListItem Text="Persona Contribuyente" Value="TIN" />
                         <asp:ListItem Text="Seguridad Social" Value="SSN" />
                     </asp:DropDownList>
                 </div>

                <div class="col-auto">
                    <asp:Label ID="lblNombreID" CssClass="form-label" runat="server" Text="Ingrese el ID:"></asp:Label>
                </div>
                <div class="col-sm-1">
                    <asp:TextBox ID="txtNombreID" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="lbBuscar" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscar_Click" style="background-color: #FBCB43; border: none;" />
                </div>
                <div class="col-auto">
                    <asp:Label ID="LabelNombre" CssClass="form-label" runat="server" Text="Ingrese nombre:"></asp:Label>
                </div>
                <div class="col-sm-1">
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="lbBuscarN" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscarN_Click" style="background-color: #FBCB43; border: none;" />
                </div>
                
                <div class="d-flex justify-content-end">
                    <div class="p-2">
                        <asp:LinkButton ID="lbRegistrar" CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-plus pe-2'></i> Registrar Cliente" OnClick="lbRegistrar_Click" style="background-color: #FF7E5F; border: none;"/>
                    </div>
                    <div class="p-2">

                        <asp:LinkButton ID="ListarTodos" CssClass="btn btn-success" runat="server" Text="Listar todos" OnClick="ListarTodos_Click" style="background-color:  #FF7E5F; border: none;"/>
                    </div>
                </div>

            </div>
            <div class="container row">
                <asp:GridView ID="dgvClientes" runat="server" AutoGenerateColumns="false"
                    OnRowDataBound="dgvClientes_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="dgvClientes_PageIndexChanging" PageSize="10"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField HeaderText="ID" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Nombres" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Apellidos" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Celular" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Email" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Puntuacion" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Activo" ItemStyle-CssClass="align-middle" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-edit pe-4' style='color: #FF7E5F;'></i>" CommandArgument='<%# Eval("persona_id") %>' OnClick="lbModificar_Click" />

                                <%--1:
                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash pe-4'></i>"  CommandArgument='<%# Eval("persona_id") %>' OnClick="lbEliminar_Click"/>
                                --%>
                                <%--2: 
                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash pe-4'></i>" CommandArgument='<%# Eval("persona_id") %>' OnClick="lbEliminar_Click" OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este cliente?');" />
                                --%>

                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash pe-4' style='color: #FBCB43;'></i>" CommandArgument='<%# Eval("persona_id") %>' OnClientClick='<%# "mostrarModalEliminar(" + Eval("persona_id") + "); return false;" %>' />

                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-eye pe-4' style='color: #FF7E5F;'></i>" CommandArgument='<%# Eval("persona_id") %>' OnClick="lbVisualizar_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

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
                                ¿Está seguro de que desea eliminar este cliente?
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

</asp:Content>
