<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Categoria.aspx.cs" Inherits="Proyecto2Progra3.Interfaz.Categoria" MasterPageFile="~/Menu.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Categoría</h1>
        <div>
            <label for="txtDescripcion">Descripción:</label>
            <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
        </div>
        <div>
            <label for="txtIdCategoria">ID de Categoría:</label>
            <asp:TextBox ID="txtIdCategoria" runat="server"></asp:TextBox>
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
        </div>
        <div>
            <asp:GridView ID="gvCategorias" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="IdCategoria" HeaderText="ID Categoría" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
