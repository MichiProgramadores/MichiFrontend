﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="ListarTrabajadores.aspx.cs" Inherits="MichiSistemaWeb.ListarTrabajadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Listar trabajadores
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <script type="text/javascript">
        function mostrarModalEliminar(id) {
            const campoOculto = document.getElementById('<%= hfIdEliminar.ClientID %>');
            campoOculto.value = id;

            const modal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar'));
            modal.show();
        }
        function mostrarModalError() {
            const modalError = new bootstrap.Modal(document.getElementById('errorModal'));
            modalError.show();
        }
    </script>
    <style type="text/css">
        container.row: first - child {
            background - color: #FF7E5F; /* Color de fondo naranja */
            color: white; /* Cambiar el color del texto a blanco */
        }
        #dgvEmpleados th {
            background - color: #FFA500; /* Naranja */
            color: white; /* Texto blanco */
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
        <h2>Listado de trabajadores</h2> 
        <div class="container row">
            <div class="row align-items-center">
                <div class="col-auto">
                    <asp:DropDownList ID="DdlTipoTrabajador" runat="server" CssClass="form-select">
                        <%--
                             OnSelectedIndexChanged="DdlTipoTrabajador_SelectedIndexChanged" AutoPostBack="True">
                        --%>
                        <asp:ListItem Text="Seleccione un tipo" Value="0" />
                        <asp:ListItem Text="Despachador" Value="DESPACHADOR" />
                        <asp:ListItem Text="Delivery" Value="DELIVERY" />
                        <asp:ListItem Text="Asistente" Value="ASISTENTE" />
                    </asp:DropDownList>
                </div>
                <div class="col-auto">
                    <asp:Label ID="lblNombreID" CssClass="form-label" runat="server" Text="Ingrese el ID:"></asp:Label>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtNombreID" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <%--<div class="col-sm-2">
                    <asp:LinkButton ID="lbBuscar" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscar_Click" style="background-color: #FBCB43; border: none;" />
                </div>--%>
                <div class="col-auto">
                    <asp:Label ID="lblNombre" CssClass="form-label" runat="server" Text="Ingrese el nombre:"></asp:Label>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="lbBuscarN" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscarN_Click" style="background-color: #FBCB43; border: none;" />
                </div>
                <div class="text-end  justify-content-end">
                    <div class="p-2">
                        <asp:LinkButton ID="lbRegistrar" CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-plus pe-2'></i> Registrar trabajador" OnClick="lbRegistrar_Click" style="background-color:  #FF7E5F; border: none;" />
                    </div>
                    <div class="p-2">
                        <asp:LinkButton ID="ListarTodos" CssClass="btn btn-success" runat="server" Text="Listar todos" OnClick="ListarTodos_Click" style="background-color:  #FF7E5F; border: none;"/>
                    </div>
                </div>
            </div>

            <div class="container row">
                <asp:GridView ID="dgvEmpleados" runat="server" AutoGenerateColumns="false"
                    OnRowDataBound="dgvEmpleados_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="dgvEmpleados_PageIndexChanging" PageSize="8"
                    CssClass="table table-hover table-responsive table-striped"> 
                    <Columns>
                        <asp:BoundField HeaderText="ID" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Nombres" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Apellidos" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Celular" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Email" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Estado" ItemStyle-CssClass="align-middle" />

                        <asp:TemplateField>
                            <ItemTemplate>                    
                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-edit pe-4' style='color: #FF7E5F;'></i>"
                                    CommandArgument='<%# Eval("persona_id") %>' OnClick="lbModificar_Click" />

                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash pe-4' style='color: #FBCB43;'></i> "
                                    CommandArgument='<%# Eval("persona_id") %>'
                                    OnClientClick='<%# "mostrarModalEliminar(" + Eval("persona_id") + "); return false;" %>' />

                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-eye pe-4' style='color: #FF7E5F;'></i>"
                                    CommandArgument='<%# Eval("persona_id") %>' OnClick="lbVisualizar_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                <EmptyDataTemplate>
                        <div class="empty-message-container">
                            <i class="fas fa-exclamation-triangle"></i>
                            <span>Lo sentimos, no se encontró el Trabajador. Asegúrese de que los datos ingresados son correctos.</span>
                        </div>
                </EmptyDataTemplate>
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
                                ¿Está seguro de que desea eliminar este trabajador?
                            </div>

                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                                <asp:LinkButton ID="btnConfirmarEliminar" runat="server" CssClass="btn btn-danger"
                                    OnClick="btnConfirmarEliminar_Click"> Eliminar </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Modal ERROR -->
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
                                <asp:Label ID="lblMensajeError" runat="server" Text="Error al eliminar usuario" CssClass="form-text text-danger"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
</asp:Content>
