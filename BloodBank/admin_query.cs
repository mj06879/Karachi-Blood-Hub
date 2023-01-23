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
    public partial class admin_query : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");
        public admin_query()
        {
            InitializeComponent();
            button1.Enabled = false;
            dataGridView1.ReadOnly = true;
        }
          
        private void admin_query_Load(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select Individual_Id, CNIC, final_Name as [Name], Blood_group, Contact_number, Blood_taken, Blood_given";
            sql += " from Individual_user where Registered = 1";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            dataGridView1.DataSource = dt;
            //dataGridView1.BackgroundColor = Color.White;
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                tb_cnic.Text = row.Cells["CNIC"].Value.ToString();
                tb_name.Text = row.Cells["Name"].Value.ToString();
                tb_btaken.Text = row.Cells["Blood_taken"].Value.ToString();
                tb_bgiven.Text = row.Cells["Blood_given"].Value.ToString();
                tb_bg.Text = row.Cells["Blood_group"].Value.ToString();
                tb_contact.Text = row.Cells["Contact_number"].Value.ToString();
                lb_user_id.Text = row.Cells["Individual_Id"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_bg.Text) || string.IsNullOrEmpty(tb_bgiven.Text) || string.IsNullOrEmpty(tb_btaken.Text) || string.IsNullOrEmpty(tb_cnic.Text) || string.IsNullOrEmpty(tb_contact.Text) || string.IsNullOrEmpty(tb_name.Text))
            {
                MessageBox.Show("Please Select a User");
            }
            else
            {
                con.Open();
                int id = Convert.ToInt32(lb_user_id.Text);
                string cnic = tb_cnic.Text;
                string name = tb_name.Text;
                float btaken = Convert.ToSingle(Convert.ToInt32(tb_btaken.Text));
                int bgiven = Convert.ToInt32(tb_bgiven.Text);
                string bg = tb_bg.Text;
                Double contact = Convert.ToInt64(tb_contact.Text);

                string sql = "Update Individual_user ";
                sql += "set CNIC = '" + cnic + "', final_Name = '" + name + "', Blood_group = '" + bg + "', Contact_number = " + contact + ", Blood_taken = " + btaken + ", Blood_given= " + bgiven + "";
                sql += "where Individual_Id = " + id + "";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                MessageBox.Show("Successfully Updated");
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            admin_profile ap = new admin_profile();
            ap.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lb_user_id.Text))
            {
                MessageBox.Show("Please Select a User");
            }
            else
            {
                con.Open();
                int id = Convert.ToInt32(lb_user_id.Text);
                string sql = "Update Individual_user set Registered = 0 where Individual_Id = "+id+"";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                MessageBox.Show("Successfully Removed");
                con.Close();

            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
