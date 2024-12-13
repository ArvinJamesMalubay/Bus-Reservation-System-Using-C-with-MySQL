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
    public partial class Admin_Interface : Form
    {
        
        public Admin_Interface()
        {
            InitializeComponent();
           
            btn_users.Click += btn_users_Click;
            btn_drivers.Click += btn_drivers_Click; 
            btn_fareMetrix.Click += btn_fareMetrix_Click;
        }
       
        private void btn_users_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.SelectedTab = tabPage1;
            display();
        }

        private void btn_drivers_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.SelectedTab = tabPage2;
            CRUD_OPERATION_FOR_USERS.displayusersandsearh("Select drivers.First_Name, drivers.Last_Name, van_list.Plate_Number, van_list.Van_Schedule , drivers.Status From drivers Inner join van_list ON drivers.Van_ID = van_list.Van_ID",dataGridView2);
        }

        private void btn_fareMetrix_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.SelectedTab = tabPage3;
        }
        public void display()
        {
            CRUD_OPERATION_FOR_USERS.displayusersandsearh("Select user_info.User_ID,user_info.First_Name , user_info.Last_Name, user_info.Age," +
                " user_info.Address, account_role.Role, accounts.Status, accounts.Username, accounts.Password " +
                "FROM ((user_info Inner join accounts On user_info.Account_ID = accounts.Account_ID)" +
                " Inner join account_role On user_info.Role_ID = account_role.Role_ID) ORDER BY user_ID ASC", dataGridView1);
        }


        private void btn_logout_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_logout.Text == "Logout")
                {
                    if (MessageBox.Show("Do you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == (DialogResult.Yes))
                    {
                        Form1 form = new Form1();
                        this.Hide();
                        form.Show();
                        
                    }
                   
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Add_User_Form addform = new Add_User_Form();
                addform.ShowDialog();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Admin_Interface_Load(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
        }
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                //Edit
                Add_User_Form form = new Add_User_Form();

                form.lbl_account_id.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.tbx_firstName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.tbx_lastName.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.tbx_age.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                form.tbx_address.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                form.cmb_role.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                form.cmb_status.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                form.tbx_username.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                form.tbx_password.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                form.btn_save.Text = "Update";
                form.ShowDialog();
                display();
                return;
            }

            if (e.ColumnIndex == 1)
            {
                //Delete
                if (MessageBox.Show("Are you sure you want to delete this field! ", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {                   
                    string querry = " Delete table1, table2  FROM user_info as table1 " +
                "Inner join accounts as table2  ON table1.Account_ID = table2.Account_ID " +
                "where table1.user_ID = '" + int.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()) + "'"; 
                    CRUD_OPERATION_FOR_USERS.Add_Update_Delete_Users_Info(querry);
                    display();
                   
                }
                return;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (tbx_search.Text != string.Empty)
            {

                CRUD_OPERATION_FOR_USERS.displayusersandsearh("Select user_info.User_ID, user_info.First_Name, user_info.Last_Name, user_info.Age, user_info.Address, account_role.Role, accounts.Status, accounts.Username, accounts.Password " +
                "FROM ((user_info Inner join accounts On user_info.Account_ID = accounts.Account_ID)" +
                " Inner join account_role On user_info.Role_ID = account_role.Role_ID) WHERE First_Name Like '" +tbx_search.Text + "' '" + "%" + "'  Or Last_Name Like '" + tbx_search.Text + "' '" + "%" + "'ORDER BY user_ID ASC", dataGridView1);
            }
            if (tbx_search.Text == string.Empty)
            {
                CRUD_OPERATION_FOR_USERS.displayusersandsearh("Select user_info.User_ID, user_info.First_Name, user_info.Last_Name, user_info.Age, user_info.Address, account_role.Role, accounts.Status, accounts.Username, accounts.Password " +
                "FROM ((user_info Inner join accounts On user_info.Account_ID = accounts.Account_ID)" +
                " Inner join account_role On user_info.Role_ID = account_role.Role_ID) ORDER BY user_ID ASC", dataGridView1);
            }
        }

        private void roleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Add_User_Form addform = new Add_User_Form();
                addform.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void roleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            try
            {
                Add_Role roleform = new Add_Role();
                roleform.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void addDriverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Drivers form = new Drivers();
            form.ShowDialog();
        }

        private void vanSListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Van form = new Van();
            form.ShowDialog();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                //Edit
                ReservationForm1 form = new ReservationForm1();

                form.lbl_reserve_ID.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.tbx_firstName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.tbx_lastName.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.dtp_Date.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                form.dtp_Time.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                form.cmb_Destination.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

                form.btn_save.Text = "Update";
                form.ShowDialog();
                display();
                return;
            }

            if (e.ColumnIndex == 1)
            {
                //Delete
                //if (MessageBox.Show("Are you sure you want to delete this field! ", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                //{
                //    string querry = " Delete table1, table2  FROM user_info as table1 " +
                //"Inner join accounts as table2  ON table1.Account_ID = table2.Account_ID " +
                //"where table1.user_ID = '" + int.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()) + "'";
                //    CRUD_OPERATION_FOR_USERS.Add_Update_Delete_Users_Info(querry);
                //    display();

                //}
                //return;
            }
        }
    }
}
