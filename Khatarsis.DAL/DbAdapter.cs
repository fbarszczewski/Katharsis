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



        //create new record in Rma Table and return its id. If return -1 that mean error
        public int CreateNewRmaRecord()
        {
            var con = new MySqlConnection(connString);
            var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO rma (id) VALUES (NULL);";
            int rmaId = 0;
            try
            {
                //create record
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "select last_insert_id();";
                var rdr = cmd.ExecuteReader();

                // get id
                while(rdr.Read())
                    rmaId = rdr.GetInt32(0);
            }
            catch (Exception e)
            {
                MessageBox.Show("CreateNewRmaRecord Error\n\n"+e.Message);
                rmaId = -1;
            }
            finally { con.Close(); }
            
            return rmaId;
        }

        //return last id from RMA Table
        public int GetLastId()
        {
            int lastId = 0;
            var con = new MySqlConnection(connString);
            var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT id FROM rma ORDER BY id DESC LIMIT 1";
            try
            {
                con.Open();
                var rdr = cmd.ExecuteReader();

                while(rdr.Read())
                lastId = rdr.GetInt32(0);
            }
            catch (Exception e)
            {
                
                MessageBox.Show("GetLastId Error\n\n"+e.Message);
                lastId=-1;
            }
            finally { con.Close(); }


            return lastId;
        }

    }
}
