<%@ Page Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="CambiarContrasena.aspx.cs" Inherits="Proyecto2Progra3.Interfaz.CambiarContrasena" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h2>Cambiar Contraseña</h2>

        Nueva Contraseña: <asp:TextBox ID="txtNuevaContrasena" runat="server" TextMode="Password"></asp:TextBox><br />
        Confirmar Contraseña: <asp:TextBox ID="txtConfirmarContrasena" runat="server" TextMode="Password"></asp:TextBox><br />
        <asp:Button ID="btnCambiarContrasena" runat="server" Text="Cambiar Contraseña" OnClick="btnCambiarContrasena_Click" />
        <asp:Label ID="lblMensaje" runat="server" />
    </div>
</asp:Content>
