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

namespace CRUD_with_LINQ_in_CSharp
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Data Source=B1T3M3\\SQLEXPRESS;Initial Catalog=CRUD_linQ_Tutorial;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConnectionString);

        private void button1_Click(object sender, EventArgs e)
        {
                con.Open();
                SqlCommand command = new SqlCommand("insert into Products_Tester values ('" + int.Parse(textBox1.Text) + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + comboBox1.Text + "', getdate(), getdate())", con);
                command.ExecuteNonQuery();
                MessageBox.Show("Successfully Inserted.");
                con.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = "";
                textBox1.Focus();
                BindData();
        }

        void BindData()
        {
            SqlCommand command = new SqlCommand("select * from Products_Tester",con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand("update Products_Tester set ItemName = '"+textBox2.Text+ "', Design = '" + textBox3.Text + "', Color = '" + comboBox1.Text + "', UpdateDate = getdate() where ProductID= '"+int.Parse(textBox1.Text)+"'", con);
            command.ExecuteNonQuery(); 
            con.Close();
            MessageBox.Show("Successfully Updated!");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            textBox1.Focus();
            BindData();

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if(MessageBox.Show("Are you sure you want to delete?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("Delete Products_Tester where ProductID= '" + int.Parse(textBox1.Text) + "'", con);
                    command.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully deleted!");
                    BindData();
                }
                
            }
            else
            {
                MessageBox.Show("Input Product ID");
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from Products_Tester where ProductID = '"+int.Parse(textBox1.Text)+"'", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
