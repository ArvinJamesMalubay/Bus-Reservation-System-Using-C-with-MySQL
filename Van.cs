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
    public partial class Van : Form
    {
        public Van()
        {
            InitializeComponent();
            AutoincrementID();
        }
        public int id = 0;
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
        public void AutoincrementID()
        {

            MySqlConnection con = CRUD_OPERATION_FOR_USERS.GetConnection();
            MySqlCommand cmd = new MySqlCommand("select count(Van_ID) from van_list ", con);
            int j = Convert.ToInt32(cmd.ExecuteScalar());
            j++;
            lbl_vanID.Text = id + j.ToString();
            lbl_vanID.Visible = false;

            con.Close();
        }
        void CLEAR()
        {
            tbx_capacity.Text = tbx_plateNumber.Text = dtp_schedule.Text = "";
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if(tbx_plateNumber.Text != string.Empty || tbx_capacity.Text != string.Empty || dtp_schedule.Text != string.Empty)
                {                  
                    string querry = "Insert into van_list (Van_ID, Plate_Number, Van_Schedule, Van_Capacity) VALUES ('"+ lbl_vanID.Text + "', '"+ tbx_plateNumber.Text + "', '" + dtp_schedule.Text + "'," +
                        "'"+ tbx_capacity.Text + "')";
                    CRUD_OPERATION_FOR_USERS.Add_Update_Delete_Users_Info(querry);
                    CLEAR();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
