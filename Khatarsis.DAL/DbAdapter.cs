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

        private const string connString = "SERVER = 127.0.0.1" + ";USERID= root" + ";PASSWORD= " +
                             ";DATABASE= katharsis" + ";Connection Timeout=3;";



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



        //Fills rma record with description,reason,so,invoice_date
        public void FillRmaRecord(Rma rma)
        {
            var con = new MySqlConnection(connString);
            var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE rma SET " +
                "description = @description, " +
                "reason = @reason, " +
                "so = @so, " +
                "invoice_date = @invoice_date, " +
                "company = @company, " +
                "phone = @phone, " +
                "mail = @mail " +
                $"WHERE id = {rma.Id}";

            cmd.Parameters.AddWithValue("@description",rma.Description);
            cmd.Parameters.AddWithValue("@reason",rma.Reason);
            cmd.Parameters.AddWithValue("@so",rma.So);
            cmd.Parameters.AddWithValue("@invoice_date",rma.InvoiceDate);
            cmd.Parameters.AddWithValue("@company",rma.Company);
            cmd.Parameters.AddWithValue("@phone",rma.Phone);
            cmd.Parameters.AddWithValue("@mail",rma.Mail);
            
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

        public void AddProduct()
        {

        }

        public void AddComment()
        {

        }

        public int AddClient(Client client)
        {
            int id=0;
            var con = new MySqlConnection(connString);
            var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO " +
                "client (company, phone, mail) " +
                "VALUES (@company,@phone,@mail);";

            cmd.Parameters.AddWithValue("@company",client.Comapny);
            cmd.Parameters.AddWithValue("@phone",client.Phone);
            cmd.Parameters.AddWithValue("@mail",client.Mail);
            try
            {
                con.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "select last_insert_id();";
                id=Int32.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("AddClient Error\n\n"+e.Message);
                id=-1;
            }
            finally { con.Close(); }
            return id;
        }

        public Client GetClient(int id)
        {

            var con = new MySqlConnection(connString);
            var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM client WHERE id='{id}'";
            Client client = new Client();
            try
            {
               con.Open();
               var rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    client.Id= rdr.GetInt32(0);
                    client.Comapny= rdr.GetString(1);
                    client.Phone= rdr.GetString(2);
                    client.Mail= rdr.GetString(3);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("GetClient Error\n\n"+e.Message);
            }
            finally { con.Close(); }

            return client;
        }

        public Rma GetRmaLog(int id)
        {

            var con = new MySqlConnection(connString);
            var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT id,description,reason,so,invoice_date,company,phone,mail  FROM rma WHERE id='{id}'";
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
                    returnedRma.Company= rdr.GetString(5);
                    returnedRma.Phone= rdr.GetString(6);
                    returnedRma.Mail= rdr.GetString(7);
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
