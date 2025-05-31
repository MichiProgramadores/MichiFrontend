<%@ Page Title="" Language="C#" MasterPageFile="~/SoftProg.Master" AutoEventWireup="true" CodeBehind="ListarAreas.aspx.cs" Inherits="SoftProgWA.ListarAreas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Listar Areas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
        <div class="container row">
            <div class="row align-items-center">
                <div class="col text-end p-3">
                    <asp:LinkButton ID="btnRegistrar" CssClass="btn btn-success" runat="server" OnClick="btnRegistrar_Click" Text="<i class='fa-solid fa-plus pe-2'></i> Registrar Area" />
                </div>
            </div>
        </div>
        <div class="container row">
            <asp:GridView ID="gvAreas" runat="server" CssClass="table table-hover table-responsive table-striped" AutoGenerateColumns="false" PageSize="7" AllowPaging="true" OnPageIndexChanging="gvAreas_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="Id. del Area" DataField="IdArea" />
                    <asp:BoundField HeaderText="Nombre del Area" DataField="Nombre" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" Text="<i class='fa-solid fa-pen pe-4'></i>" OnClick="btnModificar_Click" CommandArgument='<%# Eval("IdArea") %>' />
                            <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash'></i>" OnClick="btnEliminar_Click" CommandArgument='<%# Eval("IdArea") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    
</asp:Content>
