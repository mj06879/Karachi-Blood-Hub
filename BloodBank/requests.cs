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
    public partial class requests : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");

        public string std_user_id { get; set; }
        public string std_blood_group { get; set; }
        public requests()
        {
            InitializeComponent();
            button1.Enabled = false;
            cb_req.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_old_req.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            donations d = new donations();
            d.std_user_id = std_user_id;
            d.Show();
            this.Hide();
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

        private void button6_Click(object sender, EventArgs e)
        {
            new_requests_user nru = new new_requests_user();
            nru.std_user_id = std_user_id;
            nru.Show();
            this.Hide();
        }

        private void requests_Load(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select idRequest, Patient_CNIC, Blood_Group, Amount_needed, DateTime_2 as [Date], B.final_name as BB_Name from Request R join Blood_Bank B on R.Blood_Bank_idBlood_Bank = B.idBlood_Bank Where Requestedby_User = @user and Filfulled = 0";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@user", Int64.Parse(std_user_id));
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cb_req.DataSource = dt;
            cb_req.ValueMember = "idRequest";
            dt.Columns.Add(
                "Combined",
                typeof(string),
                "'CNIC: ' + Patient_CNIC + ' BG: ' + Blood_Group + ' Ounce: ' + Amount_needed + ' Date: ' + Date + ' BB Name: ' +  BB_Name");

            // cb_req.DisplayMember = "Patient_CNIC".ToString() + ", " + "Blood_Group".ToString() + ", " + "Amount_needed".ToString();
            cb_req.DisplayMember = "Combined";
            con.Close();

            con.Open();
            string sql2 = "select idRequest, Patient_CNIC, Blood_Group, Amount_needed, DateTime_2 as [Date], B.final_name as BB_Name from Request R join Blood_Bank B on R.Blood_Bank_idBlood_Bank = B.idBlood_Bank Where Requestedby_User = @user and Filfulled = 1";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.Parameters.AddWithValue("@user", Int64.Parse(std_user_id));
            SqlDataAdapter da2 = new SqlDataAdapter();
            da2.SelectCommand = cmd2;
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            cb_old_req.DataSource = dt2;
            dt2.Columns.Add(
                "Combined",
                typeof(string),
                "'CNIC: ' + Patient_CNIC + ' BG: ' + Blood_Group + ' Ounce: ' + Amount_needed + ' Date: ' + Date + ' BB Name: ' +  BB_Name");

            // cb_req.DisplayMember = "Patient_CNIC".ToString() + ", " + "Blood_Group".ToString() + ", " + "Amount_needed".ToString();
            cb_old_req.DisplayMember = "Combined";
            cb_old_req.ValueMember = "idRequest";
            con.Close();


            //dataGridView1.BackgroundColor = Color.White;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cb_req.GetItemText(cb_req.SelectedIndex)))
            {
                MessageBox.Show("Select a request");
            }
            else
            {
                int id = Convert.ToInt32(cb_req.GetItemText(cb_req.SelectedValue));
                con.Open();
                string sql = "Delete Request where idRequest = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cb_req.SelectedItem = null;
                //this.requests_Load(sender, e);
                con.Close();
            }

        }
    }
}
