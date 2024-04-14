<%@ Page Title="" Language="C#" MasterPageFile="~/CapaInterfaz/Menu Master/Menu.Master" AutoEventWireup="true" CodeBehind="Empleado.aspx.cs" Inherits="Proyecto2Progra3.Interfaz.Empleado" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="max-width: 1050px; min-width: 500px; float: left; margin-left: 10px; margin-right: 50px; margin-bottom: 500px;">
        <div class="card">
            <h3 class="card-header text-center">Registrar Empleado</h3>
            <div class="card-body">
                <form id="form1" class="float-end">
                    <div class="row">
                        <!-- Primera columna -->
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="mb-3">
                                    <label class="form-label">ID Empleado</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtId"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">ID Cargo</label>
                                    <asp:DropDownList ID="dropCargo" CssClass="form-control" class="form-control form-control-sm d-inline-block w-50" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">N° Cedula: </label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtDNI"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Nombre: </label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNombres"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Apellidos:  </label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtApellidos"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Sexo </label>
                                    <asp:DropDownList CssClass="form-control" ID="ddlSexo" runat="server">
                                        <asp:ListItem Text="M" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="F" Value="F"></asp:ListItem>
                                    </asp:DropDownList><br />
                                </div>
                            </div>
                        </div>
                        <!-- Segunda columna -->
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="mb-3">
                                    <label class="form-label">Fecha Nacimiento </label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtFechaNacimiento" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Dirección: </label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtDireccion"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Estado Civil: </label>
                                    <asp:DropDownList CssClass="form-control" ID="DDLEstadoCivil" runat="server">
                                        <asp:ListItem Text="Ninguno" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Soltero" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="Unión Libre" Value="U"></asp:ListItem>
                                        <asp:ListItem Text="Casado" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="Viudo" Value="V"></asp:ListItem>
                                        <asp:ListItem Text="Divorciado" Value="D"></asp:ListItem>
                                    </asp:DropDownList><br />
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Imagen: </label>
                                    <asp:FileUpload ID="fuImagen" runat="server" CssClass="form-control" /><br />
                                </div>
                                <hr />
                                <!-- Línea divisoria -->
                                <asp:Button ID="btnAgregar" class="btn btn-success" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                                <asp:Button ID="btnConsultar" runat="server" class="btn btn-info" Text="Consultar" Style="color: white;" OnClick="btnBuscar_Click" />
                                <asp:Button ID="btnEliminar" class="btn btn-danger" runat="server" Text="Borrar" OnClick="btnEliminar_Click" />
                                <asp:Button ID="btnModificar" runat="server" class="btn btn-dark" Text="Modificar" OnClick="btnModificar_Click" />
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!-- Fuera del formulario -->
    <div class="col-md-6" style="float: right;">
        <div class="card">
            <h3 class="card-header text-center">Empleados Registrados</h3>
            <div class="card-body">
                <hr />
                <!-- Línea divisoria -->
                <!-- GridView -->
                <asp:GridView ID="gvEmpleados" runat="server" AutoGenerateColumns="False" Width="1050">
                    <Columns>
                        <asp:BoundField DataField="IdEmpleado" HeaderText="ID Empleado" />
                        <asp:BoundField DataField="DNI" HeaderText="Cedula" />
                        <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                        <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                        <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                        <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha de Nacimiento" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                        <asp:BoundField DataField="EstadoCivil" HeaderText="Estado Civil" />
                        <asp:BoundField DataField="IdCargo" HeaderText="ID Cargo" />
                        <asp:TemplateField HeaderText="Imagen">
                            <ItemTemplate>
                                <asp:Image ID="imgEmpleado" runat="server" Height="100" Width="100" ImageUrl='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("Imagen")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <asp:Button ID="btnRefrescar" class="btn btn-primary btn-sm" runat="server" Text="Refrescar" OnClick="btnRefrescar_Click"/>
    </div>
    <script>
        function validar() {
            var txtDNI = document.getElementById('<%= txtDNI.ClientID %>').value;
            var txtNombres = document.getElementById('<%= txtNombres.ClientID %>').value;
            var txtApellidos = document.getElementById('<%= txtApellidos.ClientID %>').value;
            var ddlSexo = document.getElementById('<%= ddlSexo.ClientID %>').value;
            var txtFechaNacimiento = document.getElementById('<%= txtFechaNacimiento.ClientID %>').value;
            var txtDireccion = document.getElementById('<%= txtDireccion.ClientID %>').value;
            var DDLEstadoCivil = document.getElementById('<%= DDLEstadoCivil.ClientID %>').value;

            // Obtener el valor seleccionado del campo IdCargo
            var dropCargo = document.getElementById('<%= dropCargo.ClientID %>');
            var idCargo = dropCargo.value;

            if (txtDNI === '' || txtNombres === '' || txtApellidos === '' || ddlSexo === '' || txtFechaNacimiento === '' || txtDireccion === '' || DDLEstadoCivil === '' || idCargo === '') {
                alert('Por favor, rellena todos los campos, incluyendo el campo de cargo.');
                return false; // Detiene la ejecución
            }
            // Si todos los campos están llenos, continúa con la operación
            return true;
        }

        document.getElementById('<%= btnAgregar.ClientID %>').onclick = function () {
            return validar();
        };

        document.getElementById('<%= btnModificar.ClientID %>').onclick = function () {
            return validar();
        };

    </script>
</asp:Content>
