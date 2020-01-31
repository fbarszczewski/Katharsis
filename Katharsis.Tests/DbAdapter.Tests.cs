using System;
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
    }
}
