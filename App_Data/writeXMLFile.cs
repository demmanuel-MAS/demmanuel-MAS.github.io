using System;
using System.Xml;
using System.IO;

namespace MASService1._0
{
    public class writeXMLFile
    {
        string result;
        XmlDocument doc = new XmlDocument();
        public string WriteMyXMLFile(string filePath, string SOSName, string elementName)
        {
            doc.Load(filePath);//Open file

            //Find single node
            var node = doc.SelectSingleNode("//SOS");

            try
            {
                //Create a new element
                XmlElement element_Case = doc.CreateElement(elementName);
                XmlElement element_email = doc.CreateElement("email");
                node.AppendChild(element_Case);
                element_Case.AppendChild(element_email);
                element_email.InnerText = SOSName;

                doc.Save(filePath);
            }
            catch (Exception eX) { result = "" + eX; }

            return result;
        }
    }
}