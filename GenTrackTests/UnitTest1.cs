using NUnit.Framework;
using GenTrackDemo.Helpers;
using System.


namespace GenTrackTests
{
    public class Tests
    {
        
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            //arrange
            DataParser parser = new DataParser();
            string csv = "";

            //act
            parser.EvaluateDataSet(csv);

            //assert
            Assert.Pass();
        }
    }
}