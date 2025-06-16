<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="ListarEventos.aspx.cs" Inherits="MichiSistemaWeb.ListarEventos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Listar Eventos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
     <div class="container">
     <div class="container row">
         <div class="row align-items-center">
             <div class="col-auto">
                 <asp:Label ID="lblNombre" CssClass="form-label" runat="server" Text="Ingrese el ID del evento:"></asp:Label>
             </div>
             <div class="col-sm-3">
                 <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
             </div>
             <div class="col-sm-2">
                 <asp:LinkButton ID="lbBuscar" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscar_Click" />
             </div>
             <div class="col text-end p-3">
                 <asp:LinkButton ID="lbRegistrar" CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-plus pe-2'></i> Registrar Evento" OnClick="lbRegistrar_Click" />
             </div>
         </div>
    <div class="container">
        <div class="table-responsive">
            <asp:GridView ID="dgvEventos" runat="server" AutoGenerateColumns="false"
                OnRowDataBound="dgvEventos_RowDataBound" AllowPaging="true"
                OnPageIndexChanging="dgvEventos_PageIndexChanging" PageSize="10"
                Width="100%"
                CssClass="table table-hover table-striped">
                <Columns>
                    <asp:BoundField DataField="evento_id" HeaderText="ID" ItemStyle-CssClass="align-middle" />

                    <asp:BoundField DataField="fechaInicio" HeaderText="Fecha Inicio" ItemStyle-CssClass="align-middle"  DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="fechaFin" HeaderText="Fecha Fin" ItemStyle-CssClass="align-middle"  DataFormatString="{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-CssClass="align-middle" />
                    <asp:BoundField DataField="tipoEvento" HeaderText="Tipo Evento" ItemStyle-CssClass="align-middle" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" Text="<i class='fa-solid fa-edit pe-4'></i>"
                                CommandArgument='<%# Eval("evento_id") %>' OnClick="lbModificar_Click" />
                            <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash pe-4'></i>"
                                CommandArgument='<%# Eval("evento_id") %>' OnClick="lbEliminar_Click" />
                            <asp:LinkButton runat="server" Text="<i class='fa-solid fa-eye pe-4'></i>"
                                CommandArgument='<%# Eval("evento_id") %>' OnClick="lbVisualizar_Click" />

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>