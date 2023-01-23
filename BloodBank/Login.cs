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
    public partial class login : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");
        public login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("Connection Open");
            
            if (this.user_selec_cb.Text == "Individual User")
            {
                con.Open();
                string query = "Select Individual_Id, CNIC,final_Name,Date_of_birth,Blood_group,Blood_taken,Blood_given,Contact_number,final_Address as [Address]";
                query += "from Individual_user where CNIC = @user and Registered = 1 and fpassword = @password";
                SqlCommand cmd1 = new SqlCommand(query, con);
                cmd1.Parameters.AddWithValue("@user", tb_user_name.Text);
                cmd1.Parameters.AddWithValue("@password", tb_pass.Text);
                SqlDataReader dr = cmd1.ExecuteReader();

                if (dr.Read())
                {
                    // MessageBox.Show(dr["Blood_group"].ToString());
                    user_profile u1 = new user_profile();
                    u1.std_user_id = dr["Individual_Id"].ToString();
                    u1.std_Indv_name = dr["final_Name"].ToString();
                    u1.std_user_cnic = dr["CNIC"].ToString();
                    u1.std_user_age = dr["Date_of_birth"].ToString();
                    u1.std_Blood_group = dr["Blood_group"].ToString();
                    u1.std_Amnt_taken= dr["Blood_taken"].ToString();
                    u1.std_Amnt_donated = dr["Blood_given"].ToString();
                    u1.std_addrss = dr["Address"].ToString();
                    u1.std_contact = dr["Contact_number"].ToString();
                    u1.Show();
                    this.Hide();

                    this.user_selec_cb.Items.Clear();
                }
                else
                {
                    MessageBox.Show("Wrong id or password");
                }
                con.Close();
                
            }
            else if (this.user_selec_cb.Text == "Hospital")
            {
                con.Open();
                string query_2 = "select H.idHospital as[Hospital_ID], A.final_Name as [Area], H.Hospital_Name as [Hospital_Name], H.Contact_no as [Contact]";
                query_2 += " from Hospital H join Area A on H.Area_idARea = A.idArea where H.Hospital_Name = @user and fpassword = @password";
                SqlCommand cmd2 = new SqlCommand(query_2, con);
                cmd2.Parameters.AddWithValue("@user", tb_user_name.Text);
                cmd2.Parameters.AddWithValue("@password", tb_pass.Text);
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
                    this.user_selec_cb.Items.Clear();
                }
                else
                {
                    MessageBox.Show("Wrong id or password");
                }
                con.Close();

            }
            else if (this.user_selec_cb.Text == "Blood Bank")
            {
                con.Open();
                string query_3 = "select B.idBlood_Bank as BB_id,fpassword, B.final_Name as BB_Name ,A.final_Name as area_name ,B.Contact_no as bb_contact";
                query_3 += " from Blood_Bank B join Area A on B.Area_idArea = A.idArea  where B.final_Name = @user and fpassword = @password";

                SqlCommand cmd3 = new SqlCommand(query_3, con);
                cmd3.Parameters.AddWithValue("@user", tb_user_name.Text);
                cmd3.Parameters.AddWithValue("@password", tb_pass.Text);
                SqlDataReader dr = cmd3.ExecuteReader();

                if (dr.Read())
                {
                    bb_profile u3 = new bb_profile();
                    u3.std_user_id = dr["BB_id"].ToString();
                    u3.std_user_name = dr["BB_Name"].ToString();
                    u3.std_area_name = dr["area_name"].ToString();
                    u3.std_contact = dr["bb_contact"].ToString();

                    u3.Show();
                    this.Hide();
                    this.user_selec_cb.Items.Clear();
                }
                else
                {
                    MessageBox.Show("Wrong id or password");
                }
                con.Close();
                
            }
            
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignUp s1 = new SignUp();
            s1.Show();
            this.Hide();
        }

        private void label3_Click_2(object sender, EventArgs e)
        {
            login_hosp s1 = new login_hosp();
            s1.Show();
            this.Hide();

        }

        private void label7_Click(object sender, EventArgs e)
        {
            login_bb s1 = new login_bb();
            s1.Show();
            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            login_hosp s1 = new login_hosp();
            s1.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            login_admin a = new login_admin();
            a.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            user_selec_cb.Items.Add("Individual User");
            user_selec_cb.Items.Add("Hospital");
            user_selec_cb.Items.Add("Blood Bank");
            label11.Text = "Select Your User Type";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label11.Text = user_selec_cb.Text;
        }

        private void tb_user_name_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_user_name.Text) == true)
            {
                tb_user_name.Focus();
                errorProvider1.SetError(this.tb_user_name, "Please fill this field");
                // this.user_selec_cb.Items.Clear();
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void tb_user_name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
