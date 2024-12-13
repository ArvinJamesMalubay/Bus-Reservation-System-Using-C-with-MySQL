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
    public partial class Staff_Interface : Form
    {
        public Staff_Interface()
        {
            InitializeComponent();
           btn_passengers.Click += btn_passengers_Click;
           
        }
       
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //string days, months, year;
            //days = dateTimePicker1.Value.Day.ToString();
            //months = dateTimePicker1.Value.Month.ToString();
            //year = dateTimePicker1.Value.Year.ToString();
            MySqlConnection con = CRUD_OPERATION_FOR_RESERVATION.GetConnection();
            CRUD_OPERATION_FOR_RESERVATION.displaysandsearh("Select passenger.Passenger_ID, passenger.First_Name, passenger.Last_Name" +
                ", reservation.Departure_date, reservation.Departure_time, reservation.destination From passenger  Inner join reservation  ON passenger.Reservation_ID = reservation.Reservation_ID  Where reservation.Departure_date = '" + dateTimePicker1.Text+"' ", dataGridView1);
            //MySqlCommand cmd = new MySqlCommand(querry, con);

            try
            {
                //cmd.ExecuteNonQuery();
                //display();
               
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }



            }

        private void btn_logout_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (btn_logout.Text == "Logout")
                {
                    if (MessageBox.Show("Do you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == (DialogResult.Yes))
                    {
                        Form1 form = new Form1();
                        form.Show();
                        this.Hide();
                    }
                    return;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        void display()
        {
            CRUD_OPERATION_FOR_RESERVATION.displaysandsearh("Select passenger.Passenger_ID, passenger.First_Name, passenger.Last_Name" +
                ", reservation.Departure_date, reservation.Departure_time, reservation.destination, passenger.Reserved_Seat From passenger  Inner join reservation  ON passenger.Reservation_ID = reservation.Reservation_ID ",dataGridView1);
        }
        private void btn_passengers_Click(object sender, EventArgs e)
        {       
            tabControl1.SelectedTab = tabPage1;
        }

        private void btn_bookings_Click(object sender, EventArgs e)
        {
            ReservationForm1 form = new ReservationForm1();
            form.ShowDialog();
        }

        private void btn_addReservation_Click(object sender, EventArgs e)
        {
            ReservationForm1 form1 = new ReservationForm1();
            form1.ShowDialog();
            
        }

        private void Staff_Interface_Load(object sender, EventArgs e)
        {
            display();
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            display();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void tbx_search_TextChanged(object sender, EventArgs e)
        {
            if (tbx_search.Text != string.Empty)
            {

                CRUD_OPERATION_FOR_RESERVATION.displaysandsearh("Select passenger.Passenger_ID, passenger.First_Name, passenger.Last_Name" +
                 ", reservation.Departure_date, reservation.Departure_time, reservation.destination, passenger.Reserved_Seat From passenger  Inner join reservation  ON passenger.Reservation_ID = reservation.Reservation_ID WHERE First_Name Like '" + tbx_search.Text + "' '" + "%" + "' ORDER BY Passenger_ID ASC ", dataGridView1);
            }
            if (tbx_search.Text == string.Empty)
            {
                CRUD_OPERATION_FOR_RESERVATION.displaysandsearh("Select passenger.Passenger_ID, passenger.First_Name, passenger.Last_Name" +
               ", reservation.Departure_date, reservation.Departure_time, reservation.destination, passenger.Reserved_Seat From passenger  Inner join reservation  ON passenger.Reservation_ID = reservation.Reservation_ID ORDER BY Passenger_ID ASC ", dataGridView1);
            }
        }
    }
}
