<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Dsportal.Login.Register" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Register</title>
    <!-- Favicons -->
    <link href="../../img/dsplogo.png" rel="icon">

    <!-- Custom fonts for this template-->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">

    <!-- Custom styles for this template-->
    <link href="css/sb-admin-2.min.css" rel="stylesheet">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <script>
        function validateMobileNumber() {
            // Mobile Number Validation
            var mobTextbox = document.getElementById('mobTextbox');
            var mobValue = mobTextbox.value.trim();
            var mobRegex = /^[0-9]{10}$/; // Assuming 10-digit mobile numbers

            if (!mobRegex.test(mobValue)) {
                alert('Please enter a valid 10-digit mobile number.');
                mobTextbox.focus();
                return false;
            }

            // Email Validation
            var emailTextbox = document.getElementById('emailTextbox');
            var emailValue = emailTextbox.value.trim();
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            if (!emailRegex.test(emailValue)) {
                alert('Please enter a valid email address.');
                emailTextbox.focus();
                return false;
            }

            // If both validations pass, the form is considered valid
            return true;
        }
    </script>

</head>

<body class="bg-gradient-primary">

    <div class="container">

        <div class="card o-hidden border-0 shadow-lg my-5">
            <div class="card-body p-0">
                <!-- Nested Row within Card Body -->
                <div class="row">
                    <div class="col-lg-5 d-none  d-flex align-items-center">
                        <img src="../../img/dsplogo.png" height="48%" style="min-height: 260px; align-content: center" />
                    </div>
                    <div class="col-lg-7">
                        <div class="p-5">
                            <div class="text-center">
                                <h1 class="h4 text-gray-900 mb-4">Create an Account!</h1>
                            </div>
                            <form class="user" runat="server" onsubmit="return validateMobileNumber();">
                                <div class="form-group row">
                                    <div class="col-sm-6 mb-3 mb-sm-0">

                                        <asp:TextBox ID="nameTextbox" runat="server" class="form-control form-control-user" placeholder="Full Name"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6">

                                        <asp:TextBox ID="mobTextbox" runat="server" class="form-control form-control-user" placeholder="Mobile No..."></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">

                                    <asp:TextBox ID="emailTextbox" runat="server" class="form-control form-control-user" placeholder="Email Address"></asp:TextBox>
                                </div>

                                <div class="form-group row">
                                    <div class="col-sm-6 mb-3 mb-sm-0">
                                        <asp:TextBox ID="addressTextbox" runat="server" class="form-control form-control-user" placeholder="Full Address with Pincode"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="passwordTextbox" runat="server" class="form-control form-control-user" placeholder="Password"></asp:TextBox>
                                    </div>
                                </div>
                                <div>
                                    <asp:Button ID="registerbtn" class="btn btn-primary btn-user btn-block" OnClick="registerbtn_Click" runat="server" Text="Register Now" />
                                </div>
                                <hr>
                                <a href="#" class="btn btn-google btn-user btn-block" onclick="googlebtn_Click()">
                                    <i class="fab fa-google fa-fw"></i>Register with Google
                                </a>

                                <a href="index.html" class="btn btn-facebook btn-user btn-block">
                                    <i class="fab fa-facebook-f fa-fw"></i>Register with Facebook
                                </a>
                            </form>
                            <hr>
                            <div class="text-center">
                                <a class="small" href="ForgotPassword/ForgotPassword.aspx">Forgot Password?</a>
                            </div>

                            <div class="text-center">
                                <a class="small" href="Login.aspx">Already have an account? Login!</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <!-- Bootstrap core JavaScript-->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom scripts for all pages-->
    <script src="js/sb-admin-2.min.js"></script>

</body>

</html>
