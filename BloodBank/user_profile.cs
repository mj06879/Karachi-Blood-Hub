using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBank
{
    public partial class user_profile : Form
    {

        public string std_user_id{ get; set; }
        public string std_user_cnic { get; set; }
        public string std_Indv_name { get; set; }
        public string std_user_age { get; set; }
        public string std_Amnt_taken { get; set; }
        public string std_Amnt_donated { get; set; }
        public string std_Blood_group { get; set; }
        public string std_contact { get; set; }
        public string std_addrss { get; set; }

        public user_profile()
        {
            InitializeComponent();
            tb_addrss.Enabled = false;
            tb_age.Enabled = false;
            tb_bg.Enabled = false;
            tb_cnic.Enabled = false;
            tb_contact.Enabled = false;
            tb_given.Enabled = false;
            tb_name.Enabled = false;
            tb_taken.Enabled = false;
            button3.Enabled = false;
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void user_profile_Load(object sender, EventArgs e)
        {
            lbl_user_id.Text = std_user_id;
            tb_cnic.Text = std_user_cnic;
            tb_name.Text = std_Indv_name;
            tb_age.Text = std_user_age;
            tb_given.Text = std_Amnt_donated;
            tb_taken.Text = std_Amnt_taken;
            tb_bg.Text = std_Blood_group;
            tb_addrss.Text = std_addrss;
            tb_contact.Text = std_contact;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
