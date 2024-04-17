﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dsportal.Admin
{
    public partial class ProjectStatus : System.Web.UI.Page
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
                ProjectStatusGrid();

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

        private void ProjectStatusGrid()
        {
            string userId = Request.QueryString["UserID"].ToString();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM [dbo].[tbl_Project_Report] WHERE User_Id =" + userId;

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    UserProjectReportGV.DataSource = datatable;
                    UserProjectReportGV.DataBind();
                }
            }
        }

        protected void UserProjectReportGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(UserProjectReportGV.DataKeys[e.RowIndex].Value);

            //Delete Code
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "DELETE FROM [dbo].[tbl_Project_Report] WHERE Project_Id =" + id;
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAffected > 0)
                    {
                        ProjectStatusGrid();
                        string script = "swal({ title: 'Service Removed!', text: 'Service Removed Successfully! Click OK to Continue!', icon: 'success' }).then(function() {  });";
                        ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script, true);

                        string script10 = "setTimeout(function() { window.location.href = '" + Request.RawUrl + "'; }, 2000);";
                        ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script10, true);
                    }
                }
            }
        }

        protected void UserProjectReportGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Set the GridView's EditIndex property to the index of the row being edited
            UserProjectReportGV.EditIndex = e.NewEditIndex;

            // Rebind the data to the GridView
            ProjectStatusGrid();

            // Iterate through all rows to find the "btnSave" button and set its visibility
            for (int i = 0; i < UserProjectReportGV.Rows.Count; i++)
            {
                GridViewRow row = UserProjectReportGV.Rows[i];
                Button btnSave = (Button)row.FindControl("btnSave");
                Button btnEdit = (Button)row.FindControl("btnEdit");

                // Check if the button is found before attempting to set its visibility
                if (btnSave != null)
                {
                    btnSave.Visible = (i == e.NewEditIndex); // Show the button only for the edited row
                    btnEdit.Visible = !btnSave.Visible;
                }
            }
        }

        protected void UserProjectReportGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = UserProjectReportGV.Rows[e.RowIndex];
            int id = Convert.ToInt32(UserProjectReportGV.DataKeys[e.RowIndex].Values[0]);
            //string name = ((TextBox)row.Cells[1].Controls[0]).Text;
            string status = ((TextBox)row.Cells[2].Controls[0]).Text;


            // Update the database with the new values
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE [dbo].[tbl_Project_Report] SET  Completion_Value = @status WHERE Project_Id=@ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@status", status);


                    cmd.ExecuteNonQuery();

                    string script10 = "setTimeout(function() { window.location.href = '" + Request.RawUrl + "'; }, 2000);";
                    ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script10, true);
                }
            }

            UserProjectReportGV.EditIndex = -1;
            ProjectStatusGrid();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}