using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldHistoryBookStore.Models;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace WorldHistoryBookStore.Controllers
{
    public class HomeController : Controller
    {
        private pubsEntities db = new pubsEntities();
       // private string _ConnectionString = "data source=ALEXANDRA;initial catalog=pubs;integrated security=True;";

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string submit)
        {
            if (submit == "Tools")
                return RedirectToAction("../admin/Index");
            else
                return View();
            /*
            System.Diagnostics.Debug.WriteLine("inside Index(string submit) ");
            if (submit == "Restore")
            {
                string[] array = ReadBackupFiles();
                foreach (string x in array)
                {
                    if (select.Contains(x))
                    {
                        System.Diagnostics.Debug.WriteLine("It exists " + select + " " + x);
                        Restore(select);
                    }
                }
            }
            else if (submit == "Backup Incremented")
                BackupD();
            else
                Backup();
            */
            // return View();

        }


        /*private string[] ReadBackupFiles()
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
                //string BackupName = _DatabaseName + "" + DateTime.Now.Day.ToString() + "" + DateTime.Now.Month.ToString() + "" + DateTime.Now.Year.ToString() + ".bak";
                string _BackupName = _DatabaseName + ".bak";

                System.Data.SqlClient.SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = _ConnectionString;
                sqlConnection.Open();

                string subPath = "C:/SQLServerBackups";
                bool exists = System.IO.Directory.Exists(subPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(subPath);

                string sqlQuery = "BACKUP DATABASE " + _DatabaseName + " TO DISK = 'C:\\SQLServerBackups\\" + _BackupName + "' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = '" + _BackupName + "', STATS = 10";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                int iRows = sqlCommand.ExecuteNonQuery();
                try
                {
                    sqlConnection.Close();
                    string msg = "The " + _DatabaseName + " database Backup with the name " + _BackupName + " successfully...";
                    System.Diagnostics.Debug.WriteLine(msg);
                    ViewBag.Message = "Success! The " + _DatabaseName + " database backup with the name " + _BackupName + " created succesfully";
                }
                catch (SqlException se)
                {
                    ViewBag.Message = "Fail";
                }
                //ReadBackupFiles();
            }
            catch (SqlException sqlException)
            {
                System.Diagnostics.Debug.WriteLine(sqlException.Message.ToString());
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message.ToString());
            }

        }

        [HttpPost]
        protected void Restore()
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
                    string _BackupName = "pubs.bak";

                    SqlConnection sqlConnection = new SqlConnection();
                    sqlConnection.ConnectionString = _ConnectionString;
                    sqlConnection.Open();

                    string sqlQuery = "USE master RESTORE DATABASE " + _DatabaseName + " FROM DISK = 'C:\\SQLServerBackups\\" + _BackupName + "' WITH REPLACE, STATS = 10";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.CommandType = CommandType.Text;
                    int iRows = sqlCommand.ExecuteNonQuery();
                    try
                    {
                        sqlConnection.Close();
                        string msg = "The " + _DatabaseName + " database restored with the name " + _BackupName + " successfully...";
                        System.Diagnostics.Debug.WriteLine(msg);
                        ViewBag.Message = "Success! The " + _DatabaseName + " database restored with the name " + _BackupName + " succesfully";
                    }
                    catch (SqlException se)
                    {
                        ViewBag.Message = "Fail";
                    }
                }
            }
            catch (SqlException sqlException)
            {
                System.Diagnostics.Debug.WriteLine(sqlException.Message.ToString());
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message.ToString());
            }

        }*/
    }
}