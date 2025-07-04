﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="ListarEventos.aspx.cs" Inherits="MichiSistemaWeb.ListarEventos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Listar eventos
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
        <h2>Listado de eventos</h2>
        <div class="container row">
            <div class="col-auto">
                <asp:DropDownList ID="DdlTipoEvento" runat="server" CssClass="form-select">
                    <%--
        OnSelectedIndexChanged="DdlTipoEventoSelectedIndexChanged" AutoPostBack="True">
                    --%>
                    <asp:ListItem Text="Seleccione un tipo" Value="0" />
                    <asp:ListItem Text="Boda" Value="BODA" />
                    <asp:ListItem Text="Cumpleaños" Value="CUMPLEANHOS" />
                    <asp:ListItem Text="Concierto" Value="CONCIERTO" />
                    <asp:ListItem Text="Graduación" Value="GRADUACION" />
                    <asp:ListItem Text="Otro" Value="OTRO" />
                </asp:DropDownList>
            </div>
                <div class="col-auto">
                    <asp:Label ID="lblNombre" CssClass="form-label" runat="server" Text="Ingrese el ID del evento:"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="lbBuscar" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscar_Click" style="background-color: #FBCB43; border: none;" />
                </div>
                <div class="col text-end p-3">
                    <asp:LinkButton ID="lbRegistrar" CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-plus pe-2'></i> Registrar evento" OnClick="lbRegistrar_Click" style="background-color:  #FF7E5F; border: none;"/>
                </div>
            </div>
            <div class="container row">
                <asp:GridView ID="dgvEventos" runat="server" AutoGenerateColumns="false"
                    OnRowDataBound="dgvEventos_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="dgvEventos_PageIndexChanging" PageSize="10"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField DataField="evento_id" HeaderText="ID" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField DataField="fechaInicio" HeaderText="Fecha Inicio" ItemStyle-CssClass="align-middle" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="fechaFin" HeaderText="Fecha Fin" ItemStyle-CssClass="align-middle" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField DataField="tipoEvento" HeaderText="Tipo Evento" ItemStyle-CssClass="align-middle" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-edit pe-4' style='color: #FF7E5F;'></i>" CommandArgument='<%# Eval("evento_id") %>' OnClick="lbModificar_Click" />

                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash pe-4' style='color: #FBCB43;'></i>" CommandArgument='<%# Eval("evento_id") %>' OnClientClick='<%# "mostrarModalEliminar(" + Eval("evento_id") + "); return false;" %>' />

                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-eye pe-4' style='color: #FF7E5F;'></i>" CommandArgument='<%# Eval("evento_id") %>' OnClick="lbVisualizar_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                 <EmptyDataTemplate>
                        <div class="empty-message-container">
                            <i class="fas fa-exclamation-triangle"></i>
                            <span>Lo sentimos, no se encontró el Evento. Asegúrese de que el ID es el correcto.</span>
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
                                ¿Está seguro de que desea eliminar este evento?
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
