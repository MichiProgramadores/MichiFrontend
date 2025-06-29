<%@ Page Title="" Language="C#" MasterPageFile="~/Michi.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MichiSistemaWeb.Home" %>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Title" runat="server">
    Home
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="cph_Contenido" runat="server">
    <asp:Literal ID="LiteralMaxDate" runat="server" Visible="false"></asp:Literal>
    <h2>
        Bienvenido al Michi Sistema - ¿Qué quieres hacer hoy?
    </h2>

     <!-- Aquí esta todo lo de reporte -->
    <div class="text-center align-items-center" style="text-align: center; margin-top: 20px;">
        <div class="text-center justify-content-center">
            <div class="p-2" style="display: inline-block; margin-right: 10px;">
                <asp:LinkButton ID="LinkButton5" runat="server" 
                    OnClientClick="$('#tipoTrabajadorModal').modal('show'); return false;" 
                    style="background-color: #FBCB43; color: black; padding: 10px 20px; font-size: 16px; border: none; 
                    cursor: pointer; text-align: center; width: 200px; border-radius: 50px; text-decoration: none;">
                    <i class="fa-solid fa-file-earmark-plus"></i> Obtener reporte de trabajadores
                </asp:LinkButton>
            </div>
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Modal de selección de tipo de trabajador -->
            <div class="modal fade" id="tipoTrabajadorModal" tabindex="-1" role="dialog" aria-labelledby="tipoTrabajadorModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="tipoTrabajadorModalLabel">Opciones de reporte</h5>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="ddlTipoTrabajador">Seleccione el tipo de trabajador:</label>
                                <asp:DropDownList ID="ddlTipoTrabajador" runat="server" CssClass="form-select"
                                    OnSelectedIndexChanged="ddlTipoTrabajador_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="GENERAL" Value="GENERAL" />
                                    <asp:ListItem Text="DESPACHADOR" Value="DESPACHADOR" />
                                    <asp:ListItem Text="DELIVERY" Value="DELIVERY" />
                                    <asp:ListItem Text="ASISTENTE" Value="ASISTENTE" />
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="ddlEstado">Seleccione el estado::</label>
                                <asp:DropDownList ID="ddlEstadoTrabajador" runat="server" CssClass="form-select"
                                    OnSelectedIndexChanged="ddlEstadoTrabajador_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="CUALQUIERA" Value="CUALQUIERA" />
                                    <asp:ListItem Text="ACTIVOS" Value="ACTIVO" />
                                    <asp:ListItem Text="INACTIVOS" Value="INACTIVO" />
                                </asp:DropDownList>
                            </div>
                            <label>Trabajador específico:</label>
                            <div class="row align-items-center">
                                <div class="col-sm-1">
                                    <asp:Label ID="Label10" CssClass="form-label" runat="server" Text="ID: "></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="IDREPORTE" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Label ID="Label11" CssClass="form-label" runat="server" Text="Nombre: "></asp:Label>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="NOMBREREPORTE" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalTrabajadores" style="background-color:  #FF7E5F; border: none; color: black;"> 
                                        <i class="fas fa-search"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3">
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LinkButtonLimpiar" CssClass="btn btn-warning" runat="server" OnClick="LinkButtonLimpiar_Click">Limpiar
                            </asp:LinkButton>
                            <button type="button" class="btn btn-secondary" onclick="$('#tipoTrabajadorModal').modal('hide')">Cerrar</button >
                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server" style="background-color:  #FF7E5F; border: none; color: black;" OnClick="LinkButton1_Click">Obtener reporte</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal de selección de trabajador -->
            <div class="modal fade" id="modalTrabajadores" tabindex="-1" role="dialog" aria-labelledby="modalTrabajadoresLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="modalTrabajadoresLabel">Seleccionar Empleado</h5>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="dgvTrabajadoresEleccion" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="persona_id" HeaderText="ID" />
                                    <asp:BoundField DataField="nombres" HeaderText="Nombre" SortExpression="nombres" />
                                    <asp:BoundField DataField="apellidos" HeaderText="Apellido" SortExpression="apellidos" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSeleccionarTrabajador" runat="server" CssClass="btn btn-primary btn-sm"
                                                OnClick="btnSeleccionarTrabajador_Click" CommandArgument='<%# Eval("persona_id") %>'> <i class="fas fa-check"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LinkButtonCerrarModal2" runat="server" CssClass="btn btn-secondary"
                                OnClick="LinkButtonCerrarModal2_Click">Cerrar</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
             <!-- Modal ERROR -->
            <div class="modal fade" id="errorModal" tabindex="-1" aria-labelledby="errorModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header bg-danger text-white">
                            <h5 class="modal-title" id="errorModalLabel">
                                <i class="bi bi-exclamation-triangle-fill me-2"></i>Error
                            </h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblMensajeError" runat="server" Text="No hay datos para mostrar en el reporte" CssClass="form-text text-danger"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="BotonErrorModal" runat="server" CssClass="btn btn-secondary"
                                 OnClick="BotonErrorModal_Click">Cerrar</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LinkButton1" />
            <asp:AsyncPostBackTrigger ControlID="ddlTipoTrabajador" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlEstadoTrabajador" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- Aquí esta todo lo de reporte facturacion-->
    <div class="text-center align-items-center" style="text-align: center; margin-top: 20px;">
        <div class="text-center justify-content-center">
            <div class="p-2" style="display: inline-block;">
                <asp:LinkButton ID="LinkButton2" runat="server" 
                    OnClientClick="$('#reporteFacturacionModal').modal('show'); return false;" 
                    style="background-color: #F1F1F1; color: black; padding: 10px 20px; font-size: 16px; border: none; 
                    cursor: pointer; text-align: center; width: 200px; border-radius: 50px; text-decoration: none;">
                    <i class="fa-solid fa-file-earmark-plus"></i> Obtener reporte de facturación
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <!-- Modal de selección de tipo de facturación -->
        <div class="modal fade" id="reporteFacturacionModal" tabindex="-1" role="dialog" aria-labelledby="tipoTrabajadorModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="reporteFacturacionModalLabel">Reporte de facturación</h5>
                    </div>
                    <label>  Si no se especifica la fecha, se emitirá el reporte del mes actual:</label>
                    <div class="modal-body">
                        <div class="mb-3 row">
                            <asp:Label ID="lblFechaInicio" runat="server" Text="*Inicio: " CssClass="col-sm-2 col-form-label"></asp:Label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date" MinDate="2020-01-01" MaxDate="<%: LiteralMaxDate.Text %>" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <asp:Label ID="lblFechaFin" runat="server" Text="*Fin: " CssClass="col-sm-2 col-form-label"></asp:Label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" TextMode="Date" MinDate="2020-01-01" MaxDate="<%: LiteralMaxDate.Text %>" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer justify-content-between">
                        <asp:LinkButton ID="BotonCerrar" CssClass="btn btn-warning" runat="server" OnClick="BotonCerrar_Click" style="flex: 1; text-align: center;">Cerrar</asp:LinkButton>
                        <asp:LinkButton ID="BotonReporteFacturacion" CssClass="btn" runat="server" style="background-color: #FF7E5F; border: none; color: black; flex: 1; text-align: center;" OnClick="BotonReporteFacturacion_Click">Obtener reporte</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="BotonReporteFacturacion" />
    </Triggers>
    </asp:UpdatePanel>


    <!-- Aquí esta todo lo de reporte rentas -->
    <div class="text-center align-items-center" style="text-align: center; margin-top: 20px;">
        <div class="text-center justify-content-center">
            <div class="p-2" style="display: inline-block;">
                <asp:LinkButton ID="LinkButton3" runat="server" 
                    OnClientClick="$('#reporteRentasModal').modal('show'); return false;" 
                    style="background-color: #FBCB43; color: black; padding: 10px 20px; font-size: 16px; border: none; 
                    cursor: pointer; text-align: center; width: 200px; border-radius: 50px; text-decoration: none;">
                    <i class="fa-solid fa-file-earmark-plus"></i> Obtener reporte de rentas
                </asp:LinkButton>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <!-- Modal de selección de tipo de rentas -->
            <div class="modal fade" id="reporteRentasModal" tabindex="-1" role="dialog" aria-labelledby="tipoTrabajadorModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="reporteRentasModalLabel">Reporte de rentas</h5>
                        </div>

                        <label>  Si no se específica la fecha, se emitirá el reporte del mes actual:</label>
                        <div class="modal-body">
                            <div class="mb-3 row">
                                <asp:Label ID="Label1" runat="server" Text="*Inicio: " CssClass="col-sm-2 col-form-label"></asp:Label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtFechaRentaIni" runat="server" CssClass="form-control" TextMode="Date" 
                                        MinDate="2020-01-01" MaxDate="<%: LiteralMaxDate.Text %>" />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <asp:Label ID="Label2" runat="server" Text="*Fin: " CssClass="col-sm-2 col-form-label"></asp:Label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtFechaRentaFin" runat="server" CssClass="form-control" TextMode="Date" 
                                        MinDate="2020-01-01" MaxDate="<%: LiteralMaxDate.Text %>" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="BotonCerrarRenta" CssClass="btn btn-warning" runat="server" OnClick="BotonCerrarRenta_Click">Cerrar</asp:LinkButton>
                            <asp:LinkButton ID="BotonReporteRenta" CssClass="btn btn-primary" runat="server" style="background-color:  #FF7E5F; border: none; color: black;" OnClick="BotonReporteRenta_Click">Obtener reporte</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BotonReporteRenta" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        window.onload = function () {
            var minDate = new Date('2020-01-01'); // Fecha mínima: 1 de enero de 2020
            var currentDate = new Date(); // Fecha actual

            // Obtén los controles de fecha
            var txtFechaRentaIni = document.getElementById('<%= txtFechaRentaIni.ClientID %>');
        var txtFechaRentaFin = document.getElementById('<%= txtFechaRentaFin.ClientID %>');
    var txtFechaInicio = document.getElementById('<%= txtFechaInicio.ClientID %>');
    var txtFechaFin = document.getElementById('<%= txtFechaFin.ClientID %>');

            // Establece las fechas mínima y máxima para cada uno
            txtFechaRentaIni.setAttribute('min', minDate.toISOString().split('T')[0]);
            txtFechaRentaIni.setAttribute('max', currentDate.toISOString().split('T')[0]);

            txtFechaRentaFin.setAttribute('min', minDate.toISOString().split('T')[0]);
            txtFechaRentaFin.setAttribute('max', currentDate.toISOString().split('T')[0]);

            txtFechaInicio.setAttribute('min', minDate.toISOString().split('T')[0]);
            txtFechaInicio.setAttribute('max', currentDate.toISOString().split('T')[0]);

            txtFechaFin.setAttribute('min', minDate.toISOString().split('T')[0]);
            txtFechaFin.setAttribute('max', currentDate.toISOString().split('T')[0]);

            // Validación adicional para los cambios de fecha
            txtFechaRentaIni.addEventListener('change', function () {
                var selectedDate = new Date(txtFechaRentaIni.value);
                if (selectedDate < minDate) {
                    alert('La fecha de renta inicio no puede ser anterior a 1 de enero de 2020.');
                    txtFechaRentaIni.value = '';  // Resetear el valor
                }
                if (selectedDate > currentDate) {
                    alert('La fecha de renta inicio no puede ser posterior a la fecha actual.');
                    txtFechaRentaIni.value = '';  // Resetear el valor
                }
            });

            txtFechaRentaFin.addEventListener('change', function () {
                var selectedDate = new Date(txtFechaRentaFin.value);
                if (selectedDate < minDate) {
                    alert('La fecha de renta fin no puede ser anterior a 1 de enero de 2020.');
                    txtFechaRentaFin.value = '';  // Resetear el valor
                }
                if (selectedDate > currentDate) {
                    alert('La fecha de renta fin no puede ser posterior a la fecha actual.');
                    txtFechaRentaFin.value = '';  // Resetear el valor
                }
            });

            txtFechaInicio.addEventListener('change', function () {
                var selectedDate = new Date(txtFechaInicio.value);
                if (selectedDate < minDate) {
                    alert('La fecha de inicio no puede ser anterior a 1 de enero de 2020.');
                    txtFechaInicio.value = '';  // Resetear el valor
                }
                if (selectedDate > currentDate) {
                    alert('La fecha de inicio no puede ser posterior a la fecha actual.');
                    txtFechaInicio.value = '';  // Resetear el valor
                }
            });

            txtFechaFin.addEventListener('change', function () {
                var selectedDate = new Date(txtFechaFin.value);
                if (selectedDate < minDate) {
                    alert('La fecha de fin no puede ser anterior a 1 de enero de 2020.');
                    txtFechaFin.value = '';  // Resetear el valor
                }
                if (selectedDate > currentDate) {
                    alert('La fecha de fin no puede ser posterior a la fecha actual.');
                    txtFechaFin.value = '';  // Resetear el valor
                }
            });
        };
    </script>
</asp:Content>
