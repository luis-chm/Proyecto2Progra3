<%@ Page Title="" Language="C#" MasterPageFile="~/CapaInterfaz/Menu Master/Menu.Master" enableEventValidation="false" AutoEventWireup="true" CodeBehind="~/CapaInterfaz/Cliente/Cliente.aspx.cs" Inherits="Proyecto2Progra3.CapaInterfaz.Cliente.Cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="max-width: 850px; min-width: 500px; float: left; margin-left: 100px; margin-right: 50px; margin-bottom: 500px;">
        <div class="card">
            <h3 class="card-header text-center">Registrar Cliente</h3>
            <div class="card-body">
                <form id="form1" class="float-end">
                    <div class="row">
                        <!-- Primera columna -->
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="mb-3">
                                    <label class="form-label">ID Cliente: </label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtIdCliente"></asp:TextBox>
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
                            </div>
                        </div>
                        <!-- Segunda columna -->
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="mb-3">
                                    <label class="form-label">Dirección: </label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtDireccion"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Telefono: </label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtTelefono" MaxLength="10"></asp:TextBox>
                                    <br />
                                    <br />
                                    <hr />
                                    <!-- Línea divisoria -->
                                </div>
                                <asp:Button ID="btnAgregar" class="btn btn-success" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                                <asp:Button ID="btnConsultar" runat="server" class="btn btn-info" Text="Consultar" Style="color: white;" OnClick="btnBuscar_Click" />
                                <asp:Button ID="btnEliminar" class="btn btn-danger" runat="server" Text="Borrar" OnClick="btnEliminar_Click" />
                                <asp:Button ID="btnModificar" runat="server" class="btn btn-dark" Text="Modificar" OnClick="btnModificar_Click" />
                            </div>
                        </div>
                    </div>
            </div>
            </form>
        </div>
    </div>

    <!-- Fuera del formulario -->
    <div class="col-md-6" style="float: right;">
        <div class="card">
            <h3 class="card-header text-center">Clientes Registrados</h3>
            <div class="card-body">
                <hr />
                <!-- Línea divisoria -->
                <!-- GridView -->
                <br />
                <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" Width="1050px">
                    <Columns>
                        <asp:BoundField DataField="IdCliente" HeaderText="ID Cliente" />
                        <asp:BoundField DataField="DNI" HeaderText="Cedula" />
                        <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                        <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <asp:Button ID="btnReload" class="btn btn-primary btn-sm" runat="server" Text="Refrescar" OnClick="btnReload_Click"/>
    </div>
  </div>
</div>

<!-- Script de validación para los botones de agregar y modificar -->
<script>
    function validarCampos() {
        var txtDNI = document.getElementById('<%= txtDNI.ClientID %>').value;
        var txtNombres = document.getElementById('<%= txtNombres.ClientID %>').value;
        var txtApellidos = document.getElementById('<%= txtApellidos.ClientID %>').value;
        var txtDireccion = document.getElementById('<%= txtDireccion.ClientID %>').value;
        var txtTelefono = document.getElementById('<%= txtTelefono.ClientID %>').value;

        if (txtDNI === '' || txtNombres === '' || txtApellidos === '' || txtDireccion === '' || txtTelefono === '') {
            alert('Por favor, rellena todos los campos.');
            return false;
        }
        //Si todos los campos están llenos, continúa con la operación
        return true;
    }

    document.getElementById('<%= btnAgregar.ClientID %>').onclick = function() {
        return validarCampos();
    };

    document.getElementById('<%= btnModificar.ClientID %>').onclick = function () {
        return validarCampos();
    };
</script>

</asp:Content>
