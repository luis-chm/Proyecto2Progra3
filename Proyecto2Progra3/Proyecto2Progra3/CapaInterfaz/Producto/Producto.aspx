<%@ Page Title="Gestión de Productos" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="Proyecto2Progra3.Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Aquí podrías agregar CSS o JavaScript si es necesario -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h2>Agregar Producto</h2>
        <asp:Label ID="lblIdproducto" runat="server" Text="ID Producto:"></asp:Label>
        <asp:TextBox ID="txtIdProducto" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblIdCategoria" runat="server" Text="ID Categoría:"></asp:Label>
        <asp:TextBox ID="txtIdCategoria" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblMarca" runat="server" Text="Marca:"></asp:Label>
        <asp:TextBox ID="txtMarca" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblStock" runat="server" Text="Stock:"></asp:Label>
        <asp:TextBox ID="txtStock" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblPrecioCompra" runat="server" Text="Precio de Compra:"></asp:Label>
        <asp:TextBox ID="txtPrecioCompra" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblPrecioVenta" runat="server" Text="Precio de Venta:"></asp:Label>
        <asp:TextBox ID="txtPrecioVenta" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblFechaVencimiento" runat="server" Text="Fecha de Vencimiento:"></asp:Label>
        <asp:TextBox ID="txtFechaVencimiento" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        <asp:Label ID="lblImagen" runat="server" Text="Imagen:"></asp:Label>
        <asp:FileUpload ID="fuImagen" runat="server" />
        <br />
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
         <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Producto" OnClick="btnEliminar_Click" />
<asp:Button ID="btnBuscar" runat="server" Text="Buscar Producto" OnClick="btnBuscar_Click" />
        <!-- Mensaje de estado -->
        <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
    </div>
    <div>
        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False">
            <Columns>
                 <asp:BoundField DataField="IdProducto" HeaderText="Id Producto" />
                 <asp:BoundField DataField="IdCategoria" HeaderText="Id Categoria" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra" />
                <asp:BoundField DataField="PrecioVenta" HeaderText="Precio Venta" />
                <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:yyyy-MM-dd}" />
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
</asp:Content>
