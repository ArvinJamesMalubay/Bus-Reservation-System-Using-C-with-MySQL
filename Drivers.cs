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
    public partial class Drivers : Form
    {
        public Drivers()
        {
            InitializeComponent();
            fillcomboboxforrole();
            AutoincrementID();
        }
        public int ID = 0;

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
        private void fillcomboboxforrole()
        {
            try
            {
                MySqlConnection con = CRUD_OPERATION_FOR_USERS.GetConnection();
                string querry = "SELECT Plate_Number FROM van_list";
               
                MySqlCommand cmd = new MySqlCommand(querry, con);

                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string role = reader.GetString("Plate_Number");
                    cmb_plateNumber.Items.Add(role);
                }
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public void AutoincrementID()
        {

            MySqlConnection con = CRUD_OPERATION_FOR_USERS.GetConnection();
            MySqlCommand cmd = new MySqlCommand("select count(Drivers_ID) from drivers ", con);
            int j = Convert.ToInt32(cmd.ExecuteScalar());
            j++;
            lbl_driversID.Text = ID + j.ToString();
            lbl_driversID.Visible = true;

            con.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {              
                string querry = "Insert into drivers (Drivers_ID, First_Name, Last_Name, Age, Address, Status, Van_ID) " +
                    "Values('"+ lbl_driversID.Text + "', '"+ tbx_firstName.Text + "', '"+ tbx_lastName.Text + "', '"+ tbx_age.Text + "', " +
                    "'" + tbx_address.Text + "', '"+ cmb_status.Text + "', '"+ lbl_vanID.Text + "')";
                
                CRUD_OPERATION_FOR_USERS.Add_Update_Delete_Users_Info(querry);
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void cmb_plateNumber_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            MySqlConnection con = CRUD_OPERATION_FOR_USERS.GetConnection();
            MySqlCommand cmd = new MySqlCommand("Select Van_ID FROM van_list WHERE Plate_Number = '" + cmb_plateNumber.Text + "'", con);
            int vanID = System.Convert.ToInt32(cmd.ExecuteScalar());
            lbl_vanID.Text = vanID.ToString();
            lbl_vanID.Visible = true;
            con.Close();
        }
    }
}
