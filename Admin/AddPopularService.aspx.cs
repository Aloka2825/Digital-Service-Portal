using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dsportal.Admin
{
    public partial class AddPopularService : System.Web.UI.Page
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
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(servicenameTextbox.Text) || string.IsNullOrEmpty(departmentnameTextbox.Text) || string.IsNullOrEmpty(serviceUrlTextBox.Text) || string.IsNullOrEmpty(servicedetailsTextbox.Text) || string.IsNullOrEmpty(adminidTextbox.Text))
            {
                string script = "swal({ title: 'Please fill all the blanks!', text: 'All fields are mandatory! Click OK to Continue!', icon: 'warning' }).then(function() {  });";
                ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script, true);
            }
            else
            {
                string servicename = servicenameTextbox.Text;
                string department = departmentnameTextbox.Text;
                string servicedetails = servicedetailsTextbox.Text;
                string adminid = adminidTextbox.Text;
                string serviceurl = serviceUrlTextBox.Text;

                try
                {
                    using (SqlConnection conn = new SqlConnection(cs))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[tbl_Popular_Services] (Service_Name, Department_Name, Service_Details, Admin_Id, Service_Photo, Service_Link) VALUES (@ServiceName, @Department, @ServiceDetails, @AdminId, @FilePath, @ServiceUrl)", conn))
                        {
                            cmd.Parameters.AddWithValue("@ServiceName", servicename);
                            cmd.Parameters.AddWithValue("@Department", department);
                            cmd.Parameters.AddWithValue("@ServiceDetails", servicedetails);
                            cmd.Parameters.AddWithValue("@AdminId", adminid);
                            cmd.Parameters.AddWithValue("@ServiceUrl", serviceurl);

                            if (FileUpload1.HasFile)
                            {
                                string fileName = FileUpload1.FileName;
                                string fileExtension = Path.GetExtension(fileName);
                                string newFilename = servicename + fileExtension;

                                if (fileExtension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                    fileExtension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                    fileExtension.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
                                    fileExtension.Equals(".gif", StringComparison.OrdinalIgnoreCase))
                                {
                                    string filePath = Server.MapPath("~/Admin/Services/") + newFilename;

                                    FileUpload1.SaveAs(filePath);
                                    cmd.Parameters.AddWithValue("@FilePath", newFilename);
                                }
                                else
                                {
                                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Only jpg, jpeg, png, and gif files are allowed.');", true);
                                    string script1 = "swal({ title: 'Only jpg, jpeg, png, and gif files are allowed.!', text: 'Only jpg, jpeg, png, and gif files are allowed. Click OK to Continue!', icon: 'warning' }).then(function() {  });";
                                    ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script1, true);
                                    return;
                                }
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@FilePath", DBNull.Value);
                            }

                            cmd.ExecuteNonQuery();

                            servicenameTextbox.Text = "";
                            departmentnameTextbox.Text = "";
                            servicedetailsTextbox.Text = "";
                            adminidTextbox.Text = "";
                            serviceUrlTextBox.Text = "";

                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "SuccessScript", "alert('Service C');", true);
                            string script2 = "swal({ title: 'Service Added!', text: 'Service Added Successfully! Click OK to Continue!', icon: 'success' }).then(function() {  });";
                            ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script2, true);

                            string script10 = "setTimeout(function() { window.location.href = '" + Request.RawUrl + "'; }, 2000);";
                            ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script10, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log them, or display an error message
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
                }
            }
        }
    }
}