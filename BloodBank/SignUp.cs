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
    public partial class SignUp : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");

        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select idArea,final_Name as [Area Name]  from Area ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable table1 = new DataTable();
            da.Fill(table1);
            cb_Area.DataSource = table1;
            cb_Area.DisplayMember = "Area Name";
            cb_Area.ValueMember = "idArea";
            con.Close();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            SignUp2 s2 = new SignUp2();
            s2.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            SignUp2 s2 = new SignUp2();
            s2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignUp3 s3 = new SignUp3();
            s3.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }

        private void closebtn_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        { 
            if (string.IsNullOrEmpty(tb_name.Text) || string.IsNullOrEmpty(tb_contact.Text) || string.IsNullOrEmpty(tb_cnic.Text) || string.IsNullOrEmpty(tb_pass.Text)|| string.IsNullOrEmpty(cb_bg.GetItemText(cb_bg.SelectedIndex)) || string.IsNullOrEmpty(tb_adrss.Text) || string.IsNullOrEmpty(tb_dob.Text) || string.IsNullOrEmpty(cb_Area.GetItemText(cb_Area.SelectedItem)))
            {
                MessageBox.Show("Complete all fields");
            }
            else
            {
                // MessageBox.Show("Registered Completely");
                con.Open();
                string cnic = tb_cnic.Text;
                int area_id = Convert.ToInt32(cb_Area.GetItemText(cb_Area.SelectedValue));
                string name = tb_name.Text;
                string dob = tb_dob.Text;
                string bg = cb_bg.GetItemText(cb_bg.SelectedItem);
                Double contact = Convert.ToInt64(tb_contact.Text);
                string address = cb_Area.GetItemText(cb_Area.SelectedItem);
                string password = tb_pass.Text;

                string sql = "Insert into Individual_user (CNIC, Area_idArea, final_Name, Date_of_birth, Blood_group, Contact_number, final_Address, Blood_given, Blood_taken, Registered)";
                sql += "values('"+cnic+"', "+area_id+", '"+name+"', '"+dob+"', '"+bg+"', "+contact+", '"+address+"', 0, 0, 1)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                string sql2 = "Insert into FPassword (pass_word, Individual_user_Individual_Id)";
                sql2 += "values('jam', (select Individual_Id from Individual_user where final_Name = 'Azeem'))";
                SqlCommand cmd2 = new SqlCommand(sql, con);
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();

                MessageBox.Show("Successfully Registered");
                con.Close();


            }
        }

        private void tb_dob_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
