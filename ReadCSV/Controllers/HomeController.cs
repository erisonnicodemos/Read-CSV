using ReadCSV.Models;
using ReadCSV.Models.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadCSV.Controllers
{
    public class HomeController : Controller
    {
        DBModel db = new DBModel();

        // GET: Home
        public ActionResult Index()
        {
            //return View(new List<Customers>());
            return View(db.Customers.ToList());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            List<Customers> customers = new List<Customers>();
            string filepatch = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filepatch = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filepatch);

                string csvData = System.IO.File.ReadAllText(filepatch);
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        customers.Add(new Customers
                        {
                            Id = Convert.ToInt32(row.Split(';')[0]),
                            Name = row.Split(';')[1],
                            Country = row.Split(';')[2]
                        });
                    }
                }
            }
            db.Customers.AddRange(customers);

            db.SaveChanges();
            return View(customers);
        }
    }
}