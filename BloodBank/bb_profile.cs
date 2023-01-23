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

    public partial class bb_profile : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");
        // public bb_profile std_profile;
        public string std_user_id { get; set; }
        public string std_user_name { get; set; }
        public string std_contact { get; set; }
        public string std_area_name { get; set; }

        public bb_profile()
        {
            InitializeComponent();
            button3.Enabled = false;
            tb_amount.Enabled = false;
            tb_area.Enabled = false;
            tb_contact.Enabled = false;
            tb_name.Enabled = false;
            cb_bg.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bb_requests bb = new bb_requests();
            bb.std_bb_id = std_user_id;
            bb.Show();
            this.Hide();
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
            
        }

        private void bb_profile_Load(object sender, EventArgs e)
        {
            tb_name.Text = std_user_name;
            tb_contact.Text = std_contact;
            tb_area.Text = std_area_name;
            lbl_bb.Text = std_user_id.ToString();
        }

        private void cb_bg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bg = cb_bg.GetItemText(cb_bg.SelectedItem);
            con.Open();
            // string query_3 = "select BA.Amount as b_Amount from Blood_Bank B join Blood_bank_has_Blood_Amount BA on B.idBlood_Bank = BA.Blood_Bank_idBlood_Bank where B.final_Name = @user_name and BA.Blood_type = @bt";
            string query_3 = "select BA.Amount as b_Amount from Blood_bank_has_Blood_Amount BA where BA.Blood_Bank_idBlood_Bank = @userid and Ba.Blood_type = @bt";
            SqlCommand cmd3 = new SqlCommand(query_3, con);
            cmd3.Parameters.AddWithValue("@bt", bg);
            cmd3.Parameters.AddWithValue("@userid", Int64.Parse(std_user_id));
            SqlDataReader dr = cmd3.ExecuteReader();

            if (dr.Read())
            {
                tb_amount.Text = dr["b_Amount"].ToString();
                //bb_profile u3 = new bb_profile();
                //u3.std_user_id = dr["BB_id"].ToString();
                //u3.std_user_name = dr["BB_Name"].ToString();
                //u3.std_area_name = dr["area_name"].ToString();
                //u3.std_contact = dr["bb_contact"].ToString();

                //u3.Show();

            }
            else
            {
                MessageBox.Show("Wrong id or password");
            }
            con.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
