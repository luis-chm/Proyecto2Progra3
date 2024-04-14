<%@ Page Title="" Language="C#" MasterPageFile="~/CapaInterfaz/Menu Master/Menu.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="~/CapaInterfaz/Cargo/Cargo.aspx.cs" Inherits="Proyecto2Progra3.CapaInterfaz.Cargo.Cargo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="max-width: 1550px; min-width: 800px;">
  <div class="row">
    <!-- Columna para el formulario de entrada de datos -->
    <div class="col-md-6">
      <div class="card">
        <h3 class="card-header text-center">Catalogo de Cargos</h3>
        <div class="card-body">
          <form id="myForm" onsubmit="return validar()">
            <div class="mb-3">
              <label class="form-label">Descripcion</label>
              <asp:TextBox runat="server" CssClass="form-control" ID="txtDescCargo"></asp:TextBox>
              <asp:Button ID="btnAgregar" class="btn btn-success" runat="server" Text="Agregar" OnClick="btnAgregar_Click"/>
            </div>
            <div class="mb-3">
              <label class="form-label">ID de Cargo: </label>
              <asp:TextBox runat="server" CssClass="form-control" ID="txtIdCargo"></asp:TextBox>
              <asp:Button ID="btnConsultar" runat="server" class="btn btn-info" Text="Consultar" style="color: white;" OnClick="btnConsultar_Click"/>
              <asp:Button ID="btnEliminar" class="btn btn-danger" runat="server" Text="Borrar" OnClick="btnEliminar_Click" />
              <asp:Button ID="btnModificar" runat="server" class="btn btn-dark" Text="Modificar" OnClick="btnModificar_Click"/>
            </div>
          </form>
        </div>
      </div>
    </div>
    <!-- Columna para el GridView -->
    <div class="col-md-6">
      <div class="card">
        <h3 class="card-header text-center">Cargos Registradas</h3>
        <div class="card-body">
          <hr /> <!-- Línea divisoria -->
          <!-- GridView -->
          <asp:GridView ID="gvCargo" runat="server" AutoGenerateColumns="False" Width="650px">
            <Columns>
              <asp:BoundField DataField="IdCargo" HeaderText="ID Cargo" />
              <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            </Columns>
          </asp:GridView>
        </div>
      </div>
        <asp:Button ID="Button1" class="btn btn-primary btn-sm" runat="server" Text="Refrescar" OnClick="btnRefrescar1_Click"/>
    </div>
  </div>
</div>

    <script>
        function validar() {
        var txtDescripcion = document.getElementById('<%= txtDescCargo.ClientID %>').value;

        if (txtDescripcion.trim() === '') {
            alert('Por favor, rellena todos los campos.');
            return false; // Detiene la ejecución
        }
        // Si todos los campos están llenos, continúa con la operación
        return true;
    }

    document.getElementById('<%= btnAgregar.ClientID %>').onclick = function() {
        return validar();
    };

    document.getElementById('<%= btnModificar.ClientID %>').onclick = function() {
        return validar();
    };
    </script>
</asp:Content>
