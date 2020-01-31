using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient; 

namespace Khatarsis.DAL
{
    public class DbAdapter
    {
        private const string connString = "SERVER = remotemysql.com" + ";USERID= IalS35jGSf" + ";PASSWORD= lHxoGp4AQC" +
                                  ";DATABASE= IalS35jGSf" + ";Connection Timeout=3;";

        //create new record in mysql database to reserve id
        private void InsertNewBlankRma()
        {

            var con = new MySqlConnection(connString);

            var cmd = new MySqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "INSERT INTO rma (id) VALUES (NULL)";

            try
            {
            con.Open();
            cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("InsertNewBlankRma Error\n\n"+e.Message);
            }
            finally { con.Close(); }
        }

        //returns last RMA id
        private void SelectLastRmaId()
        {

        }



    }
}
