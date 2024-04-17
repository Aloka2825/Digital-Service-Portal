using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dsportal.Login.ForgotPassword
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Forgot_btn_Click(object sender, EventArgs e)
        {

        }

        protected void LgnButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Login.aspx");
        }

        protected void RegButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Register.aspx");

        }


        private string getName(string email)
        {
            string name = "";
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT Name FROM [dbo].[UserRegister] WHERE E_mail = @email";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        name = reader["Name"].ToString();
                    }
                    else
                    {
                        name = "User";
                    }
                }
            }
            return name;
        }

        protected void Forgot_btn_Click1(object sender, EventArgs e)
        {
            con.Open();
            string sqlquery = "select E_mail from [dbo].[UserRegister] where E_mail='" + EmailTextBox.Text.ToString() + "'";
            SqlCommand cmd = new SqlCommand(sqlquery, con);
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                con.Close();

                int myRandom = new Random().Next(1000, 10000);
                string forgot_otp = myRandom.ToString();


                string email = EmailTextBox.Text;
                string name = getName(email);


                con.Open();
                string updateACC = "update [UserRegister] set Otp='" + forgot_otp + "' where E_mail = '" + EmailTextBox.Text.ToString() + "'";
                SqlCommand cmdUpdate = new SqlCommand(updateACC, con);
                cmdUpdate.ExecuteNonQuery();
                con.Close();

                MailMessage mail = new MailMessage();
                mail.To.Add(EmailTextBox.Text.ToString());
                mail.From = new MailAddress("connectdigitalserviceportal@gmail.com");
                mail.Subject = "OTP";

                string emailBody = "";
                emailBody += "<html>";
                emailBody += "<!DOCTYPE html>";
                emailBody += "<html lang='en'>";
                emailBody += "<head>";
                emailBody += "    <meta charset='UTF-8'>";
                emailBody += "    <meta name='viewport' content='width=device-width, initial-scale=1.0'>";
                emailBody += "    <title>Document</title>";
                emailBody += "    <style>";
                emailBody += "        *{";
                emailBody += "            margin: 0;";
                emailBody += "            padding: 0;";
                emailBody += "        }";
                emailBody += "        html body{";
                emailBody += "            background-color: rgb(133, 132, 130) ;";
                emailBody += "            display: flex;";
                emailBody += "            justify-content: center;";
                emailBody += "        }";
                emailBody += "        .container{";
                emailBody += "            margin-top:20px;";
                emailBody += "            width: 100%;";
                emailBody += "            height: auto;";
                emailBody += "            background-color: #fff;";
                emailBody += "            box-shadow: 1px 1px 4px gray;";
                emailBody += "        }";
                emailBody += "    </style>";
                emailBody += "</head>";
                emailBody += "<body>";
                emailBody += "    <div class='container'>";
                emailBody += "              <div >";
                emailBody += "                  <img src='https://ahtscarrier.com/img/Logowhitebg.png' style='width:35%;margin-left:32%;'>";
                emailBody += "              </div>";
                emailBody += "              <br>";
                emailBody += "              <div>";
                emailBody += "                <img src='https://ahtscarrier.com/img/socialforall/header.jpeg' style='width:100%; height:150px;'>";
                emailBody += "              </div>";
                emailBody += "              <br>";
                emailBody += "              <div>";
                emailBody += "                <h1 style='text-align:center;font-family:BIZ UDMincho Medium; margin-left:2%;'>Hello";
                emailBody += "                <b style='text-align:center;'>" + name + "</b>";
                emailBody += "                </h1>";
                emailBody += "              </div>";
                emailBody += "              <br>";
                emailBody += "              <div>";
                emailBody += "                <h2 style='text-align:center;font-family:BIZ UDMincho Medium; margin-left:2%;'>";
                emailBody += "                    Your Verification Code is :";
                emailBody += "                    <b style='text-align:center;'>" + forgot_otp + "</b>";
                emailBody += "                </h2>";
                emailBody += "              </div>";
                emailBody += "              <br><br>";
                emailBody += "              <hr>";
                emailBody += "              <br>";
                emailBody += "              <div style='height: 50px; width:100%; display:flex; justify-content:center;text-align: center;'>";
                emailBody += "                <div class='icon' style='height: 40px; width:40px; border-radius: 50%; ";
                emailBody += "                background-color: transparent;";
                emailBody += "                margin: 5px 2.5px;'>";
                emailBody += "                <a href='https://www.instagram.com/ahts23/' target='_blank'>";
                emailBody += "                    <div style='height: 100%; width: 100%; background-image: url(https://ahtscarrier.com/img/socialforall/instabg.png); background-size: cover;'></div>";
                emailBody += "                </a>";
                emailBody += "        </div>";
                emailBody += "                <div class='icon' style='height: 40px; width:40px; border-radius: 50%; ";
                emailBody += "                background-color: transparent;";
                emailBody += "                margin: 5px 2.5px;'>";
                emailBody += "                <a href='https://www.facebook.com/people/Authentic-Hire-technology-Solution/100091934478649/' target='_blank'>";
                emailBody += "                    <div style='height: 100%; width: 100%; background-image: url(https://ahtscarrier.com/img/socialforall/fbpic.png); background-size: cover;'></div>";
                emailBody += "                </a>";
                emailBody += "        </div>";
                emailBody += "                    <div class='icon' style='height: 40px; width:40px; border-radius: 50%; ";
                emailBody += "                    background-color: transparent;";
                emailBody += "                    margin: 5px 2.5px;'>";
                emailBody += "                    <a href='https://www.linkedin.com/in/authentic-hire-technology-solution-928019274/' target='_blank'>";
                emailBody += "                        <div style='height: 100%; width: 100%; background-image: url(https://ahtscarrier.com/img/socialforall/linkedinbg.png); background-size: cover;'></div>";
                emailBody += "                    </a>";
                emailBody += "            </div>";
                emailBody += "                    <div class='icon' style='height: 40px; width:40px; border-radius: 50%; ";
                emailBody += "                    background-color: transparent;";
                emailBody += "                    margin: 5px 2.5px;'>";
                emailBody += "                    <a href='https://twitter.com/i/flow/login?redirect_after_login=%2FAhts2023' target='_blank'>";
                emailBody += "                        <div style='height: 100%; width: 100%; background-image: url(https://ahtscarrier.com/img/socialforall/twiterbg.png); background-size: cover;'></div>";
                emailBody += "                    </a>";
                emailBody += "            </div>";
                emailBody += "                    <div class='icon' style='height: 40px; width:40px; border-radius: 50%; ";
                emailBody += "                    background-color: transparent;";
                emailBody += "                    margin: 5px 2.5px;'>";
                emailBody += "                    <a href='https://www.youtube.com/@authentichiretechnology' target='_blank'>";
                emailBody += "                        <div style='height: 100%; width: 100%; background-image: url(https://ahtscarrier.com/img/socialforall/youtubeimg.png); background-size: cover;'></div>";
                emailBody += "                    </a>";
                emailBody += "            </div>";
                emailBody += "              </div>";
                emailBody += "              <br><br>";
                emailBody += "              <p style='text-align:center;'>Copyright © NEW PAGE";
                emailBody += "                All rights reserved. Desigend by <a href='https://ahtscarrier.com/'>AHTS</a>";
                emailBody += "                </p>";
                emailBody += "                <br>";
                emailBody += "              <hr>";
                emailBody += "    </div>";
                emailBody += "</body>";
                emailBody += "</html>";
                mail.Body = emailBody;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential("connectdigitalserviceportal@gmail.com", "lhwy zoqq yvvq gult");
                smtp.Send(mail);

                // Show OTP verification elements
                otpVerification.Visible = true;


                LabelError.Text = "OTP Sent Successfully";
                LabelError.ForeColor = System.Drawing.Color.Green;


            }
            else
            {
                LabelError.Text = "Your Email is not Valid.";
                LabelError.ForeColor = System.Drawing.Color.Red;
                con.Close();
            }
        }

        protected void VerifyOTPButton_Click1(object sender, EventArgs e)
        {
            // Retrieve the entered OTP
            string enteredOTP = OTPTextBox.Text;

            string E_Mail2 = EmailTextBox.Text.Trim();

            // Retrieve the stored OTP from the database for the user with the given email
            string storedOTP = "";

            // Establish a connection to your database and retrieve the OTP based on the userEmail
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string sqlquery = "SELECT Otp FROM [UserRegister] WHERE E_mail = @E_Mail2";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                cmd.Parameters.AddWithValue("@E_Mail2", E_Mail2);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    storedOTP = reader["Otp"].ToString();
                }
            }
            Response.Write(storedOTP);

            if (enteredOTP == storedOTP)
            {
                // OTPs match, so redirect to the reset password page
                Response.Redirect("~/Login/ForgotPassword/ResetPassword.aspx");


            }
            else
            {
                // OTPs do not match
                LabelError.Text = "OTP did not match. Please try again.";
                LabelError.ForeColor = System.Drawing.Color.Red;
            }
        }


    }
}