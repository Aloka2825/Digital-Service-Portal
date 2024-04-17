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
    public partial class CscServices : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        string email = "";
        string userId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emailId"] != null)
            {
                email = Session["emailId"].ToString();
            }
            else
            {


                string script = "swal({ title: 'Something Wents Wrong', text: 'Click Ok to Continue!', icon: 'warning' }).then(function() { window.location = '../Login/Login.aspx'; },1000);";
                ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script, true);

                // Return from the server-side code
                return;

            }
            if (!IsPostBack)
            {

                //GetServiceName();
                CscServicesGrid();
                DisplayUserName();
                BindSerchView("");
            }
        }
        private void CscServicesGrid()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string query = "SELECT ROW_NUMBER() OVER (ORDER BY CscService_Id) AS [Sl_No], * FROM [dbo].[tbl_Csc_Services]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    CscServicesGV.DataSource = datatable;
                    CscServicesGV.DataBind();
                }
            }
        }

        private void DisplayUserName()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string query = "SELECT Name,Id, Ahts_Id FROM [dbo].[UserRegister] WHERE E_mail = @EmailId";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Assuming EmailId is the column name in the UserRegister table
                cmd.Parameters.AddWithValue("@EmailId", email);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    userName.Text = reader["Name"].ToString();
                    userId = reader["Id"].ToString();
                }

                reader.Close();
            }
        }


        protected void CscServicesGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                // Get the row index from the command argument
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Access the data key value for the selected row
                int cscServiceId = Convert.ToInt32(CscServicesGV.DataKeys[rowIndex].Value);

                // Retrieve the Service_Link based on CscService_Id
                string serviceLink = GetServiceLink(cscServiceId);

                // Redirect to the ProjectStatus page with the Service_Link value
                string script = "window.open('" + serviceLink + "', '_blank');";
                ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", script, true);
            }
        }

        private string GetServiceLink(int cscServiceId)
        {
            string serviceLink = "";

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string query = "SELECT Service_Link FROM [dbo].[tbl_Csc_Services] WHERE [CscService_Id] = @CscServiceId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CscServiceId", cscServiceId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    serviceLink = reader["Service_Link"].ToString();
                }

                reader.Close();
            }

            return serviceLink;
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
                string searchquery = "SELECT ROW_NUMBER() OVER (ORDER BY CscService_Id) AS [Sl_No], * FROM [dbo].[tbl_Csc_Services] WHERE [Service_Name] LIKE '%' + @searchTerm + '%' OR [Department_Name] LIKE '%' + @searchTerm + '%'";

                using (SqlCommand cmd = new SqlCommand(searchquery, conn))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    CscServicesGV.DataSource = ds.Tables[0];
                    CscServicesGV.DataBind();
                }
            }
        }


    }
}