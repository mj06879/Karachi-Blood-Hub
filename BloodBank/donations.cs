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
    public partial class donations : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");
        public string std_user_id { get; set; }
        public string std_blood_group { get; set; }
        public donations()
        {
            InitializeComponent();
            button2.Enabled = false;
            cb_don.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            requests r = new requests();
            r.std_user_id = std_user_id;
            r.std_blood_group = std_blood_group;
            r.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "Select Individual_Id, CNIC,final_Name,Date_of_birth,Blood_group,Blood_taken,Blood_given,Contact_number,final_Address as [Address] from Individual_user where Individual_Id = @user and Registered = 1";
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            new_donation nd = new new_donation();
            nd.std_user_id = std_user_id;
            nd.std_blood_group = std_blood_group;
            nd.Show();
            this.Hide();
        }

        private void donations_Load(object sender, EventArgs e)
        {
            con.Open();
            // I.final_Name as [Name], 
            string query = "select idBlood_Bank,BB.final_Name as [Blood Bank], D.Amount_Donated as Amount, D.DateTime_2 as [Date] from Donations D join Individual_user I on D.Individual_user_Individual_Id = I.Individual_Id join Blood_Bank BB on BB.idBlood_Bank = D.Blood_Bank_idBlood_Bank where I.Individual_Id = @user_id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_id", Int64.Parse(std_user_id));
            SqlDataAdapter da2 = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da2.Fill(dt);
            cb_don.DataSource = dt;
            dt.Columns.Add(
                "Combined",
                typeof(string),
                "'Blood Bank: ' + [Blood Bank] + ' Ounce: ' + Amount + ' Date: ' + Date");

            // cb_req.DisplayMember = "Patient_CNIC".ToString() + ", " + "Blood_Group".ToString() + ", " + "Amount_needed".ToString();
            cb_don.DisplayMember = "Combined";
            cb_don.ValueMember = "idBlood_Bank";
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
