using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandTours_Project
{
    class DB_Class
    {
    }
    class DB_USERINFO
    {
        public int user_id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int Account_ID { get; set; }
        public int Role_ID { get; set; }
    }
    class DB_Account_Role
    {
        public string Acccount_Role { get; set; }
    }
    class DB_Accounts
    {
        public int account_id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public int role_id { get; set; }
    }
    class DB_Reservation
    {
        public int reserve_id { get; set; }
        public string Date_Reservation { get; set; }
        public string Departure_Date { get; set; }
        public string Departure_Time { get; set; }
        public string Destination { get; set; }
        public int fare_metrixID { get; set; }
        public int van_id { get; set; }


    }
    class DB_Passenger
    {
        public int passengerID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public double Fare_Price { get; set; }
        public double Cash { get; set; }
        public double Cash_Change { get; set; }
        public int reservation_ID { get; set; }
      
    }
    class DB_Fare_Metrix
    {
        public string Destination_From { get; set; }
        public string Destination_To { get; set; }
        public double Fare_Price { get; set; }
        public DateTime Date_Updated { get; set; }

      
    }
    class DB_Drivers
    {
        public int driverID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string status { get; set; }
        public int vanID { get; set; }
       
    }
    class DB_Van_List
    {
        public int vanID { get; set; }
        public string schedule { get; set; }
        public string Plate_Number { get; set; }
        public int capacity { get; set; }

      
    }
}
