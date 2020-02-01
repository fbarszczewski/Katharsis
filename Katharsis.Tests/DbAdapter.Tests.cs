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
        // test if new record is created
        public void InsertNewBlankRma_Insert()
        {

            //Arrange
            DbAdapter adapter =new DbAdapter();
            int lastId=adapter.GetLastId();
            int rmaId;

            //Act
            rmaId=adapter.CreateNewRmaRecord();

            //Assert
            
            Assert.AreEqual(lastId+1,rmaId);

        }


        [TestMethod]
        // teast injecting data to newly created rma
        public void FillRmaRecord_DataInsert()
        {
            //arrange
            DbAdapter adapter =new DbAdapter();

            Rma rmaIn = new Rma();
            rmaIn.Id = 1;
            rmaIn.Description = "Description Test";
            rmaIn.Reason = "Reason Test";
            rmaIn.So = "SO Test";
            rmaIn.InvoiceDate = "InvoiceDate Test";

            Rma rmaOut;

            //act
            adapter.FillRmaRecord(rmaIn);
            rmaOut = adapter.GetRmaLog(rmaIn.Id);
            //assert
            Assert.AreEqual(rmaOut.Id,rmaIn.Id);
            Assert.AreEqual(rmaOut.Reason,rmaIn.Reason);
            Assert.AreEqual(rmaOut.So,rmaIn.So);
            Assert.AreEqual(rmaOut.InvoiceDate,rmaIn.InvoiceDate);

        }
    }
}
