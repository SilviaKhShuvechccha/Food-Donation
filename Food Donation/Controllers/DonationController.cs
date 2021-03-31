using Food_Donation.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Donation.Controllers
{
    public class DonationController : Controller
    {
        IWebHostEnvironment Environment;

        public DonationController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Donate donate, IFormFile Photo)
        {
            donate.FileUrl = SaveFile(Photo);
            string constr = @"Server=LAPTOP-JHUBDUV5; Database=FoodDonation; Integrated Security= True";
            int rowAffacted;
            SqlConnection conn = new SqlConnection(constr);
            string query = "INSERT INTO Donate(DonationTitle, FoodDescription, FoodWeight, FileUrl, Location, ContactNo, DonatedBy) VALUES('" + donate.DonationTitle + "','" + donate.FoodDescription + "', '" + donate.FoodWeight + "', '" + donate.FileUrl + "', '" + donate.Location + "', '" + donate.ContactNo + "', 1)";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            rowAffacted = cmd.ExecuteNonQuery();
            conn.Close();

            if (rowAffacted > 0)
            {
                TempData["message"] = "Donation Submitted Successfully";
               return RedirectToAction("index", "Home");
            }
            else
            {
                TempData["message"] = "Donation Submission Failed !";
                return View();
            }

        }

        public byte[] ImageToByteArray(IFormFile imageFile)
        {
            using var ms = new MemoryStream();
            imageFile.CopyTo(ms);
            byte[] fileBytes = ms.ToArray();
            return fileBytes;
        }
        private string SaveFile(IFormFile image)
        {
            var uniqueFileName = GetUniqueFileName(image.FileName);
            string fileUrl = "/uploads/" + uniqueFileName;
            var uploads = Path.Combine(Environment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploads, uniqueFileName);
            image.CopyTo(new FileStream(filePath, FileMode.Create));
            return fileUrl;

        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
