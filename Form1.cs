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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       void clear()
        {
            tbx_username.Text = tbx_password.Text = "";
        }
        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {

                if (tbx_username.Text != string.Empty || tbx_password.Text != string.Empty)
                {
                    MySqlConnection con = CRUD_OPERATION_FOR_USERS.GetConnection();
                    MySqlDataAdapter sda = new MySqlDataAdapter("Select account_role.Role  from account_role Inner join accounts Where accounts.Username = '" + tbx_username.Text + "' and accounts.Password = '" + tbx_password.Text + "' ", con);

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {

                        switch (dt.Rows[1]["Role"] as string)
                        {
                            case "Administrator":
                                {
                                    Admin_Interface admin = new Admin_Interface();
                                    admin.Show();
                                    this.Hide();
                                    break;
                                }
                            case "Staff Clerk":
                                {
                                    Staff_Interface staff = new Staff_Interface();
                                    staff.Show();
                                    this.Hide();
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No Account Available with this Username and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lbl_usernameWarning.Visible = true;
                        lbl_passwordWarning.Visible = true;
                        clear();
                    }
                   
                   con.Close();

                }
                else if (tbx_username.Text == "" || tbx_password.Text == "")
                {
                    lbl_usernameWarning.Visible = true;
                    lbl_passwordWarning.Visible = true;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tbx_password.PasswordChar = show_password.Checked ? '\0' : '*';
        }

        private void tbx_password_TextChanged(object sender, EventArgs e)
        {
            tbx_password.PasswordChar = '*';
        }

        private void lbl_clickHere_MouseHover(object sender, EventArgs e)
        {
            lbl_clickHere.ForeColor = Color.Orange;
        }

        private void lbl_clickHere_MouseLeave(object sender, EventArgs e)
        {
            lbl_clickHere.ForeColor = Color.FromArgb(10, 36, 99);
        }

       
        private void tbx_username_Click(object sender, EventArgs e)
        {
            tbx_username.SelectAll();
            tbx_username.ForeColor = Color.FromArgb(10, 36, 99);
        }

        private void tbx_password_Click(object sender, EventArgs e)
        {
            tbx_password.SelectAll();
            tbx_password.ForeColor = Color.FromArgb(10, 36, 99);

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Do you want to Exit?", "Exit" , MessageBoxButtons.OKCancel , MessageBoxIcon.Warning ) == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void lbl_clickHere_MouseHover_1(object sender, EventArgs e)
        {
            lbl_clickHere.ForeColor = Color.Orange;
        }

        private void lbl_clickHere_MouseLeave_1(object sender, EventArgs e)
        {
            lbl_clickHere.ForeColor = Color.Blue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbl_usernameWarning.Visible = false;
            lbl_passwordWarning.Visible = false;
        }

        private void lbl_clickHere_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            this.Hide();
        }
    }
}
