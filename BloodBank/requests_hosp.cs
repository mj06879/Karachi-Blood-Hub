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
    public partial class requests_hosp : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");
        public string std_hosp_id { get; set; }
        public requests_hosp()
        {
            InitializeComponent();
            cb_req.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_old_req.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
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

        private void requests_hosp_Load(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select idRequest, Patient_CNIC, Blood_Group, Amount_needed, DateTime_2 as [Date], B.final_name as BB_Name from Request R join Blood_Bank B on R.Blood_Bank_idBlood_Bank = B.idBlood_Bank Where Requestedby_Hospital = @user and Filfulled = 0";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@user", Int64.Parse(std_hosp_id));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cb_req.DataSource = dt;
            dt.Columns.Add(
                "Combined",
                typeof(string),
                " 'CNIC: ' + Patient_CNIC+ ' BG: ' + Blood_Group + ' Ounce: ' + Amount_needed + ' Date: ' + Date + ' BB Name: ' +  BB_Name");
            cb_req.DisplayMember = "Combined";
            cb_req.ValueMember = "idRequest";
            con.Close();

            con.Open();
            string sql2 = "select idRequest, Patient_CNIC, Blood_Group, Amount_needed, DateTime_2 as [Date], B.final_name as BB_Name from Request R join Blood_Bank B on R.Blood_Bank_idBlood_Bank = B.idBlood_Bank Where Requestedby_Hospital = @user and Filfulled = 1";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.Parameters.AddWithValue("@user", Int64.Parse(std_hosp_id));
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
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new_requests rh = new new_requests();
            rh.std_hosp_id = std_hosp_id;
            rh.Show();
            this.Hide();
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
