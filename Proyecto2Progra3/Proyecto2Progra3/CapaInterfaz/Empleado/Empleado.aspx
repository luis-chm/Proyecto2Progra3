<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Empleado.aspx.cs" Inherits="Proyecto2Progra3.Interfaz.Empleado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h2>Registrar Empleado</h2>
         ID: <asp:TextBox ID="txtId" runat="server"></asp:TextBox><br />
        DNI: <asp:TextBox ID="txtDNI" runat="server"></asp:TextBox><br />
        Apellidos: <asp:TextBox ID="txtApellidos" runat="server"></asp:TextBox><br />
        Nombres: <asp:TextBox ID="txtNombres" runat="server"></asp:TextBox><br />
        Sexo: <asp:DropDownList ID="ddlSexo" runat="server">
            <asp:ListItem Text="M" Value="M"></asp:ListItem>
            <asp:ListItem Text="F" Value="F"></asp:ListItem>
        </asp:DropDownList><br />
        Fecha Nacimiento: <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date"></asp:TextBox><br />
        Dirección: <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox><br />
        Estado Civil: <asp:TextBox ID="txtEstadoCivil" runat="server"></asp:TextBox><br />
        Imagen: <asp:FileUpload ID="fuImagen" runat="server" /><br />
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnBuscar_Click" /><br />
        <asp:GridView ID="gvEmpleados" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="IdEmpleado" HeaderText="ID Empleado" />
                <asp:BoundField DataField="DNI" HeaderText="DNI" />
                <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha de Nacimiento" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="EstadoCivil" HeaderText="Estado Civil" />
                <asp:TemplateField HeaderText="Imagen">
                    <ItemTemplate>
                        <asp:Image ID="imgEmpleado" runat="server" Height="100" Width="100" ImageUrl='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("Imagen")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
             
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
