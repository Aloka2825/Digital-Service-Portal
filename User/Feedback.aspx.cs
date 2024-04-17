using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dsportal.User
{
    public partial class Feedback : System.Web.UI.Page
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
                //ServiceDropDownList();
                //GetUserId();
                DisplayUserName();
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
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["emailId"] = null;
            Response.Redirect("../Login/Login.aspx");
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(customernameTextbox.Text) || string.IsNullOrEmpty(mobilenumTextbox.Text) || string.IsNullOrEmpty(requirementTextbox.Text))
            {
                string script = "swal({ title: 'Please fill all the blanks!', text: 'All fields are mandatory! Click OK to Continue!', icon: 'warning' }).then(function() {  });";
                ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script, true);
            }
            else
            {

                string cust_name = customernameTextbox.Text;
                string mob = mobilenumTextbox.Text;
                //string email = emailTextBox.Text;
                string issue = requirementTextbox.Text;
                string userId = userIdTextbox.Text;
                //GetUserId();
                try
                {
                    using (SqlConnection conn = new SqlConnection(cs))
                    {
                        conn.Open();
                        //string userId = Request.QueryString["UserID"].ToString();
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[tbl_Feedback] (Name, Mobile_No, Issue, User_Id) VALUES (@Name, @Mob, @Issue, @UserId)", conn))
                        {
                            //cmd.Parameters.AddWithValue("@ServiceName", servicename);
                            cmd.Parameters.AddWithValue("@Name", cust_name);
                            cmd.Parameters.AddWithValue("@Mob", mob);
                            //cmd.Parameters.AddWithValue("@Customer_Email", email);
                            //cmd.Parameters.AddWithValue("@Customer_Address", address);
                            cmd.Parameters.AddWithValue("@Issue", issue);
                            cmd.Parameters.AddWithValue("@UserId", userId);


                            cmd.ExecuteNonQuery();


                            customernameTextbox.Text = "";
                            mobilenumTextbox.Text = "";
                            requirementTextbox.Text = "";

                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "SuccessScript", "alert('Service C');", true);
                            string script2 = "swal({ title: 'Feedback Submitted!', text: 'Feedback Submitted! Click OK to Continue!', icon: 'success' }).then(function() {  });";
                            ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script2, true);

                            string script10 = "setTimeout(function() { window.location.href = '" + Request.RawUrl + "'; }, 2000);";
                            ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script10, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    // Handle exceptions, log them, or display an error message
                    /*string script5 = "swal({ title: 'Service Addition Failed!', text: 'Service Addition Failed!', icon: 'warning' }).then(function() {  });";
                    ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script5, true);
                    return;*/

                }

            }
        }
        /*private string GetUserId()
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
                    userId = reader["Id"].ToString();
                }

                reader.Close();
            }
            return userId;
        }*/
    }
}