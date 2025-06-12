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

namespace OtpApp
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveNext_Click(object sender, EventArgs e)
        {
            string fullname = textBox1.Text.Trim();
            DateTime dob = dateTimePicker1.Value;

            if (string.IsNullOrWhiteSpace(fullname))
            {
                MessageBox.Show("Please enter the father name .");
                return;
            }

            // Here, you can store the values in a database or in memory
            MessageBox.Show($"Saved: {fullname} - {dob.ToShortDateString()}");

            // Move to next tab (if not last)
            if (tabControl1.SelectedIndex < tabControl1.TabCount - 1)
                tabControl1.SelectedIndex++;
        }

        private void btnSaveNext1_Click(object sender, EventArgs e)
        {
            string fullname = textBox2.Text.Trim();
            DateTime dob = dateTimePicker2.Value;

            if (string.IsNullOrWhiteSpace(fullname))
            {
                MessageBox.Show("Please enter the Mother name .");
                return;
            }

            // Here, you can store the values in a database or in memory
            MessageBox.Show($"Saved: {fullname} - {dob.ToShortDateString()}");

            // Move to next tab (if not last)
            if (tabControl1.SelectedIndex < tabControl1.TabCount - 2)
                tabControl1.SelectedIndex++;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            string connectionString = " Server = PRADNYEYA\\SQLEXPRESS01; Database = DPS; Integrated Security = True; TrustServerCertificate = True";
            string fatherName = textBox1.Text.Trim();
            DateTime fatherDOB = dateTimePicker1.Value;

            string motherName = textBox2.Text.Trim();
            DateTime motherDOB = dateTimePicker2.Value;

            string studentName = textBox3.Text.Trim();
            DateTime studentDOB = dateTimePicker3.Value;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Registration (FatherName, FatherDOB, MotherName, MotherDOB, StudentName, StudentDOB) " +
                    "VALUES (@fatherName, @fatherDOB, @motherName, @motherDOB, @studentName, @studentDOB)", conn);

                cmd.Parameters.AddWithValue("@fatherName", fatherName);
                cmd.Parameters.AddWithValue("@fatherDOB", fatherDOB);
                cmd.Parameters.AddWithValue("@motherName", motherName);
                cmd.Parameters.AddWithValue("@motherDOB", motherDOB);
                cmd.Parameters.AddWithValue("@studentName", studentName);
                cmd.Parameters.AddWithValue("@studentDOB", studentDOB);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                    MessageBox.Show("Registration successful!");
                else
                    MessageBox.Show("Registration failed!");
                string fullname = textBox3.Text.Trim();
                string Class = textBox4.Text.Trim();
                DateTime dob = dateTimePicker3.Value;

                if (string.IsNullOrWhiteSpace(fullname))
                {
                    MessageBox.Show("Please enter the Student name .");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Class))
                {
                    MessageBox.Show("Please enter the Class .");
                    return;
                }


                // Here, you can store the values in a database or in memory
                MessageBox.Show($"Saved: {fullname} - {Class}- {dob.ToShortDateString()}");

                // Move to next tab (if not last)
                if (tabControl1.SelectedIndex < tabControl1.TabCount - 3)
                    tabControl1.SelectedIndex++;
            }
        }
    }
}
