using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorldHistoryBookStore.Models;

namespace WorldHistoryBookStore.Controllers
{
    public class adminController : Controller
    {
        private pubsEntities db = new pubsEntities();
        private string _ConnectionString = "data source=ALEXANDRA;initial catalog=pubs;integrated security=True;";

        // GET: admin
        public ActionResult Submit()
        {
            System.Diagnostics.Debug.WriteLine("inside Submit() ");

            return View();
        }

        public ActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine("inside Index() ");

            return View();
        }

        [HttpPost] //CHECK IF USER EXISTS. IF EXISTS RETURN ANOTHER UI IN HOMEPAGE, ELSE PROMPT USER TO CHANGE INPUT
        public ActionResult Check(string lname, string emp_id)
        {
            System.Diagnostics.Debug.WriteLine("inside Check(string lname, string emp_id) " + lname + " " + emp_id);

            var emp = db.employees.ToList();
            emp = emp.FindAll(ln => ln.lname == lname);
            emp = emp.FindAll(i => i.emp_id == emp_id);

            if (emp?.Any() != true) //user doesn't exists
            {
                Session["test"] = "none";
                return RedirectToAction("Submit");
            }
            else //USER EXISTS
            {
                Session["test"] = "admin";
                return RedirectToAction("../Home/Index");
            }

        }

        [HttpPost]
        public ActionResult Index(string submit, string select)
        {
            
            System.Diagnostics.Debug.WriteLine("inside Index(string submit) ");
            if (submit == "Restore")
            {
                string[] array = ReadBackupFiles();
                foreach (string x in array)
                {
                    if (select.Contains(x))
                    {
                        if (!x.EndsWith("Incremented.bak"))
                        {
                            ViewBag.Message = "You are not allowed to restore from an incremented backup file";
                            Restore(select);
                        }
                    }
                }
            }
            else if (submit == "Backup Incremented")
                BackupD();
            else
                Backup();
            
             return View();

        }

        private string[] ReadBackupFiles()
        {
            try
            {
                string subPath = "C:/SQLServerBackups";
                bool exists = System.IO.Directory.Exists(subPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(subPath);

                DirectoryInfo di = new DirectoryInfo(subPath);
                string[] files = Directory.GetFiles(subPath, "*.bak");
                List<string> filename = new List<string>();
                foreach (string n in files)
                {
                    filename.Add(System.IO.Path.GetFileName(n));
                }
                files = filename.ToArray();
                foreach (string i in files)
                {
                    System.Diagnostics.Debug.WriteLine(i);
                }
                return files;

            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message.ToString());
                return null;
            }
        }

        [HttpPost]
        protected void Backup()
        {
            System.Diagnostics.Debug.WriteLine("INSIDE BACKUP METHOD");

            try
            {
                string _DatabaseName = "pubs";
                string BackupName = _DatabaseName + "_" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "_" + "Full" + ".bak";

                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = _ConnectionString;
                sqlConnection.Open();

                string subPath = "C:/SQLServerBackups";
                bool exists = System.IO.Directory.Exists(subPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(subPath);

                string sqlQuery = "BACKUP DATABASE " + _DatabaseName + " TO DISK = 'C:\\SQLServerBackups\\" + BackupName + "' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = '" + BackupName + "', STATS = 10";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                int iRows = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                string msg = "The " + _DatabaseName + " database Backup with the name " + BackupName + " successfully...";
                System.Diagnostics.Debug.WriteLine(msg);
                ViewBag.Message = "Success! The " + _DatabaseName + " database backup with the name " + BackupName + " created succesfully";
                //ReadBackupFiles();
            }
            catch (SqlException sqlException)
            {
                System.Diagnostics.Debug.WriteLine(sqlException.Message.ToString());
                ViewBag.Message = "Fail";
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message.ToString());
                ViewBag.Message = "Fail";
            }

        }


        [HttpPost]
        protected void BackupD()
        {
            System.Diagnostics.Debug.WriteLine("INSIDE BACKUP D METHOD");

            try
            {
                string _DatabaseName = "pubs";
                string BackupName = _DatabaseName + "_" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "_" + "Incremented" + ".bak";                //string BackupName = DatabaseName + ".bak";
                //string BackupName = DatabaseName + ".bak";

                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = _ConnectionString;
                sqlConnection.Open();

                string subPath = "C:/SQLServerBackups";
                bool exists = System.IO.Directory.Exists(subPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(subPath);

                string sqlQuery = "BACKUP DATABASE " + _DatabaseName + " TO DISK = 'C:\\SQLServerBackups\\" + BackupName + "' WITH DIFFERENTIAL, MEDIANAME = 'Z_SQLServerBackups', NAME = '" + BackupName + "', STATS = 10";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                int iRows = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                string msg = "The " + _DatabaseName + " database Backup with the name " + BackupName + " successfully...";
                System.Diagnostics.Debug.WriteLine(msg);
                ViewBag.Message = "Success! The " + _DatabaseName + " database backup with the name " + BackupName + " created succesfully";
                //ReadBackupFiles();
            }
            catch (SqlException sqlException)
            {
                System.Diagnostics.Debug.WriteLine(sqlException.Message.ToString());
                ViewBag.Message = "Fail";
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message.ToString());
                ViewBag.Message = "Fail";
            }

        }




        [HttpPost]
        public void Restore(string file)
        {
            System.Diagnostics.Debug.WriteLine("INSIDE RESTORE METHOD");
            try
            {
                string subPath = "C:/SQLServerBackups";
                bool exists = System.IO.Directory.Exists(subPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(subPath);

                DirectoryInfo di = new DirectoryInfo(subPath);
                FileInfo[] f = di.GetFiles("*.bak");
                System.Diagnostics.Debug.WriteLine(f.ToList().ToString());

                if (f.Length > 0)
                {
                    string _DatabaseName = "pubs";
                    string _BackupName = file;

                    SqlConnection sqlConnection = new SqlConnection();
                    sqlConnection.ConnectionString = _ConnectionString;
                    sqlConnection.Open();

                    string sqlQuery = "USE master ALTER DATABASE " + _DatabaseName + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE RESTORE DATABASE " +
                        _DatabaseName + " FROM DISK = 'C:\\SQLServerBackups\\" + _BackupName + "' WITH REPLACE, NOUNLOAD, STATS = 10 ALTER DATABASE " + 
                        _DatabaseName + " SET MULTI_USER";

                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.CommandType = CommandType.Text;
                    int iRows = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    string msg = "The " + _DatabaseName + " database restored with the name " + _BackupName + " successfully...";
                    System.Diagnostics.Debug.WriteLine(msg);
                    ViewBag.Message = "Success! The " + _DatabaseName + " database restored with the name " + _BackupName + " succesfully";
                    
                }
            }
            catch (SqlException sqlException)
            {
                System.Diagnostics.Debug.WriteLine(sqlException.Message.ToString());
                ViewBag.Message = "Fail";
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message.ToString());
                ViewBag.Message = "Fail";
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
