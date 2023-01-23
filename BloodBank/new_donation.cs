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
    public partial class new_donation : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");
        public string std_user_id { get; set; }
        public string std_blood_group { get; set; }
        public new_donation()
        {
            InitializeComponent();
            tb_bg.Enabled = false;
            tb_donater.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            user_profile u = new user_profile();
            u.std_user_id = std_user_id;
            u.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            requests r = new requests();
            r.std_user_id = std_user_id;
            r.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            donations d = new donations();
            d.std_user_id = std_user_id;
            d.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void new_donation_Load(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select Individual_Id, Blood_group from Individual_user Where Individual_Id = @user_id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@user_id", Int64.Parse(std_user_id));
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                tb_donater.Text = std_user_id;
                tb_bg.Text = dr["Blood_group"].ToString();
            }
            else
            {
                MessageBox.Show("Wrong id or password");
            }
            con.Close();

            con.Open();
            string sql2 = "select B.final_Name as [BB_Name], B.idBlood_Bank as [BB_ID],  BA.Amount as [Amount] from Blood_Bank B join Blood_bank_has_Blood_Amount BA on B.idBlood_Bank = BA.Blood_Bank_idBlood_Bank where BA.Blood_type = @b_type order by BA.Amount";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.Parameters.AddWithValue("@b_type", tb_bg.Text);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd2;
            DataTable table1 = new DataTable();
            da.Fill(table1);
            cb_bb.DataSource = table1;
            cb_bb.DisplayMember = "BB_Name";
            cb_bb.ValueMember = "BB_ID";
            con.Close();

        }

        private void tb_amnt_Leave(object sender, EventArgs e)
        {
            
        }

        private void tb_amnt_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cb_bb_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cb_bb_Leave(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select A.final_Name as [Area] from Blood_Bank B join Area A on B.Area_idArea = A.idArea where B.idBlood_Bank = @bb_id";
            SqlCommand cmd2 = new SqlCommand(sql, con);
            cmd2.Parameters.AddWithValue("@bb_id", cb_bb.GetItemText(cb_bb.SelectedValue));
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                tb_bb_area.Text = dr["Area"].ToString();
            }
            else
            {
                MessageBox.Show("Wrong id or password");
            }
            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_donater.Text) || string.IsNullOrWhiteSpace(tb_bg.Text) || string.IsNullOrWhiteSpace(cb_bb.GetItemText(cb_bb.SelectedItem)) || string.IsNullOrWhiteSpace(tb_amnt.Text) || string.IsNullOrWhiteSpace(tb_bb_area.Text))
            {
                MessageBox.Show("Complete All the required Fiels!");
            }
            else
            {
                con.Open();

                int amount_donated = Convert.ToInt32(tb_amnt.Text);
                int user_id = Convert.ToInt32(std_user_id);
                int bb_id = Convert.ToInt32(cb_bb.GetItemText(cb_bb.SelectedValue));


                string sql = "Insert into Donations (Amount_Donated, Individual_user_Individual_Id, Blood_Bank_idBlood_Bank, DateTime_2)";
                sql += "values("+ amount_donated + ", "+ user_id + ", "+ bb_id + ", convert(varchar, getdate(), 101))";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                string sql2 = "Update Individual_user set Blood_given += " + amount_donated + " where Individual_Id = " + user_id + "";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();

                string sql3 = "Update Blood_bank_has_Blood_Amount set Amount += "+amount_donated+" where Blood_Bank_idBlood_Bank = "+bb_id+" and Blood_type = '"+std_blood_group+"'";
                SqlCommand cmd3 = new SqlCommand(sql3, con);
                cmd3.ExecuteNonQuery();
                cmd3.Dispose();

                MessageBox.Show("Successfully Donated");

                con.Close();
            }
        }
    }
}
