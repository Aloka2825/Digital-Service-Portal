using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dsportal.Admin
{
    public partial class ProjectReport : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        string email = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emailId"] != null)
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
                DisplayAdminName();
                ProjectReportGrid();
                BindSerchView("");
            }

        }
        private void DisplayAdminName()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string query = "SELECT Name FROM [dbo].[AdminRegister] WHERE E_mail = @EmailId";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Assuming EmailId is the column name in the UserRegister table
                cmd.Parameters.AddWithValue("@EmailId", email);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    userName.Text = reader["Name"].ToString();
                    //AHTS_ID = reader["Ahts_Id"].ToString();
                }

                reader.Close();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["emailId"] = null;
            Response.Redirect("../Login/Login.aspx");
        }
        private void ProjectReportGrid()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT Project_Id, Project_Name, User_Id, Customer_Name, Customer_Mobile, Project_File FROM [dbo].[tbl_Project_Report]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    ProjectReportGV.DataSource = datatable;
                    ProjectReportGV.DataBind();
                }
            }
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
                string searchquery = "SELECT ROW_NUMBER() OVER (ORDER BY Project_Id) AS [Sl_No], * FROM [dbo].[tbl_Project_Report] WHERE [Project_Name] LIKE '%' + @searchTerm + '%' OR [Customer_Name] LIKE '%' + @searchTerm + '%' OR [User_Id] LIKE '%' + @searchTerm + '%'";

                using (SqlCommand cmd = new SqlCommand(searchquery, conn))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    ProjectReportGV.DataSource = ds.Tables[0];
                    ProjectReportGV.DataBind();
                }
            }
        }
    }
}