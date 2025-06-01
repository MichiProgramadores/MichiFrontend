<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="ListarEventos.aspx.cs" Inherits="MichiSistemaWeb.ListarEventos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Listar Eventos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
        <div class="table-responsive">
            <asp:GridView ID="dgvEventos" runat="server" AutoGenerateColumns="false"
                OnRowDataBound="dgvEventos_RowDataBound" AllowPaging="true"
                OnPageIndexChanging="dgvEventos_PageIndexChanging" PageSize="10"
                Width="100%"
                CssClass="table table-hover table-striped">
                <Columns>
                    <asp:BoundField DataField="evento_id" HeaderText="ID" ItemStyle-CssClass="align-middle" />

                    <asp:BoundField DataField="fechaInicio" HeaderText="Fecha Inicio" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField DataField="fechaFin" HeaderText="Fecha Fin" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField DataField="horaInicio" HeaderText="Hora Inicio" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField DataField="horaFin" HeaderText="Hora Fin" ItemStyle-CssClass="align-middle" />

                    <asp:BoundField DataField="direccion" HeaderText="Dirección" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField DataField="codigoPostal" HeaderText="Código Postal" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField DataField="tipoEvento" HeaderText="Tipo Evento" ItemStyle-CssClass="align-middle" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" Text="Editar" CssClass="btn btn-warning"
                                CommandArgument='<%# Eval("evento_id") %>' OnClick="lbModificar_Click" />
                            <asp:LinkButton runat="server" Text="Eliminar" CssClass="btn btn-danger"
                                CommandArgument='<%# Eval("evento_id") %>' OnClick="lbEliminar_Click" />
                            <asp:LinkButton runat="server" Text="Ver" CssClass="btn btn-info"
                                CommandArgument='<%# Eval("evento_id") %>' OnClick="lbVisualizar_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>