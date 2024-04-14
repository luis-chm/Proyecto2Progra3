<%@ Page Language="C#" MasterPageFile="~/CapaInterfaz/Menu Master/Menu.Master" AutoEventWireup="true" CodeBehind="ReporteVentas.aspx.cs" Inherits="Proyecto2Progra3.CapaInterfaz.ReportesVentas.ReporteVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="max-width: 1400px; min-width: 500px">
        <div class="card">
            <h3 class="card-header text-center">Reporte Venta</h3>
            <div class="card-body">

                <html>


                <link rel="stylesheet" href="/css/boton.css" />
                <title>ReporteVentas</title>
                <style type="text/css">
                    .auto-style1 {
                        text-align: center;
                    }

                    .auto-style2 {
                        text-align: center;
                        height: 15px;
                    }

                    .auto-style3 {
                        text-align: center;
                        height: 15px;
                        width: 144px;
                    }

                    .auto-style4 {
                        text-align: center;
                        width: 144px;
                        height: 27px;
                    }

                    .auto-style5 {
                        text-align: center;
                        height: 27px;
                    }

                    .mGrid {
                        width: 100%;
                        background-color: #fff;
                        margin: 5px 0 10px 0;
                        border: solid 1px #525252;
                        border-collapse: collapse;
                    }

                    .auto-style6 {
                        text-align: center;
                        height: 15px;
                        width: 266px;
                    }

                    .auto-style7 {
                        text-align: center;
                        height: 27px;
                        width: 266px;
                    }
                </style>

                <asp:Label ID="mensajeError" runat="server" ForeColor="Red"></asp:Label>

                <form id="form1" class="float-end">
                    <div>
                        <table style="width: 100%;" border="2">
                            <tr>
                                <td class="auto-style6"><strong>Modo de Busqueda</strong></td>
                            </tr>

                            <td class="auto-style6">

                                <asp:DropDownList ID="DropDownTipo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownTipo_SelectedIndexChanged" CssClass="offset-xxl-0">
                                    <asp:ListItem Text="Cliente" Value="Cliente"></asp:ListItem>
                                    <asp:ListItem Text="Equipo" Value="Equipo"></asp:ListItem>
                                    <asp:ListItem Text="Fecha" Value="Fecha"></asp:ListItem>
                                </asp:DropDownList>

                            <tr>
                                <td class="auto-style6">
                                    <asp:TextBox ID="tbusqueda" runat="server"></asp:TextBox>

                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style4">
                                    <asp:Button ID="btnBuscar" class="btn btn-success" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>


                    <div>
                        <asp:GridView ID="GridViewDetallesVenta" runat="server" AutoGenerateColumns="False" GridLines="None"
                            AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            PageSize="100" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="IdDetalleVenta" HeaderText="IdDetalleVenta" />
                                <asp:BoundField DataField="Linea" HeaderText="Linea" />
                                <asp:BoundField DataField="IdVenta" HeaderText="IdVenta" />
                                <asp:BoundField DataField="IdProducto" HeaderText="IdProducto" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="PrecioUnitario" HeaderText="PrecioUnitario" />
                                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" />
                            </Columns>
                        </asp:GridView>

                    </div>


                </form>
            </div>
        </div>
</asp:Content>
