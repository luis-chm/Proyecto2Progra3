<%@ Page Title="Gestión de Productos" Language="C#" EnableEventValidation="false"  MasterPageFile="~/CapaInterfaz/Menu Master/Menu.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="Proyecto2Progra3.Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
<div class="container" style="max-width: 1050px; min-width: 500px; float: left; margin-right: 50px;"">
  <div class="card">
    <h3 class="card-header text-center">Agregar Producto</h3>
    <div class="card-body">
      <form id="myForm" class="float-end">
        <div class="row">
         
          <div class="col-md-6">
            <div class="form-group">
              <div class="mb-3">
                <label class="form-label">ID Producto</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtIdProducto"></asp:TextBox>
              </div>
              <div class="mb-3">
                <label class="form-label">ID Categoría: </label>            
                  <asp:DropDownList ID="dropCategorias" CssClass="form-control" class="form-control form-control-sm d-inline-block w-50" runat="server">
               </asp:DropDownList>
              </div>             
                <div class="mb-3">
                  <label class="form-label">ID Empleado</label>         
                    <asp:DropDownList ID="dropIdEmpleado" CssClass="form-control" class="form-control form-control-sm d-inline-block w-50" runat="server">
               </asp:DropDownList>
                </div>
              <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre"></asp:TextBox>
              </div>    
              <div class="mb-3">
                <label class="form-label">Marca </label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtMarca"></asp:TextBox>
              </div>                      
            </div>
          </div>
          <!-- Segunda columna -->
          <div class="col-md-6">
            <div class="form-group">       
                <div class="mb-3">
                <label class="form-label">Stock</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtStock"></asp:TextBox>
              </div> 
                <div class="mb-3">
                <label class="form-label">Precio de Compra</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPrecioCompra"></asp:TextBox>
              </div>   
                <div class="mb-3">
                <label class="form-label">Precio de Venta</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPrecioVenta"></asp:TextBox>
              </div> 
              <div class="mb-3">
                <label class="form-label">Fecha de Vencimiento</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtFechaVencimiento" TextMode="Date"></asp:TextBox>
              </div> 
              <div class="mb-3">
                <label class="form-label">Imagen: </label>
                <asp:FileUpload ID="fuImagen" runat="server" CssClass="form-control"/><br />
              </div> 
              <asp:Button ID="btnAgregar" class="btn btn-success" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
              <asp:Button ID="btnConsultar" runat="server" class="btn btn-info" Text="Consultar"  style="color: white;" OnClick="btnBuscar_Click" />
              <asp:Button ID="btnEliminar" class="btn btn-danger" runat="server" Text="Borrar" OnClick="btnEliminar_Click"  />
              <asp:Button ID="btnModificar" runat="server" class="btn btn-dark" Text="Modificar" OnClick="btnModificar_Click"  />
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</div>
        <!-- Mensaje de estado -->
        <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
        <!-- Fuera del formulario -->
<div class="col-md-6" style="float: right;">
  <div class="card">
     <h3 class="card-header text-center">Productos Registrados</h3>  
    <div class="card-body">
      <hr /> <!-- Línea divisoria -->
      <!-- GridView -->
      <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" Width="1050px">
        <Columns>
            <asp:BoundField DataField="IdProducto" HeaderText="ID Producto" />
            <asp:BoundField DataField="IdCategoria" HeaderText="ID Categoria" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Marca" HeaderText="Marca" />
            <asp:BoundField DataField="Stock" HeaderText="Stock" />
            <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra" />
            <asp:BoundField DataField="PrecioVenta" HeaderText="Precio Venta" />
            <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:yyyy-MM-dd}" />
             <asp:BoundField DataField="IdEmpleado" HeaderText="ID Empleado" />
            <asp:TemplateField HeaderText="Imagen">
                <ItemTemplate>
                    <asp:Image ID="imgProducto" runat="server"
                        Height="100px"
                        Width="100px"
                        ImageUrl='<%# Eval("Imagen") != DBNull.Value ? "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("Imagen")) : "" %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </div>
  </div>
          <asp:Button ID="btnRefrescar" class="btn btn-primary btn-sm" runat="server" Text="Refrescar" OnClick="btnRefrescar_Click"/>
    </div>
  </div>
</div>
    <!-- Script para formatear campos numéricos -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/inputmask/5.0.6/jquery.inputmask.min.js"></script>
<!-- Script de validación para los botones de agregar y modificar -->
<script>
    function validar() {
        var dropCategorias = document.getElementById('<%= dropCategorias.ClientID %>').value;
        var txtNombre = document.getElementById('<%= txtNombre.ClientID %>').value;
        var txtMarca = document.getElementById('<%= txtMarca.ClientID %>').value;
        var txtStock = document.getElementById('<%= txtStock.ClientID %>').value;
        var txtPrecioCompra = document.getElementById('<%= txtPrecioCompra.ClientID %>').value;
        var txtPrecioVenta = document.getElementById('<%= txtPrecioVenta.ClientID %>').value;
        var txtFechaVencimiento = document.getElementById('<%= txtFechaVencimiento.ClientID %>').value;
        var dropIdEmpleado = document.getElementById('<%= dropIdEmpleado.ClientID %>').value;
        if (dropCategorias === '' || txtNombre === '' || txtMarca === '' || txtStock === '' || txtPrecioCompra === '' || txtPrecioVenta === '' || txtFechaVencimiento === '' || txtIdEmpleado === '') {
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