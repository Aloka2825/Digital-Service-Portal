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
    public partial class Dashboard : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        string email = "";
        string AHTS_ID = "";
        string progress = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emailId"] != null)
            {
                email = Session["emailId"].ToString();
            }
            else
            {
                //Response.Write("<script>alert('Session Not Exist');</script>");

                // Use JavaScript to delay the redirect by 1 second
                string script = "swal({ title: 'Something Wents Wrong', text: 'Click Ok to Continue!', icon: 'warning' }).then(function() { window.location = '../Login/Login.aspx'; },1000);";
                ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script, true);

                // Return from the server-side code
                return;

            }
            if (!IsPostBack)
            {

                DisplayAdminName();
                UserDetailsGrid();
                BindSerchView("");
            }
        }

        public void DisplayAdminName()
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

        private void UserDetailsGrid()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT ROW_NUMBER() OVER (ORDER BY Id) AS [Sl_No], [Id],[Name],[Mob],[E_mail],[Address],[Password] FROM [dbo].[UserRegister]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    UserDetailsGV.DataSource = datatable;
                    UserDetailsGV.DataBind();
                }
            }
        }

        protected void UserDetailsGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        protected void UserDetailsGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int id = Convert.ToInt32(UserDetailsGV.DataKeys[e.RowIndex].Value);

            //Delete Code
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "DELETE FROM [dbo].[UserRegister] WHERE Id =" + id;
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAffected > 0)
                    {
                        UserDetailsGrid();
                        string script = "swal({ title: 'User Removed!', text: 'User Removed Successfully! Click OK to Continue!', icon: 'success' }).then(function() {  });";
                        ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script, true);
                    }
                    else
                    {
                        string script5 = "swal({ title: 'Removation Failed!', text: 'User Deletion Failed! Click OK to Continue!', icon: 'danger' }).then(function() {  });";
                        ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script5, true);
                    }
                }
            }




        }


        protected void UserDetailsGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "UpdateProjectStatus")
            {
                // Get the row index from the command argument
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Access the data key value for the selected row
                int userId = Convert.ToInt32(UserDetailsGV.DataKeys[rowIndex].Value);

                Response.Redirect("~/Admin/ProjectStatus.aspx?UserID=" + userId);
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
                string searchquery = "SELECT ROW_NUMBER() OVER (ORDER BY Id) AS [Sl_No], * FROM [dbo].[UserRegister] WHERE [Name] LIKE '%' + @searchTerm + '%' OR [Mob] LIKE '%' + @searchTerm + '%' OR [E_mail] LIKE '%' + @searchTerm + '%' OR [Address] LIKE '%' + @searchTerm + '%'";

                using (SqlCommand cmd = new SqlCommand(searchquery, conn))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    UserDetailsGV.DataSource = ds.Tables[0];
                    UserDetailsGV.DataBind();
                }
            }
        }

    }
}