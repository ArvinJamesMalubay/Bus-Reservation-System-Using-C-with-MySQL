using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandTours_Project
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void tbx_username_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tbx_username_Click(object sender, EventArgs e)
        {
            tbx_username.SelectAll();
            tbx_username.ForeColor = Color.FromArgb(10, 36, 99);
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbx_username.Text != string.Empty)
                {
                    if (btn_Login.Text == "Search")
                    {
                        MySqlConnection con = CRUD_OPERATION_FOR_USERS.GetConnection();
                        MySqlCommand cmd = new MySqlCommand("Select Password From accounts where Username = '" + tbx_username.Text + "'", con);
                        MySqlDataReader sdr = cmd.ExecuteReader();
                        sdr.Read();
                        if (true)
                        {
                            MessageBox.Show("You can now change your old password " + "[" + sdr["Password"].ToString() + "]");
                            tbx_password.Text = sdr["Password"].ToString();
                            tbx_password.ForeColor = Color.FromArgb(10, 36, 99);
                            btn_save.Visible = true;
                            btn_Login.Text = "Cancel";

                            sdr.Close();
                        }
                        con.Close();
                    }
                    if (btn_Login.Text == "Cancel")
                    {
                        tbx_confirmpassword.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("No Account Available with this Username ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if(tbx_username.Text == string.Empty)
                {
                    lbl_usernameWarning.Visible = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Are you sure to update old password?", "Update", MessageBoxButtons.YesNo , MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MySqlConnection con = CRUD_OPERATION_FOR_USERS.GetConnection();
                    MySqlCommand cmd = new MySqlCommand("Update accounts set Password = '" + tbx_confirmpassword.Text + "' where Username  = '" + tbx_username.Text + "' ", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Password change successfully!!");
                    Form1 form = new Form1();
                    form.Show();
                    this.Hide();
                }
               
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
           
        }

        private void tbx_confirmpassword_Click(object sender, EventArgs e)
        {
            tbx_confirmpassword.SelectAll();
            tbx_confirmpassword.ForeColor = Color.FromArgb(10, 36, 99);
        }

        private void tbx_confirmpassword_TextChanged(object sender, EventArgs e)
        {
            tbx_password.ForeColor = Color.FromArgb(10, 36, 99);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lbl_passwordWarning.Visible = false;
            lbl_usernameWarning.Visible = false;
            lbl_warningConfirmPassword.Visible = false;
        }
    }
}
