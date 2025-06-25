<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="ListarProductos.aspx.cs" Inherits="MichiSistemaWeb.ListarProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Listar productos
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
        <h2>Listado de productos</h2>
        <div class="container row">
            <div class="row align-items-center">
                <div class="col-auto">
                    <asp:DropDownList ID="DdlTipoProducto" runat="server" CssClass="form-select">
                        <%--
                        OnSelectedIndexChanged="DdlTipoProducto_SelectedIndexChanged" AutoPostBack="True">
                        --%>
                        <asp:ListItem Text="Seleccione un tipo" Value="0" />
                        <asp:ListItem Text="Decoración" Value="DECORACION" />
                        <asp:ListItem Text="Regalos" Value="REGALOS" />
                        <asp:ListItem Text="Equipo Audiovisual" Value="EQUIPO_AUDIOVISUAL" />
                        <asp:ListItem Text="Vestimenta" Value="VESTIMENTA" />
                        <asp:ListItem Text="Invitación" Value="INVITACION" />
                        <asp:ListItem Text="Mobiliario" Value="MOBILIARIO" />
                        <asp:ListItem Text="Tecnología" Value="TECNOLOGIA" />
                        <asp:ListItem Text="Otro" Value="OTRO" />
                    </asp:DropDownList>
                </div>
                <div class="col-auto">
                    <asp:Label ID="lblID" CssClass="form-label" runat="server" Text="Ingrese el ID:"></asp:Label>
                </div>
                <div class="col-sm-1">
                    <asp:TextBox ID="txtID" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <%--
                <div class="col-sm-2">
                    <asp:LinkButton ID="lbBuscar" CssClass="btn btn-info" runat="server"
                        Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar"
                        OnClick="lbBuscar_Click" style="background-color: #FBCB43; border: none;"/>
                </div>
                --%>

                <div class="col-auto">
                    <asp:Label ID="LabelNombre" CssClass="form-label" runat="server" Text="Ingrese el nombre:"></asp:Label>
                </div>
                <div class="col-sm-1">
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="lblBuscarN" CssClass="btn btn-info" runat="server"
                        Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar"
                        OnClick="lblBuscarN_Click" style="background-color: #FBCB43; border: none;" />
                </div>
                <div class="d-flex justify-content-end">
                    <div class="p-2">
                        <asp:LinkButton ID="lbRegistrar" CssClass="btn btn-success" runat="server"
                            Text="<i class='fa-solid fa-plus pe-2'></i> Registrar producto"
                            OnClick="lbRegistrar_Click" style="background-color:  #FF7E5F; border: none;"/>
                    </div>
                    <div class="p-2">
                        <asp:LinkButton ID="ListarTodos" CssClass="btn btn-success" runat="server"
                            Text="Listar todos" OnClick="ListarTodos_Click" style="background-color:  #FF7E5F; border: none;"/>
                    </div>
                </div>
            </div>

            <div class="container row">
                <asp:GridView ID="dgvProductos" runat="server" AutoGenerateColumns="false"
                    OnRowDataBound="dgvProductos_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="dgvProductos_PageIndexChanging" PageSize="9"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField HeaderText="ID" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Nombre" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Tipo" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Precio" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Stock minimo" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Stock actual" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Unidad de medida" ItemStyle-CssClass="align-middle" />
                   
                        <asp:BoundField HeaderText="Estado" ItemStyle-CssClass="align-middle" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-edit pe-4' style='color: #FF7E5F;'></i>"
                                    CommandArgument='<%# Eval("producto_id") %>' OnClick="lbModificar_Click" />

                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash pe-4' style='color:  #FBCB43;'></i>"
                                    CommandArgument='<%# Eval("producto_id") %>'
                                    OnClientClick='<%# "mostrarModalEliminar(" + Eval("producto_id") + "); return false;" %>' />

                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-eye pe-4' style='color: #FF7E5F;'></i>"
                                    CommandArgument='<%# Eval("producto_id") %>' OnClick="lbVisualizar_Click" />
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
                                <h5 class="modal-title" id="modalEliminarLabel">Confirmar eliminación</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                            </div>
                            <div class="modal-body">
                                ¿Está seguro de que desea eliminar este producto?
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                                <asp:LinkButton ID="btnConfirmarEliminar" runat="server" CssClass="btn btn-danger"
                                    OnClick="btnConfirmarEliminar_Click"> Eliminar </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
