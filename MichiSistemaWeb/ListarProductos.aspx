<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="ListarProductos.aspx.cs" Inherits="MichiSistemaWeb.ListarProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
    <div class="container row">
        <div class="row align-items-center">
             <div class="col-auto">
                <asp:DropDownList ID="DdlTipoProducto" runat="server" CssClass="form-select" 
                  OnSelectedIndexChanged="DdlTipoProducto_SelectedIndexChanged" AutoPostBack="True">
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
                <asp:Label ID="lblID" CssClass="form-label" runat="server" Text="Ingrese el ID del producto:"></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:TextBox ID="txtID" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-2">
                <asp:LinkButton ID="lbBuscar" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscar_Click"/>
            </div>
            <div class="col text-end p-3">
                <asp:LinkButton ID="lbRegistrar" CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-plus pe-2'></i> Registrar Producto" OnClick="lbRegistrar_Click" />
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
                    <asp:BoundField HeaderText="Estado" ItemStyle-CssClass="align-middle" /> 
                    <asp:BoundField HeaderText="Tipo" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField HeaderText="Precio" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField HeaderText="Stock minimo" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField HeaderText="Stock actual" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField HeaderText="Unidad de medida" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField HeaderText="Descripción" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField HeaderText="Estado" ItemStyle-CssClass="align-middle" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" Text="<i class='fa-solid fa-edit pe-4'></i>" CommandArgument='<%# Eval("producto_id") %>' OnClick="lbModificar_Click" />
                            <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash pe-4'></i>" CommandArgument='<%# Eval("producto_id") %>' OnClick="lbEliminar_Click" />
                            <asp:LinkButton runat="server" Text="<i class='fa-solid fa-eye pe-4'></i>" CommandArgument='<%# Eval("producto_id") %>' OnClick="lbVisualizar_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>
</asp:Content>
