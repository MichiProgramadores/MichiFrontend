<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="ListarClientes.aspx.cs" Inherits="MichiSistemaWeb.ListarClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Listar Clientes
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
        <div class="container row">
            <div class="row align-items-center">
                <div class="col-auto">
                    <asp:Label ID="lblNombreID" CssClass="form-label" runat="server" Text="Ingrese el ID o el nombre:"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtNombreID" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="lbBuscar" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscar_Click"/>
                </div>
                <div class="col text-end p-3">
                    <asp:LinkButton ID="lbRegistrar" CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-plus pe-2'></i> Registrar Cliente" OnClick="lbRegistrar_Click"/>
                </div>
            </div>
            <div class="container row">
                <asp:GridView ID="dgvClientes" runat="server" AutoGenerateColumns="false"
                    OnRowDataBound="dgvClientes_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="dgvClientes_PageIndexChanging" PageSize="5"
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
                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-edit'></i> Modificar" CssClass="btn btn-warning" CommandArgument='<%# Eval("persona_id") %>' OnClick="lbModificar_Click" />
                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash'></i> Eliminar" CssClass="btn btn-danger" CommandArgument='<%# Eval("persona_id") %>' OnClick="lbEliminar_Click"/>
                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-eye'></i> Ver" CssClass="btn btn-info" CommandArgument='<%# Eval("persona_id") %>' OnClick="lbVisualizar_Click"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>



