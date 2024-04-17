<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="Dsportal.Login.ForgotPassword.ResetPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <!-- Favicons -->
    <link href="../../img/dsplogo.png" rel="icon">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            border-radius: 10px;
        }

        .card {
            width: 300px;
            border: none;
            border-radius: 10px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
        }

        .card-header {
            font-size: 1.5rem;
        }

        .card-body {
            padding: 20px;
        }

        .form-label {
            font-size: 0.875rem;
            color: #888;
        }

        .btn-primary {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="card">
                    <div class="card-header bg-primary text-white text-center">Change Password</div>
                    <div class="card-body">
                        <div class="form-group">
                            <label class="form-label" for="newPassword">New Password</label>

                            <asp:TextBox ID="new_PWTextBox" class="form-control" placeholder="Enter new password" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label" for="confirmPassword">Confirm Password</label>
                            <asp:TextBox ID="Confirm_PWTextBox1" class="form-control" placeholder="Confirm new password" runat="server"></asp:TextBox>
                        </div>
                        <asp:Button ID="Chng_Button1" class="btn btn-primary btn-block" runat="server" Text="Change Password" OnClick="Chng_Button1_Click" />
                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>

                        <asp:PlaceHolder ID="PlaceHolder2" runat="server">
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </asp:PlaceHolder>
                        <br />
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Back to Login</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
