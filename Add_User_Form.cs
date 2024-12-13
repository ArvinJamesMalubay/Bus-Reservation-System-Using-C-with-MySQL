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
    public partial class Add_User_Form : Form
    {
       
        public Add_User_Form()
        {
            InitializeComponent();
            fillcomboboxforrole();
           
        }          
       

        public void AutoincrementID()
        {

            MySqlConnection con = CRUD_OPERATION_FOR_USERS.GetConnection();
            MySqlCommand cmd = new MySqlCommand("select count(Account_ID) from accounts ", con);
            int j = System.Convert.ToInt32(cmd.ExecuteScalar());
            j++;
            lbl_account_id.Text = j.ToString();
            //lbl_account_id.Visible = true;
        
            con.Close();
        }
        
        private void Add_User_Form_Load(object sender, EventArgs e)
        {
            if (btn_save.Text == "Save")
            {
                AutoincrementID();
            }
                
           
        }
        private void fillcomboboxforrole()
        {
            try
            {
                MySqlConnection con = CRUD_OPERATION_FOR_USERS.GetConnection();
                string querry = "SELECT Role FROM account_role";
               
                MySqlCommand cmd = new MySqlCommand(querry, con);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string role = reader.GetString("Role");
                    cmb_role.Items.Add(role);
                }
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    
        public void clear()
        {
            tbx_firstName.Text = tbx_lastName.Text = tbx_password.Text = cmb_role.Text = tbx_username.Text = tbx_address.Text = tbx_age.Text = "";
        }
        private void btn_cancel_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to cancel?", "Cancel User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == (DialogResult.Yes))
                {
                    clear();
                    
                    this.Close();
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
                    if (tbx_firstName.Text.Trim().Length < 3 && tbx_lastName.Text.Trim().Length < 3)
                    {
                        MessageBox.Show("Please Input all Fields!");

                        lbl_warnfirstname.Visible = true;
                        lbl_warnLastname.Visible = true;
                        lbl_warnAge.Visible = true;
                        lbl_warnAddress.Visible = true;
                        lbl_warnRole.Visible = true;
                        lbl_warnUsername.Visible = true;
                        lbl_warnPassword.Visible = true;
                        lbl_warnStatus.Visible = true;
                        return;
                    }
                    else if (tbx_firstName.Text.Trim().Length < 3)
                    {
                        MessageBox.Show("First Name is Empty.");
                        lbl_warnfirstname.Visible = true;
                        return;
                    }
                    else if (tbx_lastName.Text.Trim().Length < 3)
                    {
                        MessageBox.Show("Last Name is Empty.");
                        lbl_warnLastname.Visible = true;
                        return;
                    }
                    else if (tbx_age.Text.Trim().Length < 0 || int.Parse(tbx_age.Text) < 15 || int.Parse(tbx_age.Text) > 80)
                    {
                        MessageBox.Show("Your Age must be greater than 15 and less than 80.");
                        lbl_warnAge.Visible = true;
                        return;
                    }
                    else if (tbx_address.Text.Trim().Length < 3)
                    {
                        MessageBox.Show("Address is Empty.");
                        lbl_warnAddress.Visible = true;
                        return;
                    }
                    else if (tbx_username.Text.Trim().Length < 3)
                    {
                        MessageBox.Show("Username is Empty.");
                        lbl_warnUsername.Visible = true;
                        return;
                    }
                    else if (tbx_password.Text.Trim().Length < 3)
                    {
                        MessageBox.Show("Address is Empty.");
                        lbl_warnPassword.Visible = true;
                        return;
                    }
                    else if (cmb_status.Text.Trim().Length < 3)
                    {
                        MessageBox.Show("Address is Empty.");
                        lbl_warnStatus.Visible = true;
                        return;
                    }
                if (btn_save.Text == "Save")
                {
                    string querry = "Insert Into user_info (User_ID, First_Name, Last_Name, Age, Address, Account_ID, Role_ID )" +
                        "Values ('" + int.Parse(lbl_account_id.Text) + "','" + tbx_firstName.Text + "', '" + tbx_lastName.Text + "', '" + int.Parse(tbx_age.Text) + "', '" + tbx_address.Text + "', '" + int.Parse(lbl_account_id.Text) + "', '" + int.Parse(lbl_role_id.Text) + "'); " +
                        "Insert into accounts (Account_ID, Username, Password, Status, Role_ID) " +
                        "Values( '" + int.Parse(lbl_account_id.Text) + "','" + tbx_username.Text + "', '" + tbx_password.Text + "', '" + cmb_status.Text + "', '" + int.Parse(lbl_role_id.Text) + "')";          
                    CRUD_OPERATION_FOR_USERS.Add_Update_Delete_Users_Info(querry);
                    clear();
                    AutoincrementID();
                }
                else if (btn_save.Text == "Update")
                {
                    if (MessageBox.Show("Are you sure you want to update!", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        string querry = "UPDATE user_info as table1, accounts as table2 SET table1.First_Name = '" + tbx_firstName.Text + "', table1.Last_Name = '" + tbx_lastName.Text + "'," +
                            "table1.Age = '" + int.Parse(tbx_age.Text) + "',table1.Address =  '" + tbx_address.Text + "', table1.Role_ID = '" + int.Parse(lbl_role_id.Text) + "', table2.Username = '" + tbx_username.Text + "',  table2.Password = '" + tbx_password.Text + "'," +
                            " table2.Status = '" + cmb_status.Text + "',   table2.Role_ID = '" + int.Parse(lbl_role_id.Text) + "' WHERE table1.User_ID = '"+lbl_account_id.Text+"' and table2.Account_ID = '"+lbl_account_id.Text+"'" ;
                        CRUD_OPERATION_FOR_USERS.Add_Update_Delete_Users_Info(querry);                     
                        clear();
                        this.Close();
                    }
                }
            }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }      
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Exit", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to cancel?", "Cancel User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == (DialogResult.Yes))
                {
                    clear();

                    this.Close();
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void cmb_role_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            MySqlConnection con = CRUD_OPERATION_FOR_USERS.GetConnection();
            MySqlCommand cmd = new MySqlCommand("Select Role_ID FROM account_role WHERE Role = '" + cmb_role.Text + "'", con);
            int role = System.Convert.ToInt32(cmd.ExecuteScalar());
            lbl_role_id.Text = role.ToString();
            lbl_role_id.Visible = false;

            con.Close();
        }

        private void tbx_age_KeyPress(object sender, KeyPressEventArgs e)
        {
            char num = e.KeyChar;
            if (!Char.IsDigit(num) && num != 8 && num != 46)
            {         
                    e.Handled = true;

            }
        }
    }
}
