using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenTrackDemo.Interfaces;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
//using GenTrackDemo.Entities;
 

namespace GenTrackDemo.Helpers
{
    /// <summary>
    /// Data Helper Base
    /// </summary>
    public abstract partial class DataHelperBase : IDataHelper
    {
        DataParser parser = null;

        /// <summary>
        /// DataHelperBase
        /// </summary>
        public DataHelperBase()
        {
            //initalise here
            parser = new DataParser();
        }

        /// <summary>
        /// DataHelperBase
        /// </summary>
        /// <param name="initDocumentPath"></param>
        public DataHelperBase(string initDocumentPath)
        {
            //initalise prepopulated file here

        }

        /// <summary>
        /// Load File
        /// </summary>
        /// <param name="documentToLoad"></param>
        /// <returns></returns>
        public bool LoadFile(string documentToLoad)
        {
            bool result = false;

            result = ProcessFile(documentToLoad);

            return result;
        }



        private bool ProcessFile(string documentToLoad)
        {

            try
            {
                ValidateIO(documentToLoad);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        private void ValidateIO(string documentToLoad)
        {
            if(!File.Exists(documentToLoad))
            {
                throw (new Exception("File does not exist or is not accessible."));
            }

            XmlSerializer ser = new XmlSerializer(typeof(aseXML));
            
            var fstream = new FileStream(documentToLoad, FileMode.Open);
            aseXML data = ser.Deserialize(fstream) as aseXML;
            if (data != null)
            {
                foreach(aseXMLTransactionsTransaction trans in data.Transactions.Transaction)
                {
                    string csv = trans.MeterDataNotification[0].CSVIntervalData;
                    if (csv.Length > 0)
                        parser.EvaluateDataSet(csv);
                    
                }

            }
        }
    }

    /// <summary>
    /// DataHelper class evaluates & performs data functions on xml and csv data & files
    /// </summary>
    public partial class DataHelper : DataHelperBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataHelper"/> class.
        /// </summary>
		public DataHelper()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataHelper"/> class with paramaeters.
        /// </summary>
        /// <param name="initDocumentPath"></param>
        public DataHelper(string initDocumentPath)
            : base(initDocumentPath)
        {

        }
    }
}
