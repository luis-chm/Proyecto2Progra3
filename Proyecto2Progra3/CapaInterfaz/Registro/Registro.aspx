<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Proyecto2Progra3.CapaInterfaz.Registro.Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/>
      <link href="https://fonts.googleapis.com/css?family=Roboto:300,400&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="fonts/icomoon/style.css">

    <link rel="stylesheet" href="css/owl.carousel.min.css">

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="css/bootstrap.min.css">
    
    <!-- Style -->
    <link rel="stylesheet" href="css/style.css">
    <title>Register</title>
</head>
 <body>
   <div class="content">
     <div class="container">
       <div class="row align-items-center" style="margin-right: 50px; margin-left: -300px; transform: scale(1.1);">
         <div class="col-lg-5">
         </div>
         <div class="col-lg-5 contents">
           <div class="form-block" style="height: 405px;">

           <div class="mb-4 text-center">
                 <h3>Obtener Token</h3>
                 <p class="mb-4">Por favor ingrese la informacion solicitada para generar su Password Temporal.</p>
               </div>
               <form action="#" method="post" id="form1" runat="server">
                 <div class="form-group first">
                   <label for="id">ID del Empleado</label>
                   <asp:TextBox type="text" ID="txtIdEmpleado" class="form-control" runat="server"></asp:TextBox>
                 </div>
                 <div class="form-group">
                   <label for="email">Email</label>
                   <asp:TextBox type="email" class="form-control" ID="txtCorreo" runat="server"></asp:TextBox>
                 </div>
                 <div class="d-flex mb-5 align-items-center">
                   <label class="control control--checkbox mb-3 mb-sm-0"><span class="caption"><a href="#">Términos y Condiciones</a></span>
                   <asp:TextBox type="checkbox" checked="checked" ID="tcheckbox" runat="server"></asp:TextBox>
                   <div class="control__indicator"></div>
                 </label>
                     <br />
                   <span class="ml-auto"><a href="../Login/Login.aspx" class="forgot-pass">Iniciar sesión</a></span> 
                 </div>
               <asp:Button ID="btnRegistrar" runat="server" class="btn btn-pill text-white btn-primary" Text="Registrarse" OnClick="btnRegister_Click" style="float: left;" />
<asp:Button ID="btnSolicitarToken" runat="server" class="btn btn-sm btn-pill text-white btn-primary" Text="Solicitar Nuevo Token" OnClick="btnSolicitarToken_Click" style="float: right;" />
               </form>
             </div>
         </div>
       </div>
     </div>
   </div>
   <script src="js/jquery-3.3.1.min.js"></script>
   <script src="js/popper.min.js"></script>
   <script src="js/bootstrap.min.js"></script>
   <script src="js/main.js"></script>
 </body>
</html>