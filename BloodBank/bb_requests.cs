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
    public partial class bb_requests : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");
        public string std_bb_id { get; set; }
        

        public bb_requests()
        {
            InitializeComponent();
            button1.Enabled = false;
            cb_req.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_old_req.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string query_3 = "select B.idBlood_Bank as BB_id, B.final_Name as BB_Name ,A.final_Name as area_name ,B.Contact_no as bb_contact  from Blood_Bank B join Area A on B.Area_idArea = A.idArea where B.idBlood_Bank = @userid";
            SqlCommand cmd3 = new SqlCommand(query_3, con);
            cmd3.Parameters.AddWithValue("@userid", Int64.Parse(std_bb_id));
            SqlDataReader dr = cmd3.ExecuteReader();

            if (dr.Read())
            {
                bb_profile bb = new bb_profile();
                bb.std_user_id = dr["BB_id"].ToString();
                bb.std_user_name = dr["BB_Name"].ToString();
                bb.std_area_name = dr["area_name"].ToString();
                bb.std_contact = dr["bb_contact"].ToString();

                bb.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong id or password");
            }
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void bb_requests_Load(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select idRequest,Patient_CNIC, Blood_Group, Amount_needed, DateTime_2 as Date From Request Where Blood_Bank_idBlood_Bank = @bb_id and Filfulled = 0";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@bb_id", Int64.Parse(std_bb_id));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cb_req.DataSource = dt;
            cb_req.ValueMember = "idRequest";
            dt.Columns.Add(
                "Combined",
                typeof(string),
                " 'CNIC: ' + Patient_CNIC+ ', BG: ' + Blood_Group + ', Ounce: ' + Amount_needed + ', Date: ' + Date");
            cb_req.DisplayMember = "Combined";
            con.Close();

            con.Open();
            string sql2 = "select idRequest, Patient_CNIC, Blood_Group, Amount_needed, DateTime_2 as [Date] From Request Where Blood_Bank_idBlood_Bank = @bb_id and Filfulled = 1";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.Parameters.AddWithValue("@bb_id", Int64.Parse(std_bb_id));
            SqlDataAdapter da2 = new SqlDataAdapter();
            da2.SelectCommand = cmd2;
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            cb_old_req.ValueMember = "idRequest";
            dt2.Columns.Add(
                "Combined",
                typeof(string),
                " 'CNIC: ' + Patient_CNIC+ ', BG: ' + Blood_Group + ', Ounce: ' + Amount_needed + ', Date: ' + Date");

            // cb_req.DisplayMember = "Patient_CNIC".ToString() + ", " + "Blood_Group".ToString() + ", " + "Amount_needed".ToString();
            cb_old_req.DisplayMember = "Combined";
            cb_old_req.DataSource = dt2;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cb_req.GetItemText(cb_req.SelectedItem)))
            {
                MessageBox.Show("Select the Request item");

            }
            else
            {
                //MessageBox.Show(cb_req.GetItemText(cb_req.SelectedValue));
                int id = Convert.ToInt32(cb_req.GetItemText(cb_req.SelectedValue));
                con.Open();
                string sql = "Update Request set Filfulled = 1 where idRequest = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id );
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                con.Open();
                string sql2 = "Update Individual_user set Blood_taken = Blood_taken + (select Amount_needed from Request where idRequest = @id)";
                sql2 += " where CNIC in (select Patient_CNIC from Request where idRequest = @id)";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.Parameters.AddWithValue("@id", id);
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();

                
                MessageBox.Show("Succesfully Approved");
                //cb_old_req.Items.Add(cb_req.GetItemText(cb_req.SelectedItem));
                cb_req.SelectedItem = null;
                con.Close();
            }
        }
    }
}
