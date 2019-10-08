using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Text;

namespace MASService1._0
{
    public class CreateXMLFile
    {

        public void createMyXMLFile(string filePath)
        {
            XmlWriter writer = XmlWriter.Create("C://Users/Bean Head/source/repos/MASService1.0/App_Data/alert_comms.xml");
            writer.WriteStartElement("SOS");
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
        }
    }
}