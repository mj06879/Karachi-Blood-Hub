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
    public partial class login_hosp : Form
    {
        public login_hosp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hospital_profile h = new hospital_profile();
            h.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignUp s1 = new SignUp();
            s1.Show();
            this.Hide();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            login s1 = new login();
            s1.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            login_bb s1 = new login_bb();
            s1.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            login_admin a = new login_admin();
            a.Show();
            this.Hide();
        }
    }
}
