using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Donation.Models
{
    public class DbConnection
    {
        //const string ConnectionString = @"Server=db204.my-hosting-panel.com ; Database=networld-bd.com_onlineuniversity;User Id=networld-bd.com_onlineuniversity;Password=Net@143# ";

        string connectinString = @"Server=DESKTOP-IB0IJ0J\SQLEXPRESS;Database=FoodDonation;User Id=sa;Password=123456";

        SqlConnection Connection;
        public SqlConnection GetConnection()
        {
            Connection = new SqlConnection(connectinString);
            return Connection;
        }
        public void OpenConnection()
        {
            Connection.Open();
        }
        public void CloseConnection()
        {
            Connection.Close();
        }
    }
}
