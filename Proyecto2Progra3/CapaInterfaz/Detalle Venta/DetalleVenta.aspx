<%@ Page Title="" Language="C#" MasterPageFile="~/CapaInterfaz/Menu Master/Menu.Master" AutoEventWireup="true" CodeBehind="DetalleVenta.aspx.cs" Inherits="Proyecto2Progra3.CapaInterfaz.Detalle_Venta.DetalleVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="max-width: 1400px; min-width: 500px">
        <div class="card">
            <h3 class="card-header text-center">Detalle de Venta</h3>
            <div class="card-body">

                <html>


                <link rel="stylesheet" href="/css/boton.css" />
                <title>Facturacion</title>
                

                <asp:Label ID="mensajeError1" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="mensajeError" runat="server" ForeColor="Blue"></asp:Label>

                <form id="form1" class="float-end">
                    <div>
                        <table style="width: 100%;" border="2">


                            <tr>

                                <td class="auto-style42">Fecha </td>
                                <td class="auto-style39">Serie</td>
                                <td class="auto-style35">Numero factura</td>
                                <td class="auto-style32"><strong>Tipo</strong></td>
                            </tr>
                            <tr>

                                <td class="auto-style43">
                                    <asp:TextBox ID="Fecha" runat="server" Width="90px" Enabled="False" OnTextChanged="tcedula_TextChanged"></asp:TextBox></td>
                                <td class="auto-style40">
                                    <asp:TextBox ID="Tserie" runat="server" OnTextChanged="tnumerofactura_TextChanged" Enabled="False" Width="100px"></asp:TextBox></td>
                                <td class="auto-style37">
                                    <asp:TextBox ID="tnumerofactura" runat="server" OnTextChanged="tnumerofactura_TextChanged" Enabled="False"></asp:TextBox></td>
                                <td class="auto-style20">
                                    <asp:TextBox ID="Ttipo" runat="server" Width="28px" Enabled="False"></asp:TextBox>
                                    <asp:DropDownList ID="DropDownTipo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownTipo_SelectedIndexChanged">
                                        <asp:ListItem Text="Factura" Value="F"></asp:ListItem>
                                        <asp:ListItem Text="Nota de Credito" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                            </tr>




                            <tr>
                                <td class="auto-style30"><strong>Codigo Empleado</strong></td>
                                <td class="auto-style41">Nombre Empleado</td>
                                <td class="auto-style36"><strong>Codigo cliente</strong></td>
                                <td class="auto-style33">Nombre Cliente</td>
                                <td class="auto-style31">Cedula</td>

                            </tr>
                            <tr>
                                <td class="auto-style43">
                                    <asp:TextBox ID="Tcodigoempleado" runat="server" OnTextChanged="tcodigoempleado_TextChanged" TextMode="Number" Width="90px"></asp:TextBox>&nbsp;&nbsp;
                                </td>
                                <td class="auto-style40">
                                    <asp:TextBox ID="TnombreEmpleado" runat="server" Width="184px" Enabled="False" OnTextChanged="tnombreEmpleado_TextChanged"></asp:TextBox></td>
                                <td class="auto-style38">
                                    <asp:TextBox ID="tcodigocliente" runat="server" OnTextChanged="tcodigocliente_TextChanged" TextMode="Number" Width="90px"></asp:TextBox>&nbsp;&nbsp;
                                </td>
                                <td class="auto-style34">
                                    <asp:TextBox ID="tnombrecliente" runat="server" Width="184px" Enabled="False" OnTextChanged="tnombrecliente_TextChanged"></asp:TextBox></td>
                                <td class="auto-style9">
                                    <asp:TextBox ID="tcedula" runat="server" Width="170px" Enabled="False" OnTextChanged="tcedula_TextChanged"></asp:TextBox></td>

                            </tr>


                            <tr>
                                <td class="auto-style3"><strong>Codigo</strong></td>
                                <td class="auto-style39">Producto</td>
                                <td class="auto-style35">Marca</td>
                                <td class="auto-style29">Stock&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong <strong class="blue-text">Cantidad </strong></td>
                                <td class="auto-style2">Precio</td>
                            </tr>
                            <tr>
                                <td class="auto-style43">
                                    <asp:TextBox ID="tcodigo" runat="server" OnTextChanged="tcodigo_TextChanged" TextMode="Number" Width="90px"></asp:TextBox>&nbsp;&nbsp;
                                </td>
                                <td class="auto-style40">
                                    <asp:TextBox ID="tnombre" runat="server" Width="184px" Enabled="False" OnTextChanged="tnombre_TextChanged"></asp:TextBox></td>
                                <td class="auto-style38">
                                    <asp:TextBox ID="tmarca" runat="server" Width="184px" Enabled="False" OnTextChanged="tnombre_TextChanged"></asp:TextBox></td>
                                <td class="auto-style34">
                                    <asp:TextBox ID="tStock" runat="server" TextMode="Number" Enabled="False" OnTextChanged="tcantidad_TextChanged" Width="90px"></asp:TextBox>
                                
                                    <asp:TextBox ID="tcantidad" runat="server" TextMode="Number" OnTextChanged="tcantidad_TextChanged" Width="90px"></asp:TextBox></td>
                                <td class="auto-style9">
                                    <asp:TextBox ID="tprecio" runat="server" Enabled="False" OnTextChanged="tprecio_TextChanged" Width="170px"></asp:TextBox></td>
                            </tr>



                        </table>
                    </div>

                    <asp:Button ID="btnIngresar" class="btn btn-success" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" />
                    <asp:Button ID="Limpiar" class="btn btn-dark" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                    <div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
                            AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            PageSize="7" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                <asp:BoundField DataField="Nombre" HeaderText="Articulo" />
                                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" />
                                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div>
                        <table style="width: 100%;" border="1">
                            <tr>
                                <td class="auto-style1"><strong>SUBTOTAL</strong></td>
                                <td class="auto-style1"><strong>IVA</strong></td>
                                <td class="auto-style1"><strong>TOTAL</strong></td>
                            </tr>
                            <tr>
                                <td class="auto-style1">
                                    <asp:Label ID="LSB" runat="server" Font-Size="Large" Text="0"></asp:Label>
                                </td>
                                <td class="auto-style1">
                                    <asp:Label ID="LIVA" runat="server" Font-Size="Large" Text="0"></asp:Label>
                                </td>
                                <td class="auto-style1">
                                    <asp:Label ID="LTOTAL" runat="server" Font-Size="Large" Text="0"></asp:Label>
                                </td>

                            </tr>

                        </table>
                    </div>

                    <asp:Button ID="btnFacturar" class="btn btn-success" runat="server" Text="Facturar" OnClick="btnFacturar_Click" />
                </form>
            </div>
            <style type="text/css">
                .auto-style1 {
                    text-align: center;
                }

                .auto-style2 {
                    text-align: center;
                    height: 15px;
                    vertical-align: bottom;
                }

                .auto-style3 {
                    text-align: center;
                    height: 15px;
                    width: 318px;
                    color: darkblue;
                    vertical-align: bottom;
                }

                .blue-text {
                    color: darkblue;
                }

                .mGrid {
                    width: 100%;
                    background-color: #fff;
                    margin: 5px 0 10px 0;
                    border: solid 1px #525252;
                    border-collapse: collapse;
                }

                .auto-style9 {
                    text-align: center;
                    height: 10px;
                    width: 295px;
                }

                .auto-style20 {
                    text-align: center;
                    height: 3px;
                    width: 471px;
                }

                .auto-style29 {
                    text-align: center;
                    height: 45px;
                    vertical-align: bottom;
                    width: 471px;
                }

                .auto-style30 {
                    text-align: center;
                    height: 46px;
                    width: 318px;
                    color: darkblue;
                    vertical-align: bottom;
                }

                .auto-style31 {
                    text-align: center;
                    height: 46px;
                    vertical-align: bottom;
                }

                .auto-style32 {
                    text-align: center;
                    height: 15px;
                    width: 471px;
                    color: darkblue;
                    vertical-align: bottom;
                }

                .auto-style33 {
                    text-align: center;
                    height: 46px;
                    vertical-align: bottom;
                    width: 471px;
                }

                .auto-style34 {
                    text-align: center;
                    height: 10px;
                    width: 471px;
                }

                .auto-style35 {
                    text-align: center;
                    height: 15px;
                    vertical-align: bottom;
                    width: 649px;
                }

                .auto-style36 {
                    text-align: center;
                    height: 46px;
                    width: 649px;
                    color: darkblue;
                    vertical-align: bottom;
                }

                .auto-style37 {
                    text-align: center;
                    height: 3px;
                    width: 649px;
                }

                .auto-style38 {
                    text-align: center;
                    height: 10px;
                    width: 649px;
                }

                .auto-style39 {
                    text-align: center;
                    height: 15px;
                    vertical-align: bottom;
                    width: 606px;
                }

                .auto-style40 {
                    text-align: center;
                    width: 606px;
                    height: 10px;
                }

                .auto-style41 {
                    text-align: center;
                    height: 46px;
                    vertical-align: bottom;
                    width: 606px;
                }

                .auto-style42 {
                    text-align: center;
                    height: 15px;
                    vertical-align: bottom;
                    width: 318px;
                }

                .auto-style43 {
                    text-align: center;
                    height: 10px;
                    width: 318px;
                }
            </style>
        </div>
</asp:Content>
