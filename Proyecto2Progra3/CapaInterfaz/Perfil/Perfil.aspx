<%@ Page Title="" Language="C#" MasterPageFile="~/CapaInterfaz/Menu Master/Menu.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Proyecto2Progra3.CapaInterfaz.Perfil.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="max-width: 650px; min-width: 400px;">
        <div class="card">
            <h3 class="card-header text-center">Información Personal</h3>
            <div class="card-body">
                <form id="myForm">
                    <div class="form-group">
                        <label for="codigo">ID Empleado:</label>
                        <asp:Label type="text" class="form-control" ID="lCodigo" runat="server" Text="Label"></asp:Label>
                        <label for="codigo">Cedula:</label>
                        <asp:Label type="text" class="form-control" ID="LCedula" runat="server" Text="Label"></asp:Label>
                        <label for="user">Nombre:</label>
                        <br>
                        <asp:Label class="form-control" for="user" ID="lNombre" runat="server" Text=""></asp:Label>                     
                        <label for="user">Apellidos:</label>
                        <br>
                        <asp:Label class="form-control" for="user" ID="lbApellidos" runat="server" Text=""></asp:Label>                
                        <label for="user">Cargo:</label>
                        <br>
                        <asp:Label class="form-control" for="user" ID="LCargo" runat="server" Text=""></asp:Label>
                        <label for="email">Correo Electronico:</label>
                        <asp:Label type="email" class="form-control" ID="lCorreo" runat="server" Text="Label"></asp:Label>
                        <label for="pass">Direccióm:</label>
                        <asp:Label type="text" class="form-control" ID="lDireccion" runat="server" Text="Label"></asp:Label>
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
