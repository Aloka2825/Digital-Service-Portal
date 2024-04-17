<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OtherServices.aspx.cs" Inherits="Dsportal.User.OtherServices" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Other Online Services</title>
    <link href="../../img/dsplogo.png" rel="icon">
    <!-- Custom fonts for this template-->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <!-- Vendor CSS Files -->
    <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet">
    <link href="assets/vendor/aos/aos.css" rel="stylesheet">
    <link href="assets/vendor/glightbox/css/glightbox.min.css" rel="stylesheet">
    <link href="assets/vendor/swiper/swiper-bundle.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css">

    <!-- Custom styles for this template-->
    <link href="css/sb-admin-2.min.css" rel="stylesheet">
    <style>
        .gridview-container {
    max-height:600px; /* Set your desired max height */
    overflow-y: auto; /* Enable vertical scrolling */
}
    </style>
    
</head>

<body id="page-top">
    <form runat="server">
        <!-- Page Wrapper -->
        <div id="wrapper">

            <!-- Sidebar -->
            <ul class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" id="accordionSidebar">

                <!-- Sidebar - Brand -->
                <br />
                <a class="sidebar-brand d-flex align-items-center justify-content-center" href="Dashboard.aspx">
                    <!--<div class="sidebar-brand-icon rotate-n-15">
                   <i class="fas fa-laugh-wink"></i>
               </div>
               <div class="sidebar-brand-text mx-3">Digital Service Portal.</div>-->

                    <img src="../img/dsplogo.png" style="max-width: 200px" />
                </a>
                <hr />
                <!-- Divider -->
                <hr class="sidebar-divider my-0">

                <!-- Nav Item - Dashboard -->
                <li class="nav-item active">
                    <a class="nav-link" href="Dashboard.aspx">
                        <i class="fas fa-fw fa-tachometer-alt"></i>
                        <span>Dashboard</span></a>
                </li>

                <!-- Divider -->
                <hr class="sidebar-divider">

                <!-- Heading -->
                <div class="sidebar-heading">
                    Services
                </div>

                <!-- Nav Item - Pages Collapse Menu -->
                <li class="nav-item">
                    <a class="nav-link collapsed" href="#" data-toggle="collapse" data-target="#collapseTwo"
                        aria-expanded="true" aria-controls="collapseTwo">
                        <i class="fas fa-fw fa-cog"></i>
                        <span>Services</span>
                    </a>
                    <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionSidebar">
                        <div class="bg-white py-2 collapse-inner rounded">
                            <h6 class="collapse-header">All Services:</h6>
                            <a class="collapse-item" href="AhtsServices.aspx">Ahts Services</a>
                            <a class="collapse-item" href="CscServices.aspx">Digital Services</a>
                            <a class="collapse-item" href="OtherServices.aspx">Other Online Services</a>
                        </div>
                    </div>
                </li>

                <!-- Nav Item - Utilities Collapse Menu -->
                <li class="nav-item">
                    <a class="nav-link collapsed" href="#" data-toggle="collapse" data-target="#collapseUtilities"
                        aria-expanded="true" aria-controls="collapseUtilities">
                        <i class="fas fa-fw fa-wrench"></i>
                        <span>Applied Services</span>
                    </a>
                    <div id="collapseUtilities" class="collapse" aria-labelledby="headingUtilities"
                        data-parent="#accordionSidebar">
                        <div class="bg-white py-2 collapse-inner rounded">
                            <h6 class="collapse-header">All Applied Services:</h6>
                            <a class="collapse-item" href="ViewAhtsServices.aspx">Ahts Services</a>

                        </div>
                    </div>
                </li>

                <!-- Divider -->
                <hr class="sidebar-divider">

                <!-- Heading -->
                <div class="sidebar-heading">
                    Upcoming Services
               
                </div>

                <!-- Nav Item - Pages Collapse Menu -->
                <li class="nav-item">
                    <a class="nav-link collapsed" href="#" data-toggle="collapse" data-target="#collapsePages"
                        aria-expanded="true" aria-controls="collapsePages">
                        <i class="fas fa-fw fa-folder"></i>
                        <span>New Services &nbsp;<img src="img/new1.gif" /></span>
                    </a>
                    <div id="collapsePages" class="collapse" aria-labelledby="headingPages" data-parent="#accordionSidebar">
                        <div class="bg-white py-2 collapse-inner rounded">
                            <h6 class="collapse-header">Education:</h6>
                            <a class="collapse-item" href="https://e-learning.ahtscarrier.com/" target="_blank">Free E-Learning Portal</a>
                            <!--<a class="collapse-item" href="register.html">Register</a>
                       <a class="collapse-item" href="forgot-password.html">Forgot Password</a>-->
                            <div class="collapse-divider"></div>
                            <h6 class="collapse-header">Job:</h6>
                            <a class="collapse-item" href="https://jobportal.ahtscarrier.com/" target="_blank">AHTS Job Portal</a>
                            <!--<a class="collapse-item" href="blank.html">Blank Page</a>-->
                        </div>
                    </div>
                </li>

                <!-- Nav Item - Charts -->
                <!--<li class="nav-item">
               <a class="nav-link" href="charts.html">
                   <i class="fas fa-fw fa-chart-area"></i>
                   <span>Charts</span></a>
           </li>-->

                <!-- Nav Item - Tables -->


                <!-- Divider -->
                <hr class="sidebar-divider d-none d-md-block">

                <div class="sidebar-heading">
                    Wallet
   
                </div>

                <!-- Nav Item - Utilities Collapse Menu -->
                <li class="nav-item">
                    <a class="nav-link collapsed" href="#" data-toggle="collapse" data-target="#collapseWallet"
                        aria-expanded="false" aria-controls="collapseWallet">
                        <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                        <span>Wallet</span>
                    </a>
                    <div id="" class="collapse" aria-labelledby="headingUtilities"
                        data-parent="#accordionSidebar">
                        <div class="bg-white py-2 collapse-inner rounded">
                            <h6 class="">Wallet:</h6>
                            <a class="collapse-item" href="Feedback.aspx">Add Money</a>
                            <a class="collapse-item" href="Feedback.aspx">Manage & View Rewards</a>
                            <a class="collapse-item" href="Feedback.aspx">Transfer to Bank</a>

                        </div>
                    </div>
                </li>



                <div class="sidebar-heading">
                    Report
   
                </div>

                <li class="nav-item">
                    <a class="nav-link" href="ViewAhtsServices.aspx">
                        <i class="fas fa-fw fa-table"></i>
                        <span>Daily Report</span></a>
                </li>

                <div class="sidebar-heading">
                    Feedback
  
                </div>
                <li class="nav-item">
                    <a class="nav-link" href="Feedback.aspx">
                        <i class="fas fa-fw fa-table"></i>
                        <span>Feed Back</span></a>
                </li>

                <!--<div class="text-center d-none d-md-inline">
    <button class="rounded-circle border-0" id="sidebarToggle"></button>
