using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dsportal.User
{
    public partial class ViewAhtsServices : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        string email = "";
        string userId = "";
        int projectId;
        // Global variables to store data from URL
        private string paymentStatus;
        private int urlProjectId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emailId"] != null || Session["ProjectId"] != null)
            {
                email = Session["emailId"].ToString();

            }
            else
            {


                // Use JavaScript to delay the redirect by 1 second
                string script = "swal({ title: 'Something Wents Wrong', text: 'Click Ok to Continue!', icon: 'warning' }).then(function() { window.location = '../Login/Login.aspx'; },1000);";
                ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script, true);
                //Response.Write("<script>setTimeout(function() { window.location = '../Login/Login.aspx'; }, 1000);</script>");

                // Return from the server-side code
                return;

            }
            if (!IsPostBack)
            {
                ViewAhtsServicesGrid();
                DisplayUserName();
                BindSerchView("");
                UpdateGridView();
            }
            // Get the current page's URL
            string currentUrl = HttpContext.Current.Request.Url.AbsoluteUri;

            // Call the function to retrieve data from the URL
            GetDataFromUrl(currentUrl);

            


            UpdatePaymentStatus(urlProjectId, paymentStatus);


        }

        public void DisplayUserName()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string query = "SELECT Id,Name FROM [dbo].[UserRegister] WHERE E_mail = @EmailId";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Assuming EmailId is the column name in the UserRegister table
                cmd.Parameters.AddWithValue("@EmailId", email);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    userName.Text = reader["Name"].ToString();
                    //userId = reader["Id"].ToString();
                }

                reader.Close();
            }
        }

        public string GetUserId()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string query = "SELECT Id FROM [dbo].[UserRegister] WHERE E_mail = @EmailId";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Assuming EmailId is the column name in the UserRegister table
                cmd.Parameters.AddWithValue("@EmailId", email);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    //userName.Text = reader["Name"].ToString();
                    userId = reader["Id"].ToString();
                }

                reader.Close();
            }
            return userId;
        }

        private void ViewAhtsServicesGrid()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT ROW_NUMBER() OVER (ORDER BY Project_Id) AS [Sl_No],* FROM [dbo].[tbl_Project_Report] WHERE User_Id = @UserId";
                string user = GetUserId().ToString();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Assuming userId is an int, replace SqlDbType.Int with the appropriate SqlDbType if it's a different type.
                    cmd.Parameters.AddWithValue("@UserId", user);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    ViewAhtsServicesGV.DataSource = datatable;
                    ViewAhtsServicesGV.DataBind();
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["emailId"] = null;
            Response.Redirect("../Login/Login.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = searchTextbox.Text.Trim();
            BindSerchView(searchTerm);
        }

        private void BindSerchView(string searchTerm)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string searchquery = "SELECT ROW_NUMBER() OVER (ORDER BY Project_Id) AS [Sl_No], * FROM [dbo].[tbl_Project_Report] WHERE [Project_Name] LIKE '%' + @searchTerm + '%' OR [Customer_Name] LIKE '%' + @searchTerm + '%' OR [Customer_Mobile] LIKE '%' + @searchTerm + '%'";

                using (SqlCommand cmd = new SqlCommand(searchquery, conn))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    ViewAhtsServicesGV.DataSource = ds.Tables[0];
                    ViewAhtsServicesGV.DataBind();
                }
            }
        }

        protected void ViewAhtsServicesGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Payment")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = ViewAhtsServicesGV.Rows[index];
                int projectId = Convert.ToInt32(selectedRow.Cells[1].Text); // Assuming ProjectId is in the second cell

                Session["ProjectId"] = projectId;
                projectId = (int)Session["ProjectId"];
                Response.Redirect($"https://ahtscarrier.com/Payment%20Gateway/DSP_Pay.php?ProjectId={projectId}");



            }
        }

        // New function to retrieve data from a URL and store it into variables
        private void GetDataFromUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                var queryParameters = HttpUtility.ParseQueryString(uri.Query);

                // Extract values for PaymentStatus and ProjectId
                paymentStatus = queryParameters["PaymentStatus"];
                urlProjectId = int.Parse(queryParameters["ProjectId"]);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void UpdatePaymentStatus(int urlProjectId, string paymentStatus)
        {
            if (!string.IsNullOrEmpty(paymentStatus))
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();

                    string query = "UPDATE [dbo].[tbl_Project_Report] SET Payment_Status = @PaymentStatus WHERE Project_Id = @ProjectId";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Handle null value for paymentStatus
                    if (paymentStatus == null)
                    {
                        cmd.Parameters.AddWithValue("@PaymentStatus", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus);
                    }

                    cmd.Parameters.AddWithValue("@ProjectId", urlProjectId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateGridView()
        {
            // Call your existing method to fetch and bind data to the GridView
            ViewAhtsServicesGrid();
        }


    }
}