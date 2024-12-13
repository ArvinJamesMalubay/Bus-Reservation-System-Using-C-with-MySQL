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
    public partial class Add_Role : Form
    {
        public Add_Role()
        {
            InitializeComponent();
            display();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to cancel?", "Cancel User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == (DialogResult.Yes))
                {
                   
                    this.Close();
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        void display()
        {
            CRUD_OPERATION_FOR_USERS.displayusersandsearh("Select Role From account_role", dataGridView1);
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_save.Text == "Save")
                {
                    
                    string querry = "Insert Into account_role (Role)Value ('" + tbx_positionName.Text + "')";
                    CRUD_OPERATION_FOR_USERS.Add_Update_Delete_Users_Info(querry);
                    display();
                }
                else if(btn_save.Text == "Update")
                {
                    
                    string querry = "UPDATE account_role SET Role = '" + tbx_positionName.Text + "' ";
                    CRUD_OPERATION_FOR_USERS.Add_Update_Delete_Users_Info(querry);
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                tbx_positionName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                btn_save.Text = "Update";
            }
            if(e.ColumnIndex == 1)
            {

            }
        }
    }
}
