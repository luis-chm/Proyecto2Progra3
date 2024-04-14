<%@ Page Title="" Language="C#" MasterPageFile="~/CapaInterfaz/Menu Master/Menu.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Proyecto2Progra3.CapaInterfaz.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
       
        .full-screen-image {
            width: 120vw; 
            height: 100vh;
            object-fit: cover;
            object-position: center;
        }

        .title {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="title">Bienvenido al Sistema de Facturación</h1>
    <img class="full-screen-image" src="https://e-recibos.com/wp-content/uploads/2023/02/factura-electronica.jpg" alt="Imagen de fondo" />


</asp:Content>
