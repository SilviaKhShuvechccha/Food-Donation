using Food_Donation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Donation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string constr = @"Server=LAPTOP-JHUBDUV5; Database=FoodDonation; Integrated Security= True";
            SqlConnection conn = new SqlConnection(constr);
            string query = "SELECT * FROM Donate";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<Donate> donationList = new List<Donate>();
            while (reader.Read())
            {
                Donate donate = new Donate
                {
                    DonationId = int.Parse(reader["DonationId"].ToString()),
                    DonationTitle = reader["DonationTitle"].ToString(),
                    FoodDescription = reader["FoodDescription"].ToString(),
                    FoodWeight = double.Parse(reader["FoodWeight"].ToString()),
                    FileUrl = reader["FileUrl"].ToString(),
                    Location = reader["Location"].ToString(),
                    ContactNo = reader["ContactNo"].ToString(),
                    DonatedBy = int.Parse(reader["DonatedBy"].ToString())
                };
                donationList.Add(donate);
            }
            reader.Close();
            conn.Close();
            return View(donationList);
        }
    }
}
