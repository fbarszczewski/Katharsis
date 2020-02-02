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

        private const string _connectionString = "SERVER = 127.0.0.1" + ";USERID= root" + ";PASSWORD= " +
                             ";DATABASE= katharsis" + ";Connection Timeout=3;";

        private MySqlConnection _connection;

        private MySqlCommand _command;


        /// <summary>
        /// Creates new blank rma record in database & return Rma id.
        /// </summary>
        /// <returns>Rma id</returns>
        public int WriteNewRmaRecord()
        {
            _connection = new MySqlConnection(_connectionString);
            _command = new MySqlCommand();
            _command.Connection = _connection;
            _command.CommandText = "INSERT INTO rma (id) VALUES (NULL);";


            int rmaId = 0;
            try
            {
                //create record
                _connection.Open();
                _command.ExecuteNonQuery();
                _command.CommandText = "select last_insert_id();";
                rmaId=Int32.Parse(_command.ExecuteScalar().ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("CreateNewRmaRecord Error\n\n"+e.Message);
                rmaId = -1;
            }
            finally { _connection.Close(); }
            
            return rmaId;

        }

        /// <summary>
        /// Insert data to Rma Record.
        /// </summary>
        /// <param name="rma"></param>
        public void FillRmaRecord(Rma rma)
        {
            _connection = new MySqlConnection(_connectionString);
            _command = new MySqlCommand();
            _command.Connection = _connection;
            _command.CommandText = "UPDATE rma SET " +
                "description = @description, " +
                "reason = @reason, " +
                "so = @so, " +
                "invoice_date = @invoice_date, " +
                "company = @company, " +
                "phone = @phone, " +
                "mail = @mail " +
                $"WHERE id = '{rma.Id}';";

            _command.Parameters.AddWithValue("@description",rma.Description);
            _command.Parameters.AddWithValue("@reason",rma.Reason);
            _command.Parameters.AddWithValue("@so",rma.So);
            _command.Parameters.AddWithValue("@invoice_date",rma.InvoiceDate);
            _command.Parameters.AddWithValue("@company",rma.Company);
            _command.Parameters.AddWithValue("@phone",rma.Phone);
            _command.Parameters.AddWithValue("@mail",rma.Mail);
            
            try
            {
               _connection.Open();
               _command.Prepare();
               _command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("FillRmaRecord Error\n\n"+e.Message);
            }
            finally { _connection.Close(); }
        }

        /// <summary>
        /// Retrun Rma from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Rma ReadRmaLog(int id)
        {

            _connection = new MySqlConnection(_connectionString);
            _command = new MySqlCommand();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT id,description,reason,so,invoice_date,company,phone,mail  FROM rma WHERE id='{id}';";
            Rma returnedRma=new Rma();
            try
            {
               _connection.Open();
               var rdr = _command.ExecuteReader();
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
            finally { _connection.Close(); }

            return returnedRma;
        }

        public void WriteProduct(List<Product> products)
        {
            _connection = new MySqlConnection(_connectionString);
            _command = new MySqlCommand();
            _command.Connection = _connection;
            _command.CommandText = "INSERT INTO product (model,serial,description,status,rma_id) " +
                              "VALUES (@model,@serial,@description,@status,@rma_id);";
            try
            {
                //create record
                
                foreach (Product product in products)
                {

                    _command.Parameters.AddWithValue("@model",product.Model);
                    _command.Parameters.AddWithValue("@serial",product.Serial);
                    _command.Parameters.AddWithValue("@description",product.Description);
                    _command.Parameters.AddWithValue("@status",product.Status);
                    _command.Parameters.AddWithValue("@rma_id",product.Rma_Id);

                    _connection.Open();
                    _command.Prepare();
                    _command.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("AddProduct Error\n\n"+e.Message);
            }
            finally 
            { 
                _connection.Close(); 
            }
            
        }

        public void WriteProduct(Product product)
        {
            _connection = new MySqlConnection(_connectionString);
            _command = new MySqlCommand();
            _command.Connection = _connection;
            _command.CommandText = "INSERT INTO product (model,serial,description,status,rma_id) " +
                              "VALUES (@model,@serial,@description,@status,@rma_id);";

                _command.Parameters.AddWithValue("@model",product.Model);
                _command.Parameters.AddWithValue("@serial",product.Serial);
                _command.Parameters.AddWithValue("@description",product.Description);
                _command.Parameters.AddWithValue("@status",product.Status);
                _command.Parameters.AddWithValue("@rma_id",product.Rma_Id);

            try
            {
                _connection.Open();
                _command.Prepare();
                _command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("AddProduct Error\n\n"+e.Message);
            }
            finally 
            { 
                _connection.Close(); 
            }
            
        }

        public List<Product> ReadProducts(int rma_id)
        {
            
            _connection = new MySqlConnection(_connectionString);
            _command = new MySqlCommand();
            _command.Connection = _connection;
            _command.CommandText = 
                $"SELECT id,model,serial,description,status,rma_id " +
                $"FROM product " +
                $"WHERE rma_id='{rma_id}';";

            List<Product>products=new List<Product>();

            try
            {
               _connection.Open();
               var rdr = _command.ExecuteReader();
                while(rdr.Read())
                {
                    products.Add(new Product{
                        Id = rdr.GetInt32(0),
                        Model=rdr.GetString(1),
                        Serial=rdr.GetString(2),
                        Description=rdr.GetString(3),
                        Status=rdr.GetString(4),
                        Rma_Id=rdr.GetInt32(5)
                    });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ReadProducts Error\n\n"+e.Message);
            }
            finally { _connection.Close(); }

            return products;
        }

        

        public void EditProduct()
        {

        }



        public void WriteComment()
        {

        }
        public void EditComment()
        {

        }




        /// <summary>
        /// Adds new client to database
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public int WriteClient(Client client)
        {
            int id=0;
            _connection = new MySqlConnection(_connectionString);
            _command = new MySqlCommand();
            _command.Connection = _connection;
            _command.CommandText = "INSERT INTO " +
                "client (company, phone, mail) " +
                "VALUES (@company,@phone,@mail);";

            _command.Parameters.AddWithValue("@company",client.Comapny);
            _command.Parameters.AddWithValue("@phone",client.Phone);
            _command.Parameters.AddWithValue("@mail",client.Mail);
            try
            {
                _connection.Open();
                _command.Prepare();
                _command.ExecuteNonQuery();
                _command.CommandText = "select last_insert_id();";
                id=Int32.Parse(_command.ExecuteScalar().ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("AddClient Error\n\n"+e.Message);
                id=-1;
            }
            finally { _connection.Close(); }
            return id;
        }

        /// <summary>
        /// Returns client from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Client ReadClient(int id)
        {

            _connection = new MySqlConnection(_connectionString);
            _command = new MySqlCommand();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM client WHERE id='{id}';";
            Client client = new Client();
            try
            {
               _connection.Open();
               var rdr = _command.ExecuteReader();
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
            finally { _connection.Close(); }

            return client;
        }

        
    }
}

