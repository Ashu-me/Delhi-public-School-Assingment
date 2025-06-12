using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure.Core;
using RestSharp;
using Twilio;
using Twilio.Http;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Voice;


namespace OtpApp
{
    public partial class Form1 : Form
    
    {
        private string connectionString = "Server=PRADNYEYA\\SQLEXPRESS01;Database=DPS;Integrated Security=True;TrustServerCertificate=True;";
        private string generatedOTP = "";

      
        


        public Form1()
        {
            InitializeComponent();
        }
        private void linkResend_Load(object sender, EventArgs e)
        {
           
        }

        private async void button1_Click(object sender, EventArgs e)
        {
          string mobile = txtMobile.Text.Trim();
            generatedOTP = new Random().Next(100000, 999999).ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO OTP_Records (MobileNo, OTP) VALUES (@mobile, @otp)", conn);
                cmd.Parameters.AddWithValue("@mobile", mobile);
                cmd.Parameters.AddWithValue("@otp", generatedOTP);
                cmd.ExecuteNonQuery();
            }

            await SendOTPAsync(mobile, generatedOTP);
            MessageBox.Show("OTP Sent!");

        }


       

        private void button2_Click(object sender, EventArgs e)
        {
            string mobile = txtMobile.Text.Trim();
            string enteredOTP = txtyOtp.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 OTP FROM OTP_Records WHERE MobileNo = @mobile ORDER BY CreatedAt DESC", conn);
                cmd.Parameters.AddWithValue("@mobile", mobile);
                var dbOTP = cmd.ExecuteScalar() as string;

                if (enteredOTP == dbOTP)
                    MessageBox.Show("OTP Validated Successfully!");
                else
                    MessageBox.Show("Invalid OTP.");
            }


        }

        private async void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string mobile = txtMobile.Text.Trim();
            generatedOTP = new Random().Next(100000, 999999).ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO OTP_Records (MobileNo, OTP) VALUES (@mobile, @otp)", conn);
                cmd.Parameters.AddWithValue("@mobile", mobile);
                cmd.Parameters.AddWithValue("@otp", generatedOTP);
                cmd.ExecuteNonQuery();
            }

            await SendOTPAsync(mobile, generatedOTP);
            MessageBox.Show("OTP Re-sent!");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          Mainform Regform = new Mainform();
            Regform.Show();
            this.Hide();

        }
        private async System.Threading.Tasks.Task SendOTPAsync(string mobile, string otp)
        {
            var client = new RestClient();
            var request = new RestRequest("https://www.fast2sms.com/dev/bulkV2", Method.Post);
            request.AddHeader("authorization", "TdNqI8GtuAsY2JBKEgf5oVjSR7UzLn1plZihPQeMawCv4DO0HbXrwRm1ZETfUSsApFdJWVuva9nyiH7z");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("sender_id", "FSTSMS");
            request.AddParameter("message", $"Your OTP is {otp}");
            request.AddParameter("language", "english");
            request.AddParameter("route", "q");
            request.AddParameter("numbers", mobile);

            RestResponse response = await client.ExecuteAsync(request);
            // Optional: MessageBox.Show(response.Content);
        }
    }
}
