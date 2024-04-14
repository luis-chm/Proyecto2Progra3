<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Proyecto2Progra3.Interfaz.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Registro de Usuario</h2>
    IdEmpleado: <asp:TextBox ID="txtIdEmpleado" runat="server"></asp:TextBox><br />
    Usuario: <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox><br />
    
    <asp:Button ID="btnRegister" runat="server" Text="Registrar" OnClick="btnRegister_Click" />
    
</asp:Content>