</div>-->

                <!-- Sidebar Message -->
                <!--<div class="sidebar-card d-none d-lg-flex">
               <img class="sidebar-card-illustration mb-2" src="img/undraw_rocket.svg" alt="...">
               <p class="text-center mb-2"><strong>SB Admin Pro</strong> is packed with premium features, components, and more!</p>
               <a class="btn btn-success btn-sm" href="https://startbootstrap.com/theme/sb-admin-pro">Upgrade to Pro!</a>
           </div>-->

            </ul>
            <!-- End of Sidebar -->

            <!-- Content Wrapper -->
            <div id="content-wrapper" class="d-flex flex-column">

                <!-- Main Content -->
                <div id="content">

                    <!-- Topbar -->
                    <nav class="navbar navbar-expand navbar-light bg-white topbar mb-4 static-top shadow">

                        <!-- Sidebar Toggle (Topbar) -->
                        <button id="sidebarToggleTop" class="btn btn-link d-md-none rounded-circle mr-3">
                            <i class="fa fa-bars"></i>
                        </button>

                        <!-- Topbar Search -->
                        <form
                            class="d-none d-sm-inline-block form-inline mr-auto ml-md-3 my-2 my-md-0 mw-100 navbar-search">
                            <div class="input-group">
                                <asp:TextBox class="form-control bg-light border-0 small" placeholder="Search for Ahts Services..." ID="searchTextbox" runat="server"></asp:TextBox>
                                <div class="input-group-append">

                                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click">
        <i class="fas fa-search fa-sm"></i>
        <asp:Literal runat="server" Text=" Search" />
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </form>

                        <!-- Topbar Navbar -->
                        <ul class="navbar-nav ml-auto">

                            <!-- Nav Item - Search Dropdown (Visible Only XS) -->
                            <li class="nav-item dropdown no-arrow d-sm-none">
                                <a class="nav-link dropdown-toggle" href="#" id="searchDropdown" role="button"
                                    data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-search fa-fw"></i>
                                </a>
                                <!-- Dropdown - Messages -->
                                <div class="dropdown-menu dropdown-menu-right p-3 shadow animated--grow-in"
                                    aria-labelledby="searchDropdown">
                                    <form class="form-inline mr-auto w-100 navbar-search">
                                        <div class="input-group">
                                            <input type="text" class="form-control bg-light border-0 small"
                                                placeholder="Search for..." aria-label="Search"
                                                aria-describedby="basic-addon2">
                                            <div class="input-group-append">
                                                <button class="btn btn-primary" type="button">
                                                    <i class="fas fa-search fa-sm"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </li>

                            <!-- Nav Item - Alerts -->
                            <li class="nav-item dropdown no-arrow mx-1">
                                <a class="nav-link dropdown-toggle" href="#" id="alertsDropdown" role="button"
                                    data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-bell fa-fw"></i>
                                    <!-- Counter - Alerts -->
                                    <span class="badge badge-danger badge-counter">3+</span>
                                </a>
                                <!-- Dropdown - Alerts -->
                                <div class="dropdown-list dropdown-menu dropdown-menu-right shadow animated--grow-in"
                                    aria-labelledby="alertsDropdown">
                                    <h6 class="dropdown-header">Alerts Center
                                    </h6>
                                    <a class="dropdown-item d-flex align-items-center" href="#">
                                        <div class="mr-3">
                                            <div class="icon-circle bg-primary">
                                                <i class="fas fa-file-alt text-white"></i>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="small text-gray-500">December 12, 2019</div>
                                            <span class="font-weight-bold">Welcome to the Digital Service Portal!</span>
                                        </div>
                                    </a>
                                    <a class="dropdown-item d-flex align-items-center" href="#">
                                        <div class="mr-3">
                                            <div class="icon-circle bg-success">
                                                <i class="fas fa-donate text-white"></i>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="small text-gray-500"></div>
                                            You are now a family member of AHTS!
                                        </div>
                                    </a>
                                    <a class="dropdown-item d-flex align-items-center" href="#">
                                        <div class="mr-3">
                                            <div class="icon-circle bg-warning">
                                                <i class="fas fa-exclamation-triangle text-white"></i>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="small text-gray-500"></div>
                                            Earn Money without any Investstment by giving the lead.
                                        </div>
                                    </a>
                                    <a class="dropdown-item text-center small text-gray-500" href="#">Show All Alerts</a>
                                </div>
                            </li>

                            <!-- Nav Item - Messages -->
                            <li class="nav-item dropdown no-arrow mx-1">
                                <a class="nav-link dropdown-toggle" href="#" id="messagesDropdown" role="button"
                                    data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-envelope fa-fw"></i>
                                    <!-- Counter - Messages -->
                                    <span class="badge badge-danger badge-counter">7</span>
                                </a>
                                <!-- Dropdown - Messages -->
                                <div class="dropdown-list dropdown-menu dropdown-menu-right shadow animated--grow-in"
                                    aria-labelledby="messagesDropdown">
                                    <h6 class="dropdown-header">Message Center
                                    </h6>
                                    <a class="dropdown-item d-flex align-items-center" href="#">
                                        <div class="dropdown-list-image mr-3">
                                            <img class="rounded-circle" src="img/undraw_profile_1.svg"
                                                alt="...">
                                            <div class="status-indicator bg-success"></div>
                                        </div>
                                        <div class="font-weight-bold">
                                            <div class="text-truncate">
                                                Hi there! I am wondering if you can help me with a
                                           problem I've been having.
                                            </div>
                                            <div class="small text-gray-500">Jyosna Ojha · 58m</div>
                                        </div>
                                    </a>
                                    <a class="dropdown-item d-flex align-items-center" href="#">
                                        <div class="dropdown-list-image mr-3">
                                            <img class="rounded-circle" src="img/undraw_profile_2.svg"
                                                alt="...">
                                            <div class="status-indicator"></div>
                                        </div>
                                        <div>
                                            <div class="text-truncate">
                                                I have the photos that you ordered last month, how
                                           would you like them sent to you?
                                            </div>
                                            <div class="small text-gray-500">Abhisekh Rana · 1d</div>
                                        </div>
                                    </a>
                                    <a class="dropdown-item d-flex align-items-center" href="#">
                                        <div class="dropdown-list-image mr-3">
                                            <img class="rounded-circle" src="img/undraw_profile_3.svg"
                                                alt="...">
                                            <div class="status-indicator bg-warning"></div>
                                        </div>
                                        <div>
                                            <div class="text-truncate">
                                                Last month's report looks great, I am very happy with
                                           the progress so far, keep up the good work!
                                            </div>
                                            <div class="small text-gray-500">Laxmikanta Jena · 2d</div>
                                        </div>
                                    </a>
                                    <a class="dropdown-item d-flex align-items-center" href="#">
                                        <div class="dropdown-list-image mr-3">
                                            <img class="rounded-circle" src="https://source.unsplash.com/Mv9hjnEUHR4/60x60"
                                                alt="...">
                                            <div class="status-indicator bg-success"></div>
                                        </div>
                                        <div>
                                            <div class="text-truncate">
                                                Am I a good boy? The reason I ask is because someone
                                           told me that people say this to all dogs, even if they aren't good...
                                            </div>
                                            <div class="small text-gray-500">Soubhagya Mohanty · 2w</div>
                                        </div>
                                    </a>
                                    <a class="dropdown-item text-center small text-gray-500" href="#">Read More Messages</a>
                                </div>
                            </li>

                            <div class="topbar-divider d-none d-sm-block"></div>

                            <!-- Nav Item - User Information -->
                            <li class="nav-item dropdown no-arrow">
                                <a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button"
                                    data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                    <asp:Label ID="userName" class="mr-2 d-none d-lg-inline text-gray-600 small" runat="server" Text=""></asp:Label>
                                    <img class="img-profile rounded-circle"
                                        src="img/undraw_profile.svg">
                                </a>
                                <!-- Dropdown - User Information -->
                                <div class="dropdown-menu dropdown-menu-right shadow animated--grow-in"
                                    aria-labelledby="userDropdown">

                                    <asp:LinkButton CssClass="dropdown-item" ID="btnLogout" runat="server" OnClick="btnLogout_Click">
                                   <i class="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-gray-400"></i>Logout</asp:LinkButton>


                                </div>
                            </li>

                        </ul>

                    </nav>
                    <!-- End of Topbar -->

                    <!-- Begin Page Content -->
                    <div class="container-fluid">

                        <!-- Page Heading -->
                        <h1 class="h3 mb-4 text-gray-800" style="text-align: center">Other Digital Services</h1>

                        <div class="table-responsive gridview-container">
                            <asp:GridView
                                ID="OtherServicesGV"
                                runat="server"
                                AutoGenerateColumns="False"
                                OnRowCommand="OtherServicesGV_RowCommand"
                                DataKeyNames="OthService_Id"
                                CssClass="table table-striped">
                                <Columns>
                                    <asp:BoundField DataField="Sl_No" HeaderText="Sl. No." SortExpression="Column1" />
                                    <asp:BoundField DataField="OthService_Id" HeaderText="Service Id" SortExpression="Column2" />
                                    <asp:BoundField DataField="Service_Name" HeaderText="Service Name" SortExpression="Column3" />
                                    <asp:BoundField DataField="Department_Name" HeaderText="Department Name" SortExpression="Column4" />
                                    <asp:BoundField DataField="Service_Details" HeaderText="Service Details" SortExpression="Column5" />

                                    <asp:ButtonField HeaderText="View" ControlStyle-CssClass="btn btn-info" Text="Click Here" CommandName="View" />


                                </Columns>
                            </asp:GridView>
                        </div>

                        <!--<div id="contentPanel" runat="server">
                        <!-- Generated content will be added here -->
                        <!--</div>-->


                    </div>
                    <!-- /.container-fluid -->

                </div>
                <!-- End of Main Content -->

                <!-- Footer -->
                <footer class="sticky-footer bg-white">
                    <div class="container my-auto">
                        <div class="copyright text-center my-auto">
                            <span>Copyright &copy; Authentic Hire Technology Solution 2023</span>
                        </div>
                    </div>
                </footer>
                <!-- End of Footer -->

            </div>
            <!-- End of Content Wrapper -->

        </div>
        <!-- End of Page Wrapper -->

        <!-- Scroll to Top Button-->
        <a class="scroll-to-top rounded" href="#page-top">
            <i class="fas fa-angle-up"></i>
        </a>



        <!-- Bootstrap core JavaScript-->
        <script src="vendor/jquery/jquery.min.js"></script>
        <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

        <!-- Core plugin JavaScript-->
        <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

        <!-- Custom scripts for all pages-->
        <script src="js/sb-admin-2.min.js"></script>
    </form>
</body>

</html>
