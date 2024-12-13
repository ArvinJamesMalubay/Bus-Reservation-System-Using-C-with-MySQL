using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandTours_Project
{
    class CRUD_OPERATION_FOR_USERS
    { 
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=my_grand_tours_project";
            MySqlConnection connectionstring = new MySqlConnection(sql);
            try
            {
                connectionstring.Open();
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("MySQL Connection! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return connectionstring;
        }
        public static void Add_Update_Delete_Users_Info(string querry)
        {
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(querry, con);
            
            try
            {
                cmd.ExecuteNonQuery();
               
                MessageBox.Show("Successfull.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Fatal Error!. \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }
        
        public static void displayusersandsearh(string querry, DataGridView dgv)
        {
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(querry, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dgv.DataSource = dt;
            con.Close();
        }
    }
}
