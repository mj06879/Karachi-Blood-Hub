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
    public partial class login_admin : Form
    {
        public login_admin()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            login a = new login();
            a.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            login_bb a = new login_bb();
            a.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            login_hosp a = new login_hosp();
            a.Show();
            this.Hide();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.tb_user_name.Text == "admin123" && this.tb_pass.Text  == "123" )
            {
                admin_profile a = new admin_profile();
                a.Show();
                this.Hide();
            }
            else if (string.IsNullOrEmpty(tb_user_name.Text) || string.IsNullOrEmpty(tb_pass.Text))
            {
                MessageBox.Show("Give username and password");
            }
            else
            {
                MessageBox.Show("Wrong id or password");
            }
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
