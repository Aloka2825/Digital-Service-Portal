<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Dsportal.Login.ForgotPassword.ForgotPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <!-- Favicons -->
    <link href="../../img/dsplogo.png" rel="icon">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
   <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
        }

        .auto-style1 {
            border-radius: 10px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }

        .card-header {
            font-size: 1.5rem;
            background-color: #007BFF;
            color: #fff;
            text-align: center;
            padding: 15px;
        }

        .card-body {
            padding: 20px;
        }

        .form-label {
            font-size: 0.875rem;
            color: #888;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .btn-primary {
            font-weight: bold;
        }

        .d-flex {
            margin-top: 20px;
            justify-content: space-between;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="auto-style1">
                    <div class="card-header">Password Reset</div>
                    <div class="card-body">
                        <p class="card-text text-center">
                            Enter your email address and we'll send you an email with instructions to reset your password.
                        </p>
                        <div class="form-group">
                            <label class="form-label" for="typeEmail">Email </label>
                            <asp:TextBox ID="EmailTextBox" placeholder="Enter your email" class="form-control" runat="server"></asp:TextBox>
                            <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:Button ID="Forgot_btn" class="btn btn-primary btn-block" runat="server" Text="Send OTP" OnClick="Forgot_btn_Click1" />
                      <!-- OTP Verification Elements (Initially Hidden) -->
                        <div runat="server" id="otpVerification" visible="false">
                            <div class="form-group">
                                <label class="form-label" for="otp">Enter OTP</label>
                                <asp:TextBox ID="OTPTextBox" placeholder="Enter OTP" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <asp:Button ID="VerifyOTPButton" class="btn btn-primary btn-block" runat="server" Text="Verify OTP" OnClick="VerifyOTPButton_Click1" />
                        </div>
                        
                        <div class="d-flex justify-content-between mt-4">
                            <!-- "Back to Login" button -->
                            <a href="../Login.aspx" class="btn btn-link">Back to Login</a>
                            
                            <!-- "Register" button -->
                            <a href="../Register.aspx" class="btn btn-link">Register</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>