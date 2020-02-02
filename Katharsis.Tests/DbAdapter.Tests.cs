using System;
using System.Collections.Generic;
using Katharsis.model;
using Khatarsis.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Katharsis.Tests
{
    [TestClass]
    public class UnitTest1
    {
        DbAdapter adapter =new DbAdapter();
        [TestMethod]
        public void AddRmaFillWithValuesAndCheckValues()
        {
            //Arrange
            
            //new record
            int testedRmaId = adapter.WriteNewRmaRecord();

            Rma rmaIn = new Rma();
            rmaIn.Id = testedRmaId;
            rmaIn.Description = "Description Test";
            rmaIn.Reason = "Reason Test";
            rmaIn.So = "SO Test";
            rmaIn.InvoiceDate = "InvoiceDate Test";
            rmaIn.Company = "Company Test";
            rmaIn.Phone = "Phone Test";
            rmaIn.Mail = "Mail Test";

            Rma rmaOut;
            //act
            adapter.FillRmaRecord(rmaIn);
            rmaOut = adapter.ReadRmaLog(testedRmaId);

            //Assert
            Assert.AreEqual(rmaOut.Id,rmaIn.Id,"id");
            Assert.AreEqual(rmaOut.Reason,rmaIn.Reason,"Reason");
            Assert.AreEqual(rmaOut.So,rmaIn.So);
            Assert.AreEqual(rmaOut.InvoiceDate,rmaIn.InvoiceDate,"InvoiceDate");
            Assert.AreEqual(rmaOut.Company,rmaIn.Company,"Company");
            Assert.AreEqual(rmaOut.Phone,rmaIn.Phone,"Phone");
            Assert.AreEqual(rmaOut.Mail,rmaIn.Mail,"Mail");
        }

        [TestMethod]
        public void AddNewClientAndCheckValues()
        {
            //Arrange
            Client inputClient=new Client();
            inputClient.Comapny= "Test Company";
            inputClient.Phone= "Test Phone";
            inputClient.Mail= "Test Mail";
            Client outputClient;

            //Act
            //Add client & get its id
            inputClient.Id = adapter.WriteClient(inputClient);
            //Retrive client
            outputClient = adapter.ReadClient(inputClient.Id);

            //Assert
            Assert.AreEqual(inputClient.Id,outputClient.Id," client id");
            Assert.AreEqual(inputClient.Comapny,outputClient.Comapny," client Comapny");
            Assert.AreEqual(inputClient.Phone,outputClient.Phone," client Phone");
            Assert.AreEqual(inputClient.Mail,outputClient.Mail," client Mail");
        }

        [TestMethod]
        public void WriteAndReadProduct()
        {
            //Arrange

            int rma_id = new Random().Next(0,999);

            List<Product> products = new List<Product>();

            products.Add(new Product{
                        Model="Test Model 1",
                        Serial="Test Serial 1",
                        Description="Test Description 1",
                        Status="Test Status 1",
                        Rma_Id=rma_id
            }); 
            
            products.Add(new Product{
                        Model="Test Model 2",
                        Serial="Test Serial 2",
                        Description="Test Description 2",
                        Status="Test Status 2",
                        Rma_Id=rma_id
            });  
            
            List<Product> outputProducts;

            //Act

            //write
            foreach (var product in products)
            {
                adapter.WriteProduct(product);
            }

            //read
            outputProducts = adapter.ReadProducts(rma_id);

            //Assert
            Assert.AreEqual(outputProducts[0].Model, products[0].Model, "Model");
            Assert.AreEqual(outputProducts[0].Serial, products[0].Serial, "Serial");
            Assert.AreEqual(outputProducts[0].Description, products[0].Description, "Description");
            Assert.AreEqual(outputProducts[0].Status, products[0].Status, "Status");
            Assert.AreEqual(outputProducts[0].Rma_Id, products[0].Rma_Id, "Rma_Id");

            Assert.AreEqual(outputProducts[1].Model, products[1].Model, "Model");
            Assert.AreEqual(outputProducts[1].Serial, products[1].Serial, "Serial");
            Assert.AreEqual(outputProducts[1].Description, products[1].Description, "Description");
            Assert.AreEqual(outputProducts[1].Status, products[1].Status, "Status");
            Assert.AreEqual(outputProducts[1].Rma_Id, products[1].Rma_Id, "Rma_Id");
        }


        [TestMethod]
        public void EditProductAndCheckValues()
        {
            //act
            int random=new Random().Next(0,999);
            Product product = new Product
            {
                Model=$"Model Edit{random}",
                Serial=$"Serial Edit{random}",
                Description=$"Description Edit{random}",
                Status=$"Status Edit{random}",
                Rma_Id=random
            };
            

            //act
            adapter.EditProduct(product);


        }

    }
}
