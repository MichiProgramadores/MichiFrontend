<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="ListarOrdenes.aspx.cs" Inherits="MichiSistemaWeb.ListarOrdenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container">
        <div class="container row">
            <div class="row align-items-center">
                <div class="col-auto">
                    <asp:Label ID="lblNombre" CssClass="form-label" runat="server" Text="Ingrese el ID de la orden:"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="lbBuscar" CssClass="btn btn-info" runat="server" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscar_Click" />
                </div>
                <div class="col text-end p-3">
                    <asp:LinkButton ID="lbRegistrar" CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-plus pe-2'></i> Registrar Orden" OnClick="lbRegistrar_Click" />
                </div>
            </div>
            <div class="container">
                <div class="table-responsive">
                    <asp:GridView ID="dgvOrdenes" runat="server" AutoGenerateColumns="false"
                        OnRowDataBound="dgvOrdenes_RowDataBound" AllowPaging="true"
                        OnPageIndexChanging="dgvOrdenes_PageIndexChanging" PageSize="13"
                        Width="100%"
                        CssClass="table table-hover table-striped">
                        <Columns>
                            <asp:BoundField DataField="idOrden" HeaderText="ID" ItemStyle-CssClass="align-middle" />
                            <asp:BoundField DataField="tipoRecepcion" HeaderText="Tipo recepcion" ItemStyle-CssClass="align-middle" />
                            <asp:BoundField DataField="setUpPersonalizado" HeaderText="Set up personalizado" ItemStyle-CssClass="align-middle" />
                            <asp:BoundField DataField="totalPagar" HeaderText="Monto total" ItemStyle-CssClass="align-middle" />
                            <asp:BoundField DataField="saldo" HeaderText="Saldo" ItemStyle-CssClass="align-middle" />
                            <asp:BoundField DataField="cantDias" HeaderText="Cantidad dias" ItemStyle-CssClass="align-middle" />
                            
                            <asp:BoundField DataField="fecha_devolucion" HeaderText="Fecha devolución" ItemStyle-CssClass="align-middle" DataFormatString="{0:dd/MM/yyyy}"/>
                            <asp:BoundField DataField="fecha_emisión" HeaderText="Fecha emisión" ItemStyle-CssClass="align-middle" DataFormatString="{0:dd/MM/yyyy}"  />
                            <asp:BoundField DataField="fecha_registro" HeaderText="Fecha registro" ItemStyle-CssClass="align-middle" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="fecha_entrega" HeaderText="Fecha entrega" ItemStyle-CssClass="align-middle" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="tipoEstadoDevolucion" HeaderText="Tipo estado devolución" ItemStyle-CssClass="align-middle" />
                            <asp:BoundField DataField="clienteID" HeaderText="Id cliente" ItemStyle-CssClass="align-middle" />
                            <asp:BoundField DataField="trabajadorID" HeaderText="Id trabajador" ItemStyle-CssClass="align-middle" />

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" Text="<i class='fa-solid fa-edit'></i> Modificar" CssClass="btn btn-warning" CommandArgument='<%# Eval("idOrden") %>' OnClick="lbModificar_Click" />
                                    <asp:LinkButton runat="server" Text="<i class='fa-solid fa-trash'></i> Eliminar" CssClass="btn btn-danger" CommandArgument='<%# Eval("idOrden") %>' OnClick="lbEliminar_Click" />
                                    <asp:LinkButton runat="server" Text="<i class='fa-solid fa-eye'></i> Ver" CssClass="btn btn-info" CommandArgument='<%# Eval("idOrden") %>' OnClick="lbVisualizar_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
