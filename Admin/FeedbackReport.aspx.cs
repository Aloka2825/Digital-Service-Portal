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
    public partial class FeedbackReport : System.Web.UI.Page
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
                FeedbackReportGrid();
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

        private void FeedbackReportGrid()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT Feedback_Id, Name, Mobile_No, Issue, User_Id FROM [dbo].[tbl_Feedback]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    FeedbackReportGV.DataSource = datatable;
                    FeedbackReportGV.DataBind();
                }
            }
        }
        protected void FeedbackReportGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(FeedbackReportGV.DataKeys[e.RowIndex].Value);

            //Delete Code
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "DELETE FROM [dbo].[tbl_Feedback] WHERE Feedback_Id =" + id;
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAffected > 0)
                    {
                        FeedbackReportGrid();
                        string script = "swal({ title: 'Query Solved!', text: 'Query Solved! Click OK to Continue!', icon: 'success' }).then(function() {  });";
                        ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script, true);

                        string script10 = "setTimeout(function() { window.location.href = '" + Request.RawUrl + "'; }, 2000);";
                        ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script10, true);
                    }
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
                string searchquery = "SELECT ROW_NUMBER() OVER (ORDER BY Feedback_Id) AS [Sl_No], * FROM [dbo].[tbl_Feedback] WHERE [Name] LIKE '%' + @searchTerm + '%' OR [Mobile_No] LIKE '%' + @searchTerm + '%' OR [User_Id] LIKE '%' + @searchTerm + '%'";

                using (SqlCommand cmd = new SqlCommand(searchquery, conn))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    FeedbackReportGV.DataSource = ds.Tables[0];
                    FeedbackReportGV.DataBind();
                }
            }
        }
    }
}