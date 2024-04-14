<%@ Page Language="C#" MasterPageFile="~/CapaInterfaz/Menu Master/Menu.Master" AutoEventWireup="true" CodeBehind="ReportesCatalogos.aspx.cs" Inherits="Proyecto2Progra3.CapaInterfaz.ReportesCatalogos.ReportesCatalogos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="max-width: 1100px; min-width: 500px">
        <div class="card" style="height: 550px">
            <h3 class="card-header text-center">Reporte Catalogos</h3>
            <div class="card-body">

                <html>


                <link rel="stylesheet" href="/css/boton.css" />
                <style type="text/css">
                    .espacio-entre-botones {
                        margin-bottom: 20px;
                    }

                    .auto-style4 {
                    }

                    .mGrid {
                        width: 100%;
                        background-color: #fff;
                        margin: 5px 0 10px 0;
                        border: solid 1px #525252;
                        border-collapse: collapse;
                    }

                    .auto-style7 {
                        height: 239px;
                        text-align: center;
                    }
                </style>

                <asp:Label ID="mensajeError" runat="server" ForeColor="Red"></asp:Label>

                <form id="form1" class="float-end">
                    <div class="auto-style7">
                        <table style="width: 100%;" border="2">
                            <br />
                            <asp:Button ID="btnCatEmpleados" class="btn btn-success auto-style4 espacio-entre-botones" runat="server" Text="Descargar Catalogo De Empleados" OnClick="btnBuscarCatEmpleados_Click" Width="370px" />
                            <br />
                            <asp:Button ID="btnCatClientes" class="btn btn-success auto-style4 espacio-entre-botones" runat="server" Text="Descargar Catalogo De Clientes" OnClick="btnBuscarCatClientes_Click" Width="370px" />
                            <br />
                            <asp:Button ID="btnCatProductos" class="btn btn-success auto-style4 espacio-entre-botones" runat="server" Text="Descargar Catalogo De Productos" OnClick="btnBuscarCatProductos_Click" Width="370px" />
                            <br />
                            <asp:Button ID="btnBuscarCatUsuario" class="btn btn-success auto-style4 espacio-entre-botones" runat="server" Text="Descargar Catalogo De Usuarios" OnClick="btnBuscarCatUsuario_Click" Width="370px" />
                            <br />
                            <asp:Button ID="btnCatCategoria" class="btn btn-success auto-style4 espacio-entre-botones" runat="server" Text="Descargar Catalogo De Categorias" OnClick="btnCatCategoria_Click" Width="370px" />
                            <br />
                            <asp:Button ID="btnCatCargo" class="btn btn-success auto-style4 espacio-entre-botones" runat="server" Text="Descargar Catalogo De Cargos" OnClick="btnCatCargo_Click" Width="370px" />
                            <br />
                            <asp:Button ID="btnCatTodos" class="btn btn-success auto-style4 espacio-entre-botones" runat="server" Text="Descargar Todos Los Catalogos" Width="370px" OnClick="btnCatTodos_Click" />
                        </table>
                    </div>

                </form>
            </div>
        </div>
</asp:Content>
