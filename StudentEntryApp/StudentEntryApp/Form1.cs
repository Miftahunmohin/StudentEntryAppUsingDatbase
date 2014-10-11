using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentEntryApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string Name = nameTextBox.Text;
            string Email = emailTextBox.Text;
            string Address = AddressTextBox.Text;
            string conn = @"server=BITM-401-PC10\SQLEXPRESS;database=ABC_University;integrated security=true";
            SqlConnection connection = new SqlConnection(conn);

            connection.Open();

            string queryDatabase = String.Format("INSERT INTO Student_Table VALUES('{0}','{1}','{2}')", Name, Email, Address);
            SqlCommand command = new SqlCommand(queryDatabase, connection);
            int affected = command.ExecuteNonQuery();
            connection.Close();

            if (affected > 0)
            {
                MessageBox.Show("succes");
            }

            else
            {
                MessageBox.Show("some problem");
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            string conn = @"server=BITM-401-PC10\SQLEXPRESS;database=ABC_University;integrated security=true";
            SqlConnection connection = new SqlConnection(conn);

            connection.Open();

            string queryDatabase = String.Format("SELECT * FROM Student_Table");
            SqlCommand command = new SqlCommand(queryDatabase, connection);
            SqlDataReader aReader = command.ExecuteReader();
   
            List<Student> studedList=new List<Student>();
            if (aReader.HasRows)
            {
                while (aReader.Read())
                {
                    Student aStudent=new Student();
                    aStudent.StudentId = (int)aReader[0];
                    aStudent.Name = aReader[1].ToString();
                    aStudent.Email = aReader[2].ToString();
                    aStudent.Address = aReader[3].ToString();
                    studedList.Add(aStudent);
                }
            }
            connection.Close();
            dataGridView1.DataSource=studedList;
        }
    }
}
