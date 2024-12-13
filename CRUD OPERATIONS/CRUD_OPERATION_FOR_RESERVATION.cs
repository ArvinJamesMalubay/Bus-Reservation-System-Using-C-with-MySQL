using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace GrandTours_Project
{
    class CRUD_OPERATION_FOR_RESERVATION
    {
        public static  MySqlConnection GetConnection()
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
       
        public static void add_update(string querry, string updatecapacity)
        { 
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(querry, con);
            MySqlCommand cmd3 = new MySqlCommand(updatecapacity, con);

            try
            {

                cmd.ExecuteNonQuery();              
                cmd3.ExecuteNonQuery();

                MessageBox.Show("Added Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Passenger not Inserted. \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }
       
        public static void displaysandsearh(string querry, DataGridView dgv)
        {
            string sql = querry;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dgv.DataSource = dt;
            con.Close();
        }
    }
}
    

