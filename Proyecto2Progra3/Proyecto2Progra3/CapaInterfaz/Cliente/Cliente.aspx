<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="Proyecto2Progra3.CapaInterfaz.Cliente.Cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h2>Clientes</h2>
        <div>
            <asp:Label ID="lblIdCliente" runat="server" Text="ID Cliente:"></asp:Label>
            <asp:TextBox ID="txtIdCliente" runat="server"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
        </div>
        <br />
        <div>
            <asp:Label ID="lblDNI" runat="server" Text="DNI:"></asp:Label>
            <asp:TextBox ID="txtDNI" runat="server"></asp:TextBox>
            <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label>
            <asp:TextBox ID="txtApellidos" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label>
            <asp:TextBox ID="txtNombres" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblDireccion" runat="server" Text="Dirección:"></asp:Label>
            <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:"></asp:Label>
            <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
            <!-- Otros campos de entrada para datos del cliente -->
            <br />
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
        </div>
        <br />
        <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="IdCliente" HeaderText="ID Cliente" />
                <asp:BoundField DataField="DNI" HeaderText="DNI" />
                <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
