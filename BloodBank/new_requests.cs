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
    public partial class new_requests : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");
        public string std_hosp_id { get; set; }
        public new_requests()
        {
            InitializeComponent();
            tb_bb_area.Enabled = false;
            cb_bb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_bg.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_curnt_area.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string query_2 = "select H.idHospital as[Hospital_ID], A.final_Name as [Area], H.Hospital_Name as [Hospital_Name], H.Contact_no as [Contact] from Hospital H join Area A on H.Area_idARea = A.idArea where H.idHospital = @userid";
            SqlCommand cmd2 = new SqlCommand(query_2, con);
            cmd2.Parameters.AddWithValue("@userid", Int64.Parse(std_hosp_id));
            SqlDataReader dr = cmd2.ExecuteReader();

            if (dr.Read())
            {
                hospital_profile u2 = new hospital_profile();
                u2.std_hosp_id = dr["Hospital_ID"].ToString();
                u2.std_area_name = dr["Area"].ToString();
                u2.std_contact = dr["Contact"].ToString();
                u2.std_name = dr["Hospital_Name"].ToString();
                u2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong id or password");
            }
            con.Close();
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            requests_hosp rh = new requests_hosp();
            rh.std_hosp_id = std_hosp_id;
            rh.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void new_requests_Load(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select A.idArea as area_id, A.final_Name as Area_Name from Hospital H join Area A on H.Area_idArea = A.idArea where H.idHospital = @user";
            SqlCommand cmd2 = new SqlCommand(sql, con);
            cmd2.Parameters.AddWithValue("@user", Int64.Parse(std_hosp_id));
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd2;
            DataTable table1 = new DataTable();
            da.Fill(table1);
            cb_curnt_area.DataSource = table1;
            cb_curnt_area.DisplayMember = "Area_Name";
            cb_curnt_area.ValueMember = "area_id";
            con.Close();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_cnic.Text) || (tb_cnic.Text.Length > 13 || tb_cnic.Text.Length < 13) || string.IsNullOrWhiteSpace(tb_amnt.Text) || string.IsNullOrWhiteSpace(tb_bb_area.Text) || string.IsNullOrWhiteSpace(cb_bb.GetItemText(cb_bb.SelectedItem)) || string.IsNullOrWhiteSpace(cb_bg.GetItemText(cb_bg.SelectedItem)) || string.IsNullOrWhiteSpace(cb_curnt_area.GetItemText(cb_curnt_area.SelectedItem)))
            {
                MessageBox.Show("Complete All the required Fiels!");
            }
            else
            {
                //con.Open();
                string pt_cnic = tb_cnic.Text;
                string bg = cb_bg.GetItemText(cb_bg.SelectedItem);
                //string query;
                //query = "select CNIC from Individual_user where CNIC = " + pt_cnic + "";
                //SqlCommand cmd1 = new SqlCommand(query, con);
                //SqlDataReader dr = cmd1.ExecuteReader();

                //if (dr.Read())
                //{
                //    MessageBox.Show("Patient already exists");
                //}
                //else
                //{
                //    string sql2 = "Insert into Individual_user (CNIC, Blood_group) values ('" + pt_cnic + "', '" + bg + "')";
                //    SqlCommand cmd4 = new SqlCommand(sql2, con);
                //    cmd4.ExecuteNonQuery();
                //    cmd4.Dispose();
                //}
                //con.Close();

                //MessageBox.Show("Added Patient");

                con.Open();
                int rq_by_hosp = Convert.ToInt32(std_hosp_id);
                
                int bb_id = Convert.ToInt32(cb_bb.GetItemText(cb_bb.SelectedValue));
                string cur_area = cb_curnt_area.GetItemText(cb_bb.SelectedItem);
                int amnt_need = Convert.ToInt32(tb_amnt.Text);

                string sql = "Insert into Request (Patient_CNIC, Blood_Group, Requestedby_User, Requestedby_Hospital, Blood_Bank_idBlood_Bank, DateTime_2, User_Current_Area, Amount_needed, Filfulled)";
                sql += "values( " + pt_cnic + ", '" + bg + "', NULL, "+ rq_by_hosp +", " + bb_id + ", convert(varchar, getdate(), 110), '" + cur_area + "', " + amnt_need + ", 0)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                MessageBox.Show("Request Submitted");
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void tb_amnt_Leave(object sender, EventArgs e)
        {
            con.Open();
            string sql2 = "select B.final_Name as [BB_Name], B.idBlood_Bank as [BB_ID],  BA.Amount as [Amount] from Blood_Bank B join Blood_bank_has_Blood_Amount BA on B.idBlood_Bank = BA.Blood_Bank_idBlood_Bank where BA.Blood_type = @b_type and BA.Amount >= @b_amount order by BA.Amount";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.Parameters.AddWithValue("@b_type", cb_bg.GetItemText(cb_bg.SelectedItem));
            cmd2.Parameters.AddWithValue("@b_amount", tb_amnt.Text);
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
    }
}
