using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BloodBank
{
    public partial class SignUp2 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");

        public SignUp2()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            SignUp s1 = new SignUp();
            s1.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            SignUp3 s3 = new SignUp3();
            s3.Show();
            this.Hide();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SignUp s1 = new SignUp();
            s1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignUp3 s3 = new SignUp3();
            s3.Show();
            this.Hide();
        }

        private void closebtn_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }

        private void closebtn_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_name.Text) || string.IsNullOrEmpty(tb_pass.Text) || string.IsNullOrEmpty(tb_contact.Text) || string.IsNullOrEmpty(cb_area.GetItemText(cb_area.SelectedItem)))
            {
                MessageBox.Show("Complete all fields");
            }
            else
            {
                con.Open();
                string name = tb_name.Text;
                string pass = tb_pass.Text;
                string contact = tb_contact.Text;
                int area_id = Convert.ToInt32(cb_area.GetItemText(cb_area.SelectedValue));
                string sql = "Insert into hospital (Area_idArea,fpassword,Hospital_Name,Contact_no)";
                sql += "values (" + area_id + ",'" + pass + "','" + name + "'," + contact + ")";


                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                MessageBox.Show("Successfully Registered");
                con.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignUp2_Load(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select idArea,final_Name as [Area Name]  from Area ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable table1 = new DataTable();
            da.Fill(table1);
            cb_area.DataSource = table1;
            cb_area.DisplayMember = "Area Name";
            cb_area.ValueMember = "idArea";
            con.Close();
        }
    }
}
