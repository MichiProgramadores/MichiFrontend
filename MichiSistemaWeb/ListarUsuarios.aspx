<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="ListarUsuarios.aspx.cs" Inherits="MichiSistemaWeb.ListarUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
        <h2>Listado de Usuarios</h2>
        <div class="container row">
            <div class="row align-items-center">
                <div class="col-auto">
                    <asp:Label ID="lblNombreID" CssClass="form-label" runat="server" Text="Ingrese el ID:"></asp:Label>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtNombreID" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="lbBuscar" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscar_Click" style="background-color: #FBCB43; border: none;" />
                </div>
      
                <div class="d-flex justify-content-end">
                    <div class="p-2">
                        <asp:LinkButton ID="lbRegistrar" CssClass="btn btn-success" runat="server"  Text="<i class='fa-solid fa-plus pe-2'></i> Registrar Usuario" OnClick="lbRegistrar_Click" style="background-color:  #FF7E5F; border: none;"/>
                    </div>

                </div>

            </div>
            <div class="container row">
                <asp:GridView ID="dgvUsuarios" runat="server" AutoGenerateColumns="false"
                    OnRowDataBound="dgvUsuarios_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="dgvUsuarios_PageIndexChanging" PageSize="10"
                    CssClass="table table-hover table-responsive table-striped">
                    <Columns>
                        <asp:BoundField HeaderText="ID" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField HeaderText="Nombre de usuario" ItemStyle-CssClass="align-middle" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbModificar" runat="server" Text="<i class='fa-solid fa-edit pe-4' style='color: #FF7E5F;'></i>" CommandArgument='<%# Eval("id") %>' OnClick="lbModificar_Click" />
                                                   
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>



            </div>
        </div>
    </div>
</asp:Content>
