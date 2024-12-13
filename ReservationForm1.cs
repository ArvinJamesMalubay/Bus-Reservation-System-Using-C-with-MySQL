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
    public partial class ReservationForm1 : Form
    {
        public ReservationForm1()
        {
            InitializeComponent();
            fillcombobox();
           
        }
        public int available, passNumber, total;
        public double price, sum;
        public double cash, change;

        public int reserveid = 0;
      
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Do you want to cancel?", "Cancel Reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== (DialogResult.Yes))
                {
                    this.Close();
                }
                
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public void AutoincrementID()
        {

            MySqlConnection con = CRUD_OPERATION_FOR_USERS.GetConnection();
            MySqlCommand cmd = new MySqlCommand("select count(Reservation_ID) from reservation ", con);
            int j = Convert.ToInt32(cmd.ExecuteScalar());
            j++;
            lbl_reserve_ID.Text = reserveid + j.ToString();


            lbl_reserve_ID.Visible = false;


            con.Close();
        }
        void display()
        {
            CRUD_OPERATION_FOR_RESERVATION.displaysandsearh("SELECT Van_ID, Plate_Number, Van_Capacity FROM van_list Where Van_Schedule = '"+dtp_Time.Text+"'", dataGridView1);
        }
        private void btn_next_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbx_firstName.Text.Trim().Length < 3 && tbx_lastName.Text.Trim().Length < 3 && cmb_Destination.Text.Trim().Length < 3)

                {
                    MessageBox.Show("Please Input all Fields!");
                    lbl_warndeaparturetime.Visible = true;
                    lbl_warndeparturedate.Visible = true;
                    lbl_warndestination.Visible = true;
                    lbl_warnfirstname.Visible = true;
                    lbl_warnLastname.Visible = true;
                    lbl_warningPassengerNumber.Visible = true;
                    return;
                }
                else if (tbx_firstName.Text.Trim().Length < 3)
                {

                    lbl_warnfirstname.Visible = true;
                    return;
                }
                else if (tbx_lastName.Text.Trim().Length < 3)
                {
                    lbl_warnLastname.Visible = true;
                    return;
                }
                else if (cmb_Destination.Text.Trim().Length < 3)
                {
                    lbl_warndestination.Visible = true;
                    return;
                }
                else if (System.Convert.ToDateTime(dtp_Date.Value.ToShortDateString().Trim()) == null)
                {
                    lbl_warndeparturedate.Visible = true;
                    return;
                }
                else if (System.Convert.ToDateTime(dtp_Time.Value.ToShortTimeString().Trim()) == null)
                {
                    lbl_warndeaparturetime.Visible = true;
                    return;
                }
                else if(tbx_passNumber.Text.Trim().Length < 1)
                {
                    lbl_warningPassengerNumber.Visible = true;
                    return;
                }
                else if(tbx_cash.Text.Trim().Length < 1)
                {
                    lbl_warningcaseReceived.Visible = true;
                    return;
                }
                else if (btn_save.Text == "Save")
                {

                    string querry = "Insert into passenger (Passenger_ID,First_Name,Last_Name,Fare_Price, Cash, Change_returned, Reserved_Seat, Reservation_ID ) " +
                        "values ( '" + int.Parse(lbl_reserve_ID.Text) + "', '" + tbx_firstName.Text + "', '" + tbx_lastName.Text + "', '" + double.Parse(lbl_total.Text) + "'," +
                        "'" + double.Parse(tbx_cash.Text) + "', '" + double.Parse(tbx_change.Text) + "','" +tbx_passNumber.Text + "', '" + int.Parse(lbl_reserve_ID.Text) + "'); " +
                        "Insert into reservation (Reservation_ID, Reservation_date, Departure_date, Departure_time, Destination, Van_ID, Fare_Metrix_ID) " +
                        "Values ('"+int.Parse(lbl_reserve_ID.Text)+"', '"+ lbl_reserve_date.Text + "', '"+ dtp_Date.Text + "', '"+ dtp_Time.Text + "', '"+ cmb_Destination.Text + "'," +
                        "'"+ int.Parse(lbl_vanID.Text) + "' ,'"+ int.Parse(lbl_fareMetrixID.Text) + "')";
 
                    string updatecapacity = "Update van_list Set Van_Capacity = '"+ int.Parse(lbl_capacityNumber.Text)+ "' where Van_ID = '"+ int.Parse(lbl_vanID.Text)+"'" ;
                   
                    CRUD_OPERATION_FOR_RESERVATION.add_update(querry, updatecapacity);
                    clear();
                    display();
                }
                else if(btn_save.Text == "Update")
                {


                    string querry = "Update passenger as table1, reservation as table2 SET table1.First_Name = '"+ tbx_firstName.Text + "'" +
                        ",table1.Last_Name ='"+ tbx_lastName.Text + "', table1.Fare_Price =  '" + double.Parse(lbl_total.Text) + "' ,  table1.Cash = '" + double.Parse(tbx_cash.Text) + "'," +
                        " table1.Change_returned = '" + double.Parse(tbx_change.Text) + "',table1.Reserved_Seat = '"+tbx_passNumber.Text+"', table2.Reservation_date =  '" + lbl_reserve_date.Text + "', table2.Departure_date =  '" + dtp_Date.Text + "', " +
                        "table2.Departure_time = '" + dtp_Time.Text + "', table2.Destination = '" + cmb_Destination.Text + "', table2.Van_ID = '" + int.Parse(lbl_vanID.Text) + "' ," +
                        " table2.Fare_Metrix_ID = '" + int.Parse(lbl_fareMetrixID.Text) + "'  WHERE table1.Passenger_ID =  '" + int.Parse(lbl_reserve_ID.Text) + "' and table2.Reservation_ID = '" + int.Parse(lbl_reserve_ID.Text)+"'";

                    string updatecapacity = "Update van_list Set Van_Capacity = '" + int.Parse(lbl_capacityNumber.Text) + "' where Van_ID = '" + int.Parse(lbl_vanID.Text) + "'";

                    CRUD_OPERATION_FOR_RESERVATION.add_update(querry, updatecapacity);
                }
                
            }
            catch(MySqlException err)
            {
                MessageBox.Show(err.Message);
            }
           
        }
        void clear()
        {
            tbx_firstName.Text = tbx_lastName.Text = cmb_Destination.Text = dtp_Date.Text = dtp_Time.Text = tbx_cash.Text = tbx_change.Text = tbx_passNumber.Text = "";
        }
        private void fillcombobox()
        {
            try
            {                
                string querry = "SELECT CONCAT(Destination_From, ' -' , Destination_To) AS destination FROM fare_metrix";
                MySqlConnection con = CRUD_OPERATION_FOR_USERS.GetConnection();
                MySqlCommand cmd = new MySqlCommand(querry, con);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    string destination = reader.GetString("destination");
                    cmb_Destination.Items.Add(destination);
                }

                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        private void cmb_Destination_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection con = CRUD_OPERATION_FOR_RESERVATION.GetConnection();
            MySqlCommand cmd = new MySqlCommand("Select Fare_Price FROM fare_metrix WHERE CONCAT(Destination_From, ' -' , Destination_To) = '" + cmb_Destination.Text + "'", con);
            int fare = System.Convert.ToInt32(cmd.ExecuteScalar());
            MySqlCommand cmd1 = new MySqlCommand("Select Fare_Metrix_ID FROM fare_metrix where CONCAT(Destination_From, ' -' , Destination_To) = '" + cmb_Destination.Text + "'", con);
            int id = System.Convert.ToInt32(cmd1.ExecuteScalar());
            lbl_price.Text = fare.ToString();
            lbl_fareMetrixID.Text = id.ToString();
            con.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_reserve_date.Text = DateTime.Now.Date.ToShortDateString();
        }

        private void ReservationForm1_Load(object sender, EventArgs e)
        {
            AutoincrementID();
            timer1.Start();
            lbl_warndeaparturetime.Visible = false;
            lbl_warndeparturedate.Visible = false;
            lbl_warndestination.Visible = false;
            lbl_warnfirstname.Visible = false;
            lbl_warnLastname.Visible = false;
            lbl_warningPassengerNumber.Visible = false;
            lbl_warningcaseReceived.Visible = false;
        }

        private void dtp_Time_ValueChanged(object sender, EventArgs e)
        {
            display();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbl_vanID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            lbl_capacityNumber.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
           
        }    
        private void tbx_cash_TextChanged(object sender, EventArgs e)
        {
            getchange();
        }

        private void tbx_passNumber_TextChanged(object sender, EventArgs e)
        {
            getAvailable();
            getTotal();
        }
        void getAvailable()
        {
            if (tbx_passNumber.Text != string.Empty)
            {
                available = int.Parse(lbl_capacityNumber.Text.ToString());
                passNumber = int.Parse(tbx_passNumber.Text.ToString());
                if (available <= passNumber)
                {

                }
                else if (available > passNumber)
                {
                    total = available - passNumber;
                    lbl_capacityNumber.Text = total.ToString();
                }
            }
            else
            {

                MessageBox.Show("No more available");
                tbx_passNumber.Text = "";
            }
            return;
        }
        void getTotal()
        {
            if (tbx_passNumber.Text != string.Empty)
            {
                price = double.Parse(lbl_price.Text.ToString());
                passNumber = int.Parse(tbx_passNumber.Text.ToString());
                sum = passNumber * price;
                lbl_total.Text = sum.ToString();
            }
            else if(tbx_passNumber.Text == string.Empty)
            {
                lbl_total.Text = "0";
            }
        }
        void getchange()
        {
            if (tbx_cash.Text != string.Empty)
            {
                sum = int.Parse(lbl_total.Text.ToString());
                cash = double.Parse(tbx_cash.Text.ToString());

                change = cash - sum;
                tbx_change.Text = change.ToString();
            }
            else if(tbx_cash.Text == string.Empty)
            {
                tbx_change.Text = "0";
            }
        }
    }
}
