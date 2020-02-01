using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Katharsis.model;
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
                rmaId=Int32.Parse(cmd.ExecuteScalar().ToString());


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

        public void FillRmaRecord(Rma rma)
        {
            var con = new MySqlConnection(connString);
            var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE rma SET description=@description, reason=@reason, so=@so, invoice_date=@invoice_date " +
                $"WHERE id = {rma.Id}";

            cmd.Parameters.AddWithValue("@description",rma.Description);
            cmd.Parameters.AddWithValue("@reason",rma.Reason);
            cmd.Parameters.AddWithValue("@so",rma.So);
            cmd.Parameters.AddWithValue("@invoice_date",rma.InvoiceDate);
            

            try
            {
               con.Open();
               cmd.Prepare();
               cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("FillRmaRecord Error\n\n"+e.Message);
            }
            finally { con.Close(); }
        }

        public Rma GetRmaLog(int id)
        {

            var con = new MySqlConnection(connString);
            var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT id,description,reason,so,invoice_date  FROM rma WHERE id='{id}'";
            Rma returnedRma=new Rma();
            try
            {
               con.Open();
               var rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    returnedRma.Id= rdr.GetInt32(0);
                    returnedRma.Description= rdr.GetString(1);
                    returnedRma.Reason= rdr.GetString(2);
                    returnedRma.So= rdr.GetString(3);
                    returnedRma.InvoiceDate= rdr.GetString(4);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("GetRmaLog Error\n\n"+e.Message);
            }
            finally { con.Close(); }

            return returnedRma;
        }
    }
}
