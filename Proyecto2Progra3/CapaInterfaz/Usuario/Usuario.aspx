<%@ Page Title="" Language="C#" MasterPageFile="~/CapaInterfaz/Menu Master/Menu.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="Proyecto2Progra3.CapaInterfaz.Usuario.Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="max-width: 850px; min-width: 500px; float: left; margin-left: 100px; margin-right: 50px; margin-bottom: 500px;">
        <div class="card">
            <h3 class="card-header text-center">Usuarios</h3>
            <div class="card-body">
                <form id="form1" class="float-end">
                    <div class="row">
                        <div class="mb-3">
                            <label class="form-label">ID Usuario::</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtUsusarioID"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">ID Empleado:</label>
                            <asp:DropDownList ID="dropIdEmpleado" CssClass="form-control" class="form-control form-control-sm d-inline-block w-50" runat="server" EnableViewState="true" >
                            </asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Correo:</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtCorreo"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Contraseña:</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtContrasena" TextMode="Password"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Button ID="btnAgregar" class="btn btn-success" runat="server" Text="Agregar" OnClick="btnAgregar_Click"/>
                            <asp:Button ID="btnConsultar" runat="server" class="btn btn-info" Text="Consultar" Style="color: white;" OnClick="btnConsultar_Click1"/>
                            <asp:Button ID="btnEliminar" class="btn btn-danger" runat="server" Text="Borrar" OnClick="btnEliminar_Click1"/>
                            <asp:Button ID="btnModificar" runat="server" class="btn btn-dark" Text="Modificar" OnClick="btnModificar_Click"/>    
                            <asp:Button ID="Reload" class="btn btn-primary" runat="server" Text="Refrescar" OnClick="Reload_Click" />
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
            <h3 class="card-header text-center">Usuarios Registrados</h3>
            <div class="card-body">
                <hr />
                <!-- Línea divisoria -->
                <!-- GridView -->
                <br />
<asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" Width="1050px">
    <Columns>
        <asp:BoundField DataField="IDUsuario" HeaderText="ID Usuario" />
        <asp:BoundField DataField="IDEmpleado" HeaderText="ID Empleado" />
        <asp:BoundField DataField="Correo" HeaderText="Correo" />
        <asp:BoundField DataField="Contrasena" HeaderText="Contraseña" />
    </Columns>
</asp:GridView>
            </div>
        </div>
        <div>
        </div>
  </div>
</div>
    <!-- Script para formatear campos numéricos -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/inputmask/5.0.6/jquery.inputmask.min.js"></script>
    <!-- Script de validación para los botones de agregar y modificar -->
    <script>
        function validar() {
            var dropIdEmpleado = document.getElementById('<%= dropIdEmpleado.ClientID %>').value;
            var txtCorreo = document.getElementById('<%= txtCorreo.ClientID %>').value;
            var txtContrasena = document.getElementById('<%= txtContrasena.ClientID %>').value;

            if (dropIdEmpleado === '' || txtCorreo === '' || txtContrasena === '') {
                alert('Por favor, rellena todos los campos.');
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
