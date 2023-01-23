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
    public partial class hospital_profile : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");
        public string std_hosp_id { get; set; }
        public string std_area_name { get; set; }
        public string std_name { get; set; }
        public string  std_contact { get; set; }

        public hospital_profile()
        {
            InitializeComponent();
            button3.Enabled = false;
            tb_area.Enabled = false;
            tb_contact.Enabled = false;
            tb_Name.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            requests_hosp rh = new requests_hosp();
            rh.std_hosp_id = std_hosp_id;
            rh.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void hospital_profile_Load(object sender, EventArgs e)
        {
            tb_Name.Text = std_name;
            tb_contact.Text = std_contact;
            tb_area.Text = std_area_name;
            lbl_id.Text = std_hosp_id;
        }
    }
}
