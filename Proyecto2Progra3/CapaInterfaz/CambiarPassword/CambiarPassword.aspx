<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarPassword.aspx.cs" Inherits="Proyecto2Progra3.CapaInterfaz.CambiarPassword.CambiarPassword" %>

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
    <title>Reset Password</title>
</head>
 <body>
   <div class="content">
     <div class="container">
       <div class="row align-items-center" style="margin-right: 50px; margin-left: -300px; transform: scale(1.1);">
         <div class="col-lg-5">
         </div>
         <div class="col-lg-5 contents">
           <div class="form-block">
           <div class="mb-4 text-center">
                 <h3>Cambiar contraseña</h3>
                 <p class="mb-4">Introduzca una nueva contraseña.</p>
               </div>
               <form action="#" method="post" id="form1" runat="server">
                 <div class="form-group">
                   <label for="password">Nueva Contraseña</label>
                   <asp:TextBox type="password" class="form-control" ID="txtNuevaContrasena" runat="server" required></asp:TextBox>
                 </div>
                 <div class="form-group last mb-4">
                   <label for="re-password">Confirmar Contraseña</label>
                   <asp:TextBox  type="password" class="form-control" ID="txtConfirmarContrasena" runat="server" required></asp:TextBox>
                 </div>
                 <div class="d-flex mb-5 align-items-center">
                   <span class="ml-auto"><a href="../Login/Login.aspx" class="forgot-pass">Iniciar sesión</a></span> 
                 </div>
                <asp:Button ID="btnCambiar" runat="server" class="btn btn-pill text-white btn-block btn-primary" Text="Cambiar" OnClick="btnCambiarContrasena_Click" />
                   <asp:Label ID="lblMensaje" runat="server" />
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
