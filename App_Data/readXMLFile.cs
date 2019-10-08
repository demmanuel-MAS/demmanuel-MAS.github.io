using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;

namespace MASService1._0
{
    public class readXMLFile
    {
        
        public List<string> readMyXMLFile(string filePath, string email, string randomNum)
        {
            List<string> results = new List<string>();
            XmlTextReader reader = new XmlTextReader(filePath);
            while (reader.Read())
            {
                if (reader.Value == email || reader.Value == randomNum )//If email or code found
                {
                    results.Add(reader.Value);//return the email address
                }

            } 
            
            reader.Close();

            return results;
        }
    }
}