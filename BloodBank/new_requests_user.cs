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
    public partial class new_requests_user : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");
        public string std_user_id;

        public new_requests_user()
        {
            InitializeComponent();
            tb_bb_area.Enabled = false;
            cb_bg.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_curnt_area.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_bb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "Select Individual_Id, CNIC,final_Name,Date_of_birth,Blood_group,Blood_taken,Blood_given, Contact_number,final_Address as [Address] from Individual_user where Individual_Id = @user and Registered = 1";
            SqlCommand cmd1 = new SqlCommand(query, con);

            cmd1.Parameters.AddWithValue("@user", Int64.Parse(std_user_id));
            SqlDataReader dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                // MessageBox.Show(dr["Blood_group"].ToString());
                user_profile u1 = new user_profile();
                u1.std_user_id = std_user_id; // dr["Individual_Id"].ToString();
                u1.std_user_cnic = dr["CNIC"].ToString();
                u1.std_Indv_name = dr["final_Name"].ToString();
                u1.std_user_age = dr["Date_of_birth"].ToString();
                u1.std_Blood_group = dr["Blood_group"].ToString();
                u1.std_Amnt_taken = dr["Blood_taken"].ToString();
                u1.std_Amnt_donated = dr["Blood_given"].ToString();
                u1.std_addrss = dr["Address"].ToString();
                u1.std_contact = dr["Contact_number"].ToString();
                u1.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Wrong id or password");
            }
            con.Close();
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

        private void new_requests_user_Load(object sender, EventArgs e)
        {
            con.Open();
            string sql= "select idArea , final_Name as [Area Name] from Area";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da2 = new SqlDataAdapter();
            da2.SelectCommand = cmd;
            DataTable table1 = new DataTable();
            da2.Fill(table1);

            DataRow itemrow = table1.NewRow();
            itemrow[1] = "Select Current Area";
            table1.Rows.InsertAt(itemrow, 0);

            cb_curnt_area.DataSource = table1;
            cb_curnt_area.DisplayMember = "Area Name";
            cb_curnt_area.ValueMember = "idArea";
            con.Close();
        }

        private void tb_amnt_rq_Leave(object sender, EventArgs e)
        {
            con.Open();
            string sql2 = "select B.final_Name as [BB_Name], B.idBlood_Bank as [BB_ID],  BA.Amount as [Amount] from Blood_Bank B join Blood_bank_has_Blood_Amount BA on B.idBlood_Bank = BA.Blood_Bank_idBlood_Bank where BA.Blood_type = @b_type and BA.Amount >= @b_amount order by BA.Amount";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.Parameters.AddWithValue("@b_type", cb_bg.GetItemText(cb_bg.SelectedItem));
            cmd2.Parameters.AddWithValue("@b_amount", tb_amnt_rq.Text);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd2;
            DataTable table1 = new DataTable();
            da.Fill(table1);
            cb_bb.DataSource = table1;
            cb_bb.DisplayMember = "BB_Name";
            cb_bb.ValueMember = "BB_ID";
            con.Close();
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
            
            con.Close();
        }

        private void tb_amnt_rq_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        { 
            if (string.IsNullOrWhiteSpace(tb_cnic.Text) || (tb_cnic.Text.Length > 13 || tb_cnic.Text.Length <13) || string.IsNullOrWhiteSpace(tb_amnt_rq.Text) || string.IsNullOrWhiteSpace(tb_bb_area.Text) || string.IsNullOrWhiteSpace(cb_bb.GetItemText(cb_bb.SelectedItem)) || string.IsNullOrWhiteSpace(cb_bg.GetItemText(cb_bg.SelectedItem)) || string.IsNullOrWhiteSpace(cb_curnt_area.GetItemText(cb_curnt_area.SelectedItem)))
            {
                MessageBox.Show("Complete All the required Fiels!");
            }
            else
            {
                con.Open();
                string pt_cnic = tb_cnic.Text;
                int rq_by_user = Convert.ToInt32(std_user_id);
                string bg = cb_bg.GetItemText(cb_bg.SelectedItem);
                int bb_id = Convert.ToInt32(cb_bb.GetItemText(cb_bb.SelectedValue));
                string cur_area = cb_curnt_area.GetItemText(cb_bb.SelectedItem);
                int amnt_need = Convert.ToInt32(tb_amnt_rq.Text);

                string sql = "Insert into Request (Patient_CNIC, Blood_Group, Requestedby_Hospital, Requestedby_User, Blood_Bank_idBlood_Bank, DateTime_2, User_Current_Area, Amount_needed, Filfulled)";
                sql += "values( "+ pt_cnic+", '"+bg+"', NULL, "+ rq_by_user + ", "+ bb_id + ", convert(varchar, getdate(), 110), '"+ cur_area + "', "+ amnt_need + ", 0)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                MessageBox.Show("Request Submitted");
                con.Close();
            }
        }

        private void cb_bb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
