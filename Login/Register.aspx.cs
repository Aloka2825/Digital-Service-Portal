using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dsportal.Login
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void registerbtn_Click(object sender, EventArgs e)
        {
            if (nameTextbox.Text == "" || mobTextbox.Text == "" || emailTextbox.Text == "" || addressTextbox.Text == "" || passwordTextbox.Text == "")
            {
                Response.Write("<script>alert('Must Fill the field')</script>");
            }
            else
            {
                string name = nameTextbox.Text;
                string mob = mobTextbox.Text;
                string email = emailTextbox.Text;
                string address = addressTextbox.Text;
                string password = passwordTextbox.Text;


                // Assuming InsertUser returns true for a successful registration
                if (InsertUser(name, mob, email, address, password))
                {
                    // Registration successful, show SweetAlert using ClientScript
                    string script = "swal({ title: 'Thank You for Register!', text: 'Registration successful! Click OK to visit the login page.', icon: 'success' }).then(function() { window.location = '/Login/Login.aspx'; });";
                    ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script, true);
                }
                else
                {
                    // Registration failed, you might want to handle this case differently
                    Response.Write("<script>alert('Registration failed. Please try again.');</script>");
                }
            }
        }

        private bool InsertUser(string name, string mob, string email, string address, string password)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                string chkquery = "SELECT COUNT(*) FROM [dbo].[UserRegister] WHERE [E_mail] = @E_mail";
                SqlCommand chkcmd = new SqlCommand(chkquery, connection);

                chkcmd.Parameters.AddWithValue("@E_mail", emailTextbox.Text);

                int count = (int)chkcmd.ExecuteScalar();
                connection.Close();

                if (count > 0)
                {
                    Response.Write("<script>alert('Email already exists.')</script>");
                    return false;
                }
                else
                {


                    connection.Open();

                    string query = "INSERT INTO [dbo].[UserRegister] (Name, Mob, E_mail, Address, Password)" +
                                   "VALUES(@Name, @Mob, @E_mail, @Address, @Password)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Mob", mob);
                        cmd.Parameters.AddWithValue("@E_mail", email);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@Password", password);

                        // Use ExecuteNonQuery to execute the insert query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Assuming the registration is successful if at least one row is affected
                        return rowsAffected > 0;
                    }
                }
            }
        }
    }
}
