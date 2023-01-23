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
    public partial class SignUp3 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4TJIRA" + "\\" + "SPARTA1;Initial Catalog=Blood_hub;Integrated Security=True");
        public SignUp3()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            SignUp s1 = new SignUp();
            s1.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            SignUp2 s2 = new SignUp2();
            s2.Show();
            this.Hide();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SignUp s1 = new SignUp();
            s1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SignUp2 s2 = new SignUp2();
            s2.Show();
            this.Hide();
        }

        private void closebtn_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }

        private void closebtn_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SignUp3_Load(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select idArea,final_Name as [Area Name]  from Area ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable table1 = new DataTable();
            da.Fill(table1);
            cb_area.DataSource = table1;
            cb_area.DisplayMember = "Area Name";
            cb_area.ValueMember = "idArea";
            con.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_name.Text) || string.IsNullOrEmpty(tb_contact.Text) || string.IsNullOrEmpty(cb_area.GetItemText(cb_area.SelectedValue)) || string.IsNullOrEmpty(tb_pass.Text) || string.IsNullOrEmpty(tb_Ap.Text) || string.IsNullOrEmpty(tb_An.Text) || string.IsNullOrEmpty(tb_Bp.Text) || string.IsNullOrEmpty(tb_Bn.Text) || string.IsNullOrEmpty(tb_ABp.Text) || string.IsNullOrEmpty(tb_ABn.Text) || string.IsNullOrEmpty(tb_Op.Text) || string.IsNullOrEmpty(tb_On.Text))
            {
                string name = tb_name.Text;
                string contact = tb_contact.Text;
                string pass = tb_pass.Text;
                int area_id = Convert.ToInt32(cb_area.GetItemText(cb_area.SelectedValue));
                string Ap = tb_Ap.Text;
                string An = tb_An.Text;
                string Bp = tb_Bp.Text;
                string Bn = tb_Bn.Text;
                string ABp = tb_ABp.Text;
                string ABn = tb_ABn.Text;
                string Op = tb_Op.Text;
                string On = tb_On.Text;

                con.Open();
                string sql = "Insert into Blood_Bank (Area_idArea, fpassword, final_Name, Contact_no, Availability_status)";
                sql += " Values(" + area_id + ",'" + pass + "','" + name + "'," + contact + ", 1)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                con.Open();
                string sql2 = "select B.idBlood_Bank as BB_id from Blood_Bank where final_Name = " + tb_name.Text + "";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                con.Close();

                if (dr2.Read())
                {   

                    string temp = dr2["BB_id"].ToString();
                    int bb_id = Convert.ToInt32(temp);
                    con.Open();
                    string sql3 = "Insert into Blood_bank_has_Blood_Amount (Blood_Bank_idBlood_Bank, Blood_type, Amount)";
                    sql3 = "values(" + bb_id + ", 'A+' , " + Ap + " ) ";
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.ExecuteNonQuery();
                    cmd3.Dispose();
                    con.Close();

                    con.Open();
                    string sql4 = "Insert into Blood_bank_has_Blood_Amount (Blood_Bank_idBlood_Bank, Blood_type, Amount)";
                    sql4 = "values(" + bb_id + ", 'A-' , " + An + " ) ";
                    SqlCommand cmd4 = new SqlCommand(sql4, con);
                    cmd4.ExecuteNonQuery();
                    cmd4.Dispose();
                    con.Close();

                    con.Open();
                    string sql5 = "Insert into Blood_bank_has_Blood_Amount (Blood_Bank_idBlood_Bank, Blood_type, Amount)";
                    sql5 = "values(" + bb_id + ", 'B+' , " + Bp + " ) ";
                    SqlCommand cmd5 = new SqlCommand(sql5, con);
                    cmd5.ExecuteNonQuery();
                    cmd5.Dispose();
                    con.Close();

                    con.Open();
                    string sql6 = "Insert into Blood_bank_has_Blood_Amount (Blood_Bank_idBlood_Bank, Blood_type, Amount)";
                    sql4 = "values(" + bb_id + ", 'B-' , " + Bn + " ) ";
                    SqlCommand cmd6 = new SqlCommand(sql6, con);
                    cmd6.ExecuteNonQuery();
                    cmd6.Dispose();
                    con.Close();

                    con.Open();
                    string sql7 = "Insert into Blood_bank_has_Blood_Amount (Blood_Bank_idBlood_Bank, Blood_type, Amount)";
                    sql7 = "values(" + bb_id + ", 'AB+' , " + ABp + " ) ";
                    SqlCommand cmd7 = new SqlCommand(sql7, con);
                    cmd7.ExecuteNonQuery();
                    cmd7.Dispose();
                    con.Close();

                    con.Open();
                    string sql8 = "Insert into Blood_bank_has_Blood_Amount (Blood_Bank_idBlood_Bank, Blood_type, Amount)";
                    sql8 = "values(" + bb_id + ", 'AB-' , " + ABn + " ) ";
                    SqlCommand cmd8 = new SqlCommand(sql8, con);
                    cmd8.ExecuteNonQuery();
                    cmd8.Dispose();
                    con.Close();

                    con.Open();
                    string sql9 = "Insert into Blood_bank_has_Blood_Amount (Blood_Bank_idBlood_Bank, Blood_type, Amount)";
                    sql9 = "values(" + bb_id + ", 'O+' , " + Op + " ) ";
                    SqlCommand cmd9 = new SqlCommand(sql9, con);
                    cmd9.ExecuteNonQuery();
                    cmd9.Dispose();
                    con.Close();

                    con.Open();
                    string sql10 = "Insert into Blood_bank_has_Blood_Amount (Blood_Bank_idBlood_Bank, Blood_type, Amount)";
                    sql10 = "values(" + bb_id + ", 'O-' , " + On + " ) ";
                    SqlCommand cmd10 = new SqlCommand(sql10, con);
                    cmd10.ExecuteNonQuery();
                    cmd10.Dispose();
                    con.Close();
                }
                
            }
        }
    }
}