using System;
using Katharsis.model;
using Khatarsis.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Katharsis.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void AddNewBalnkRmaAndFillItWithBasicData()
        {
            //Arrange
            DbAdapter adapter =new DbAdapter();
            //new record
            int testedRmaId = adapter.CreateNewRmaRecord();

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
            rmaOut = adapter.GetRmaLog(testedRmaId);

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
        public void AddNewClient()
        {
            //Arrange
            DbAdapter adapter =new DbAdapter();
            Client inputClient=new Client();
            inputClient.Comapny= "Test Company";
            inputClient.Phone= "Test Phone";
            inputClient.Mail= "Test Mail";
            Client outputClient;

            //Act
            //Add client & get its id
            inputClient.Id = adapter.AddClient(inputClient);
            //Retrive client
            outputClient = adapter.GetClient(inputClient.Id);

            //Assert
            Assert.AreEqual(inputClient.Id,outputClient.Id," client id");
            Assert.AreEqual(inputClient.Comapny,outputClient.Comapny," client Comapny");
            Assert.AreEqual(inputClient.Phone,outputClient.Phone," client Phone");
            Assert.AreEqual(inputClient.Mail,outputClient.Mail," client Mail");

        }

    }
}
