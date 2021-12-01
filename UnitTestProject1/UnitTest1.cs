using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using GenTrackDemo.Helpers;
using ConsoleLogger.Tests;


namespace UnitTestProject1
{
    /// <summary>
    /// Tests the DataParser Class
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        DataParser parser = new DataParser();

        [TestMethod]
        public void ValidCSV()
        {
            //Arrange
            string csv = ConfigurationManager.AppSettings["ValidCSV"];
            var currentConsoleOut = Console.Out;

            //Act
            //Assert
            using (var consoleOutput = new ConsoleOutput())
            {
                parser.EvaluateDataSet(csv);

                string cOut = consoleOutput.GetOuput();
                if (cOut.Contains("File Created Ok"))
                    Assert.AreSame("File Created Ok", "File Created Ok");
                else
                    Assert.Fail();
            }
            
        }

        [TestMethod]
        public void MissingHeaderCSV()
        {
            //Arrange
            string csv = ConfigurationManager.AppSettings["MissingHeaderCSV"];

            //Act
            //Assert
            using (var consoleOutput = new ConsoleOutput())
            {
                try
                {
                    parser.EvaluateDataSet(csv);
                }
                catch (Exception ex)
                {
                    Assert.AreEqual("No Header", ex.Message);
                    return;
                }

                Assert.Fail();
            }
        }

        [TestMethod]
        public void MissingFooterCSV()
        {
            //Arrange
            string csv = ConfigurationManager.AppSettings["MissingFooterCSV"];

            //Act
            parser.EvaluateDataSet(csv);
            try
            {
                parser.EvaluateDataSet(csv);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual("No Footer", ex.Message);
                return;
            }
            //Assert
            Assert.Fail();

        }

        [TestMethod]
        public void IncorrectDataBundle1()
        {
            //Arrange
            string csv = ConfigurationManager.AppSettings["IncorrectDataBundle1"];

            //Act
            try
            {
                parser.EvaluateDataSet(csv);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual("No 300", ex.Message);
                return;
            }

            //Assert
            Assert.Fail();

        }

        [TestMethod]
        public void No200()
        {
            //Arrange
            string csv = ConfigurationManager.AppSettings["No200"];

            //Act
            //Assert
            using (var consoleOutput = new ConsoleOutput())
            {

                try
                {
                    parser.EvaluateDataSet(csv);
                }
                catch (Exception ex)
                {
                    Assert.AreEqual("No 200", ex.Message);
                    return;
                }

                Assert.Fail();
            }
        }

        [TestMethod]
        public void No300()
        {
            //Arrange
            string csv = ConfigurationManager.AppSettings["No300"];

            //Act
            //Assert
            using (var consoleOutput = new ConsoleOutput())
            {

                try
                {
                    parser.EvaluateDataSet(csv);
                }
                catch (Exception ex)
                {
                    Assert.AreEqual("No 300", ex.Message);
                    return;
                }

                Assert.Fail();
            }
        }
    }
}
