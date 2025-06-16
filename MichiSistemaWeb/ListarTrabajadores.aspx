<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="ListarTrabajadores.aspx.cs" Inherits="MichiSistemaWeb.ListarTrabajadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Listar Empleados
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
        <div class="container row">
            <div class="row align-items-center">
                <div class="col-auto">
                    <asp:Label ID="lblNombreID" CssClass="form-label" runat="server" Text="Ingrese el ID:"></asp:Label>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtNombreID" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="lbBuscar" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscar_Click"/>
                </div>
                <div class="col-auto">
                    <asp:Label ID="lblNombre" CssClass="form-label" runat="server" Text="Ingrese nombre:"></asp:Label>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="lbBuscarN" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscarN_Click" />
                    
                </div>
                <div class="d-flex justify-content-end">
                    <div class="p-2">
                        <asp:LinkButton ID="lbRegistrar" CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-plus pe-2'></i> Registrar Trabajador" OnClick="lbRegistrar_Click"/>
                    </div>
                    <div class="p-2">
                        <asp:LinkButton ID="ListarTodos" CssClass="btn btn-success" runat="server" Text="Listar todos" OnClick="ListarTodos_Click" />
                    </div>
                </div>
            </div>
            <div class="container row">
                <asp:GridView ID="dgvEmpleados" runat="server" AutoGenerateColumns="false"
                    OnRowDataBound="dgvEmpleados_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="dgvEmpleados_PageIndexChanging" PageSize="10"
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
                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-edit pe-4'></i>" CommandArgument='<%# Eval("persona_id") %>' OnClick="lbModificar_Click" />
                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash pe-4'></i>" CommandArgument='<%# Eval("persona_id") %>' OnClick="lbEliminar_Click" />
                                <asp:LinkButton runat="server" Text="<i class='fa-solid fa-eye pe-4'></i>" CommandArgument='<%# Eval("persona_id") %>' OnClick="lbVisualizar_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>



