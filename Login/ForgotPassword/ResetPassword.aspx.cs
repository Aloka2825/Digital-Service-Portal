using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dsportal.Login.ForgotPassword
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

            string forgot_otp = Request.QueryString["Otp"];
            string E_Mail2 = Request.QueryString["E_mail"];



            con.Open();
            string checkActivation = "SELECT Id FROM [UserRegister] WHERE E_mail = '" + E_Mail2 + "' AND Otp = '" + forgot_otp + "'";
            SqlCommand cmd = new SqlCommand(checkActivation, con);
            SqlDataReader dr = cmd.ExecuteReader();



            con.Close();



        }

        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString; // Replace with your actual connection string

        protected void Chng_Button1_Click(object sender, EventArgs e)
        {
            string E_Mail2 = Request.QueryString["E_mail"];
            Label1.Text = E_Mail2;
            Label1.ForeColor = System.Drawing.Color.Green;
            if (new_PWTextBox.Text == Confirm_PWTextBox1.Text)
            {
                con.Open();
                string updateAcc = "update [UserRegister] set Otp = 0,Password = '" + new_PWTextBox.Text + "' where E_mail = '" + E_Mail2 + "'";
                SqlCommand cmd = new SqlCommand(updateAcc, con);
                cmd.ExecuteNonQuery();
                Label1.Text = "Password Reset Successfully";
                Label1.ForeColor = System.Drawing.Color.Green;
                string script = "swal({ title: 'Password Changed Successfully!', text: 'Password Reset successfully! Click OK to visit the login page.', icon: 'success' }).then(function() { window.location = '../Login.aspx'; });";
                ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script, true);
                con.Close();


            }
            else
            {
                Label1.Text = "Password and confirm password not matched";
                Label1.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Login.aspx");
        }
    }
}